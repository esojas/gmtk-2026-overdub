using UnityEngine;

public class MovingObjectReplayObject : ReplayObject
{
    public override void SetDataForFrame(ReplayData data)
    {
        MovingObjectReplayData objData = (MovingObjectReplayData)data;

        this.transform.position = objData.position;

        //this.transform.localScale = objData.;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            Invoke("DestroyHarmfulObject", .5f);
        }
    }

    private void DestroyHarmfulObject()
    {
        Destroy(this.gameObject);
    }
}
