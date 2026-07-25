using UnityEngine;

public class StartRecordingObject : MonoBehaviour
{

    public static bool startRecording;

    public static bool hasCollided;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7 && !hasCollided) // player
        {
            hasCollided = true;
            startRecording = true;
            GameEventsManager.Instance.StartLevel();
            StartCoroutine(ReplayCollisionUtility.SuppressCloneCollisionsBriefly());
        }
    }


    private void Start()
    {
        startRecording = false;
        hasCollided = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
