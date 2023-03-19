using UnityEngine;
using Zenject;

public class EnemyHealth : MonoBehaviour, IDamageble
{
    [SerializeField] private float _health = 1;
    private SignalBus _signalBus;
    private Enemy _enemy;
    private bool _isTakeDamage;

    public void Init(SignalBus signalBus, Enemy enemy) {
        _enemy = enemy;
        _signalBus = signalBus;
        _isTakeDamage = true;
    }

    public void TakeDamage(float damage) {
        if (!_isTakeDamage) return;

        _health -= damage;
        if (_health <= 0) {
            Die();
        }
    }

    public void Die() {
        _signalBus.Fire(new SignalRemoveEnemy(_enemy));
        _signalBus.Fire<SignalUpdateCoinValue>();
        Destroy(gameObject);
    }
}
