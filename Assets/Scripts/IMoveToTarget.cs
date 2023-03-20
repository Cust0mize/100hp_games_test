using UnityEngine;

public interface IEnemyMove
{
    public void Init(Vector3 targetPosition);
    public void StopMove();
    public void StartMove();
}