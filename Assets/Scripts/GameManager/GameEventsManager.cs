using System;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager Instance { get; private set; }

    public event Action onGoalReached;

    public event Action onRestartLevel;

    public event Action<GameObject> onChangeCameraTarget;

    public event Action onPlayerRespawn;

    public event Action onPlayerStartLevel;

    private void Awake()
    {
        if (Instance != null) Debug.LogError("There is more than one Game Event manager in the scene!");
        Instance = this;
    }
    
    public void GoalReached()
    {
        if(onGoalReached!= null) onGoalReached();
    }

    public void StartLevel()
    {
        if(onPlayerStartLevel!= null) onPlayerStartLevel();
    }

    public void RestartLevel()
    {
        if(onRestartLevel!=null) onRestartLevel();
    }

    public void ChangeCameraTarget(GameObject newTarget)
    {
        if(onChangeCameraTarget!= null) onChangeCameraTarget(newTarget);
    }

    public void PlayerRespawn()
    {
        if(onPlayerRespawn!= null) onPlayerRespawn();
    }

}
