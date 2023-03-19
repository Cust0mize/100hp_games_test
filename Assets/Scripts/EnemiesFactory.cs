using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnZones;
    private List<Enemy> _enemies = new List<Enemy>();
    private Tower _tower;
    private SignalBus _signalBus;
    private GameSaver _gameSaver;
    private MoneyService _moneyService;
    private ResourceLoader _resourceLoader;
    private int _waweNumber;
    private bool _isFirstBoot = true;
    private bool _isInitEnemyEnd = false;

    [Inject]
    public void Construct(SignalBus signalBus, GameSaver gameSaver, MoneyService moneyService, ResourceLoader resourceLoader) {
        _signalBus = signalBus;
        _gameSaver = gameSaver;
        _moneyService = moneyService;
        _resourceLoader = resourceLoader;
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
        _isInitEnemyEnd = false;

        for (int i = 0; i < _waweNumber; i++) {
            GameObject newObject = Instantiate(ChangeEnemy(), SpawnTo(), Quaternion.identity);
            Enemy enemy = newObject.GetComponent<Enemy>();

            if (await enemy.CheckCollision()) {
                Destroy(enemy.gameObject);
                i--;
            }
            else {
                if (_enemies.Count > _waweNumber) {
                    Destroy(enemy.gameObject);
                    continue;
                }
                enemy.Init(_signalBus, _tower.transform.position);
                enemy.Stop();
                _enemies.Add(enemy);
            }
        }
        _isInitEnemyEnd = true;
        StartNewWave();
    }

    public Enemy ChangeRandomEnemie() {
        if (_enemies.Count == 0 || !_isInitEnemyEnd) return null;

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
        _moneyService.AddCoinForEnemyKill(signalRemoveEnemy.Enemy);

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

    private GameObject ChangeEnemy() {
        GameObject enemy = null;
        if (_waweNumber >= 5) {
            int randomIndex = Random.Range(0, 2);
            if (randomIndex == 0) {
                enemy = _resourceLoader.GetEnemy("StandartEnemy");
            }
            else {
                enemy = _resourceLoader.GetEnemy("EnemySinusMove");
            }
        }
        else {
            enemy = _resourceLoader.GetEnemy("StandartEnemy");
        }
        return enemy;
    }
}
