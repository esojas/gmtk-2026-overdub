using System.Collections.Generic;
using UnityEngine;

public class Recording 
{
    public ReplayObject replayObject { get; private set; }

    private Queue<ReplayData> originalQueue;

    private Queue<ReplayData> replayQueue; //mebe in future if game goes well improve this into only one queue instead of two

    public Recording(Queue<ReplayData> recordingQueue)
    {
        this.originalQueue = new Queue<ReplayData>(recordingQueue);
        this.replayQueue = new Queue<ReplayData>(recordingQueue);
    }

    public void RestartFromBeginning()
    {
        this.replayQueue = new Queue<ReplayData>(originalQueue);
    }

    public bool PlayNextFrame()
    {
        if(replayObject == null)
        {
            return false;
        }
        bool hasMoreFrames = false;
        if(replayQueue.Count != 0)
        {
            ReplayData data = replayQueue.Dequeue();
            replayObject.SetDataForFrame(data);
            hasMoreFrames = true;
            //Debug.Log($"Replay object is moving in data: {data.position}");
        }
        return hasMoreFrames;
    }

    public void InstantiateReplayObject(GameObject replayObjectPrefab)
    {
        if (replayObject != null)
        {
            return;
        }

        if (replayQueue.Count != 0)
        {
            ReplayData startingData = replayQueue.Peek();
            this.replayObject = Object.Instantiate(replayObjectPrefab, startingData.position, Quaternion.identity).GetComponent<ReplayObject>();

        }
    }


    public void DestroyReplayObjectIfExist()
    {
        if (replayObject != null)
        {
            Object.Destroy(replayObject.gameObject);
        }
    }
}
