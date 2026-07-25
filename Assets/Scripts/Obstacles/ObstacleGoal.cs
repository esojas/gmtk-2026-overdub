using UnityEngine;

public class ObstacleGoal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            GameEventsManager.Instance.GoalReached();
        }
    }
}
