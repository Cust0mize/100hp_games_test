using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    protected float CurrentSpeed;
    protected Vector3 TargetPosition;

    public virtual void Init(Vector3 targetPosition) {
        TargetPosition = targetPosition;
        CurrentSpeed = _speed;
    }

    protected float GetSpeed() {
        return _speed;
    }

    private void Update() {
        Move();
    }

    private void Move() {
        Vector3 direction = TargetPosition - transform.position;
        Vector3 newPosition = direction.normalized * CurrentSpeed * Time.deltaTime;
        transform.position += newPosition;
    }
}
