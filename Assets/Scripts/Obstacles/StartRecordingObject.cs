using UnityEngine;

public class StartRecordingObject : MonoBehaviour
{

    public static bool startRecording;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7) // player
        {
            startRecording = true;
            GameEventsManager.Instance.StartLevel();
        }
    }


    private void Start()
    {
        startRecording = false;
    }
}
