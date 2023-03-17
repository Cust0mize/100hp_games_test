using System.Collections.Generic;
using Random = UnityEngine.Random;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using Zenject;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnZones;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private float _timeSpawnDelay = 2;
    [SerializeField] private int _spawnCount = 5;
    private List<Enemy> _enemies = new List<Enemy>();
    private Enemy _oldEnemy;
    private Tower _tower;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(SignalBus signalBus) {
        _signalBus = signalBus;
    }

    public void Init(Tower tower) {
        _tower = tower;
        _signalBus.Subscribe<SignalRemoveEnemy>(RemoveEnemie);
        _signalBus.Subscribe<SignalGameOver>(StopCreateEnemies);
        CreateEnemies();
    }

    public void CreateEnemies() {
        for (int i = 0; i < _spawnCount; i++) {
            Enemy enemy = Instantiate(_enemyPrefab, SpawnTo(), Quaternion.identity);
            if (enemy.Init(_signalBus, _tower.transform.position)) {
                Destroy(enemy.gameObject);
                i--;
                continue;
            }
            else {
                _enemies.Add(enemy);
            }
        }

        _signalBus.Fire(new SignalNewWave(_enemies[Random.Range(0, _enemies.Count)]));
    }

    public Enemy ChangeRandomEnemie() {
        if (_enemies.Count == 0) return null;
        Enemy newEnemy = null;
        int tryCount = 0;
        do {
            newEnemy = _enemies[Random.Range(0, _enemies.Count)];

            if (newEnemy == _oldEnemy) {
                tryCount++;
                if (tryCount == 5) {
                    break;
                }
            }
            else {
                tryCount = 0;
            }
        } while (newEnemy == _oldEnemy);
        _oldEnemy = newEnemy;
        return newEnemy;
    }

    private void StopCreateEnemies() {
        _signalBus.Unsubscribe<SignalGameOver>(StopCreateEnemies);

        foreach (var enemy in _enemies) {
            enemy.Stop();
        }
    }

    private Vector3 SpawnTo() {
        Transform currentSpawnZone = _spawnZones[Random.Range(0, _spawnZones.Count)];
        Vector3 localpoint = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
        Vector3 position = currentSpawnZone.TransformPoint(localpoint);
        return position;
    }

    private async void RemoveEnemie(SignalRemoveEnemy signalRemoveEnemy) {
        _enemies.Remove(signalRemoveEnemy.Enemy);

        if (_enemies.Count == 0) {
            _enemies.Clear();
            await UniTask.Delay(TimeSpan.FromSeconds(_timeSpawnDelay));
            CreateEnemies();
        }
    }
}
