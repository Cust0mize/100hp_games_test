using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;
using Zenject;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnZones;
    [SerializeField] private Enemy _enemyPrefab;
    private List<Enemy> _enemies = new List<Enemy>();
    private Tower _tower;
    private SignalBus _signalBus;
    private GameSaver _gameSaver;
    private int _waweNumber;
    private bool _isFirstBoot = true;

    [Inject]
    public void Construct(SignalBus signalBus, GameSaver gameSaver) {
        _signalBus = signalBus;
        _gameSaver = gameSaver;
        _waweNumber = _gameSaver.GetWaweNumber();
    }

    public void Init(Tower tower) {
        _tower = tower;
        _signalBus.Subscribe<SignalRemoveEnemy>(RemoveEnemie);
        _signalBus.Subscribe<SignalGameOver>(StopCreateEnemies);
        CreateEnemies();
    }

    public async void CreateEnemies() {
        _gameSaver.SetWaweNumber(_waweNumber);

        for (int i = 0; i < _waweNumber; i++) {
            Enemy enemy = Instantiate(_enemyPrefab, SpawnTo(), Quaternion.identity);

            if (await enemy.Init(_signalBus, _tower.transform.position)) {
                Destroy(enemy.gameObject);
                i--;
            }
            else {
                enemy.Stop();
                _enemies.Add(enemy);
            }
        }
        StartNewWave();
    }

    public Enemy ChangeRandomEnemie() {
        if (_enemies.Count == 0) return null;
        Enemy newEnemy = _enemies[Random.Range(0, _enemies.Count)];
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

    private void RemoveEnemie(SignalRemoveEnemy signalRemoveEnemy) {
        _enemies.Remove(signalRemoveEnemy.Enemy);

        if (_enemies.Count == 0) {
            _enemies.Clear();
            CreateEnemies();
        }
    }

    private void StartNewWave() {
        foreach (var enemy in _enemies) {
            enemy.StartMove();
        }

        _waweNumber++;

        if (_isFirstBoot) {
            _isFirstBoot = false;
            _signalBus.Fire(new SignalNewWave(_enemies[Random.Range(0, _enemies.Count)]));
        }
    }
}
