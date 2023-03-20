using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    private Vector3 _targetPosition;

    public void Init(Vector3 targetPosition) {
        _targetPosition = targetPosition - transform.position;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        transform.position += _targetPosition.normalized * _speed * Time.deltaTime;
    }
}
