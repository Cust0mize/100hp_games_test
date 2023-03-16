using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using Zenject;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _startShootTransform;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private CircleRenderer _circleRenderer;
    private EnemiesFactory _enemiesFactory;
    private Enemy _targetEnemie;
    private GameSaver _gameSaver;
    private SignalBus _signalBus;
    private float _shootTime;
    private float _shootRadius;

    [Inject]
    public void Construct(GameSaver gameSaver, SignalBus signalBus) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
    }

    public void Init(EnemiesFactory enemiesFactory) {
        _enemiesFactory = enemiesFactory;
        _targetEnemie = _enemiesFactory.ChangeRandomEnemie();
        SubscribeSignal();
        UpdateShootTime();
        UpdateRadius();
        Shoot();
    }

    private async void Shoot(SignalNewWave signalNewWave = null) {
        await UniTask.Delay(TimeSpan.FromSeconds(_shootTime));

        if (signalNewWave != null) {
            _targetEnemie = signalNewWave.TargetEnemy;
        }

        while (_targetEnemie != null) {
            if (Vector3.Distance(transform.position, _targetEnemie.transform.position) > _shootRadius) {
                _targetEnemie = _enemiesFactory.ChangeRandomEnemie();
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
                continue;
            }
            else {
                Bullet bullet = Instantiate(_bulletPrefab, _startShootTransform.position, _startShootTransform.rotation);
                bullet.Init(_gameSaver, _targetEnemie.transform.position);
                await UniTask.Delay(TimeSpan.FromSeconds(_shootTime));
                _targetEnemie = _enemiesFactory.ChangeRandomEnemie();
            }
        }
    }

    private void UpdateShootTime() {
        _shootTime = _gameSaver.GetShootTime();
    }

    private void SubscribeSignal() {
        _signalBus.Subscribe<SignalNewWave>(Shoot);
        _signalBus.Subscribe<SignalUpdateRadius>(UpdateRadius);
        _signalBus.Subscribe<SignalUpdateShootTime>(UpdateShootTime);
    }

    private void UpdateRadius() {
        _shootRadius = _gameSaver.GetTowerRadius();
        _circleRenderer.Init(_shootRadius);
    }

    private void OnDestroy() {
        _signalBus.Unsubscribe<SignalNewWave>(Shoot);
    }
}
