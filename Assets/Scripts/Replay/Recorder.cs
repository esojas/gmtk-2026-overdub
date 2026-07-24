using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour 
{
    public Queue<ReplayData> recordingQueue { get; private set; }

    private void Awake()
    {
        recordingQueue = new Queue<ReplayData>();
    }

    public void RecordReplayFrame(ReplayData data)
    {
        recordingQueue.Enqueue(data);
        //Debug.Log("Recorded Data: " +  data.position);
    }

}
