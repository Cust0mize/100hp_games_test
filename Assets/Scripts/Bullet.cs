using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MoveToTarget _moveToTarget;
    [SerializeField] private float _damage;

    public static float DefaultBulletDamage { get; private set; }

    public void Init(GameSaver gameSaver, Vector3 targetPosition) {
        DefaultBulletDamage = _damage;

        _moveToTarget.Init(targetPosition);
        _damage = gameSaver.GetSkillValue(SkillType.AttackSkill);
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