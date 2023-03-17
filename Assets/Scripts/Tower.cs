using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using Zenject;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _startShootTransform;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private CircleRenderer _circleRenderer;
    [SerializeField] private float _shootTime;
    [SerializeField] private float _shootRadius;
    private EnemiesFactory _enemiesFactory;
    private Enemy _targetEnemy;
    private GameSaver _gameSaver;
    private SignalBus _signalBus;
    private bool _isShooting = true;

    public static float _defaultShootTime{ get; private set; }
    public static float _defaultShootRadius{ get; private set; }

    [Inject]
    public void Construct(GameSaver gameSaver, SignalBus signalBus) {
        _gameSaver = gameSaver;
        _signalBus = signalBus;
    }

    public void DoInit() {
        _defaultShootTime = _shootTime;
        _defaultShootRadius = _shootRadius;
    }

    public void Init(EnemiesFactory enemiesFactory) {
        _enemiesFactory = enemiesFactory;
        _targetEnemy = _enemiesFactory.ChangeRandomEnemie();
        SubscribeSignal();
        UpdateShootTime();
        UpdateRadius();
        Shoot();
    }

    private async void Shoot(SignalNewWave signalNewWave = null) {
        if (_isShooting) {
            await UniTask.Delay(TimeSpan.FromSeconds(_shootTime));

            if (signalNewWave != null) {
                _targetEnemy = signalNewWave.TargetEnemy;
            }

            while (_targetEnemy != null && _isShooting) {
                if (Vector3.Distance(transform.position, _targetEnemy.transform.position) > _shootRadius) {
                    _targetEnemy = _enemiesFactory.ChangeRandomEnemie();
                    await UniTask.Delay(TimeSpan.FromSeconds(0.01f));
                    continue;
                }
                else {
                    Bullet bullet = Instantiate(_bulletPrefab, _startShootTransform.position, _startShootTransform.rotation);
                    bullet.Init(_gameSaver, _targetEnemy.transform.position);
                    await UniTask.Delay(TimeSpan.FromSeconds(_shootTime));
                    _targetEnemy = _enemiesFactory.ChangeRandomEnemie();
                }
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
        _signalBus.Subscribe<SignalGameOver>(StopShooting);
    }

    private void StopShooting() {
        _isShooting = false;
    }

    private void UpdateRadius() {
        _shootRadius = _gameSaver.GetTowerRadius();
        _circleRenderer.Init(_shootRadius);
    }
}
