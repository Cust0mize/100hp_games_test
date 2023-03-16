using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    private Vector3 _targetPosition;

    public void Init(Vector3 targetPosition) {
        _targetPosition = targetPosition;
    }

    private void Update() {
        MoveToTower();
    }

    private void MoveToTower() {
        Vector3 direction = _targetPosition - transform.position;
        Vector3 moveDirection = direction.normalized;
        Vector3 newPosition = transform.position + moveDirection * _speed * Time.deltaTime;
        transform.position = newPosition;
    }
}