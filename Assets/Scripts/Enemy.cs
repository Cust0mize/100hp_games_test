using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private EnemyMove _enemyMove;
    [SerializeField] private float _rewardForKill = 1;
    private SignalBus _signalBus;
    private bool _isCollisionEnemy;

    public async UniTask<bool> Init(SignalBus signalBus, Vector3 targetPosition) {
        _spriteRenderer.enabled = false;
        _signalBus = signalBus;
        _enemyMove.Init(targetPosition);
        _health.Init(signalBus, this);
        await UniTask.Delay(TimeSpan.FromMilliseconds(20));
        return _isCollisionEnemy;
    }

    public float GetReward() {
        return _rewardForKill;
    }

    public void Stop() {
        _enemyMove.StopMove();
    }    
    
    public void StartMove() {
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
