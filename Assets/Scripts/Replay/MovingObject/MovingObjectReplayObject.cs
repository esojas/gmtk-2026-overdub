using UnityEngine;

public class MovingObjectReplayObject : ReplayObject
{
    public override void SetDataForFrame(ReplayData data)
    {
        MovingObjectReplayData objData = (MovingObjectReplayData)data;

        this.transform.position = objData.position;

        //this.transform.localScale = objData.;

    }
}
