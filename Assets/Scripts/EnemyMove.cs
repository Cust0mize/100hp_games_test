using UnityEngine;

public class EnemyMove : MoveToTarget
{
    public override void Init(Vector3 targetPosition) {
        TargetPosition = targetPosition - transform.position;
    }

    public virtual void StopMove() {
        CurrentSpeed = 0;
    }

    public virtual void StartMove() {
        CurrentSpeed = GetSpeed();
    }
}
