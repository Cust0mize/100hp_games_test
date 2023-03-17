using UnityEngine;

public class MoveToTarget : MonoBehaviour
{
    [field: SerializeField] protected float Speed { get; private set; } = 3;
    protected float CurrentSpeed;
    protected Vector3 TargetPosition;

    public virtual void Init(Vector3 targetPosition) {
        TargetPosition = targetPosition;
        CurrentSpeed = Speed;
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
