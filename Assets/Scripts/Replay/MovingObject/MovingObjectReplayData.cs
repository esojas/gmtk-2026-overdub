using UnityEngine;

public class MovingObjectReplayData : ReplayData
{
    public Vector3 targetPos { get; private set; }

    public MovingObjectReplayData(Vector3 position, Vector3 targetPos, float t)
    {
        this.position = position;
    }
}
