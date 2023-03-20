using UnityEngine;

public class EnemyMove : MonoBehaviour, IEnemyMove
{
    [SerializeField] private float _speed = 3;
    private Vector3 _targetPosition;
    private float _currentSpeed;

    public void Init(Vector3 targetPosition) {
        _targetPosition = targetPosition - transform.position;
    }

    public void StopMove() {
        _currentSpeed = 0;
    }

    public void StartMove() {
        _currentSpeed = _speed;
    }

    private void Move() {
        transform.position += _targetPosition.normalized * _currentSpeed * Time.deltaTime;
    }

    private void Update() {
        Move();
    }
}
