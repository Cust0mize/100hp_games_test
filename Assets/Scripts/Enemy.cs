using UnityEngine;
using Zenject;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyHealth _health;
    [SerializeField] private MoveToTarget _moveToTarget;
    [SerializeField] private float _rewardForKill = 1;
    private SignalBus _signalBus;

    public void Init(SignalBus signalBus, Vector3 targetPosition) {
        _signalBus = signalBus;
        _moveToTarget.Init(targetPosition);
        _health.Init(signalBus, this);
    }

    public float GetReward() {
        return _rewardForKill;
    }

    public void Stop() {
        _moveToTarget.StopMove();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.GetComponent<Tower>()) {
            _signalBus.Fire<SignalGameOver>();
        }
    }
}
