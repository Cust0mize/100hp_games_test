using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;
using System;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyMove _enemyMove;
    [SerializeField] private float _rewardForKill = 1;
    private SignalBus _signalBus;
    private bool _isCollisionEnemy;
    private float _timeCollisionDetection = 20;

    public void Init(SignalBus signalBus, Vector3 targetPosition) {
        _signalBus = signalBus;
        _enemyMove.Init(targetPosition);
        _health.Init(signalBus, this);
    }

    public async UniTask<bool> CheckCollision() {
        _spriteRenderer.enabled = false;
        await UniTask.Delay(TimeSpan.FromMilliseconds(_timeCollisionDetection));
        return _isCollisionEnemy;
    }

    public float GetReward() {
        return _rewardForKill;
    }

    public void Stop() {
        _health.enabled = false;
        _enemyMove.StopMove();
    }

    public void StartMove() {
        _health.enabled = true;
        _spriteRenderer.enabled = true;
        _enemyMove.StartMove();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Tower>()) {
            _signalBus.Fire<SignalGameOver>();
        }

        if (collision.GetComponent<Enemy>()) {
            _isCollisionEnemy = true;
        }
    }
}
