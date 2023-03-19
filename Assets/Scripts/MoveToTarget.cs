using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    protected float CurrentSpeed;
    protected Vector3 TargetPosition;

    public virtual void Init(Vector3 targetPosition) {
        TargetPosition = targetPosition;
        CurrentSpeed = _speed;
        TargetPosition = targetPosition - transform.position;
    }

    protected float GetSpeed() {
        return _speed;
    }

    private void Update() {
        Move();
    }

    protected virtual void Move() {
        transform.position += TargetPosition.normalized * CurrentSpeed * Time.deltaTime;
    }
}
