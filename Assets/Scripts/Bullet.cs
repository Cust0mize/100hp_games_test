using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MoveToTarget _moveToTarget;
    [SerializeField] private float _damage;

    public static float _defaultBulletDamage { get; private set; }

    public void Init(GameSaver gameSaver, Vector3 targetPosition) {
        _moveToTarget.Init(targetPosition);
        _damage = gameSaver.GetBulletDamage();
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.TryGetComponent(out IDamageble damageble)) {
            damageble.TakeDamage(_damage);
            DestroyBullet();
        }
    }

    private void DestroyBullet() {
        Destroy(gameObject);
    }
}