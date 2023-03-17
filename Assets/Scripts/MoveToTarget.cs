using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    private Vector3 _targetPosition;

    public void Init(Vector3 targetPosition) {
        _targetPosition = targetPosition;
    }

    public void StopMove() {
        _speed = 0;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        Vector3 direction = _targetPosition - transform.position;
        Vector3 newPosition = direction.normalized * _speed * Time.deltaTime;
        transform.position += newPosition;
    }
}