using UnityEngine;

public class EnemyMove : MoveToTarget
{
    public override void Init(Vector3 targetPosition) {
        TargetPosition = targetPosition;
    }

    public void StopMove() {
        CurrentSpeed = 0;
    }

    public void StartMove() {
        CurrentSpeed = Speed;
    }
}