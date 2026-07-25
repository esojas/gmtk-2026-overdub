using UnityEngine;

public class HarmfulObject : MonoBehaviour
{
    [SerializeField] private bool isMoving = false;
    [SerializeField] private Transform destination;
    [SerializeField] private float speed = 1;

    private float t;

    private Vector3 originPos;

    private Recorder recorder;

    private Renderer rendererObject;
    private Collider colliderObject;

    private void Awake()
    {
        recorder = GetComponent<Recorder>();

        rendererObject = GetComponent<Renderer>();
        colliderObject = GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        ReplayData data = new MovingObjectReplayData(this.transform.position, this.destination.position, this.t);
        recorder.RecordReplayFrame(data);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originPos = transform.position;

        GameEventsManager.Instance.onGoalReached += OnGoalReached;
        GameEventsManager.Instance.onRestartLevel += OnRestartLevel;
        GameEventsManager.Instance.onPlayerRespawn += OnPlayerRespawn;
    }

    private void OnDestroy()
    {
        GameEventsManager.Instance.onGoalReached -= OnGoalReached;
        GameEventsManager.Instance.onRestartLevel -= OnRestartLevel;
        GameEventsManager.Instance.onPlayerRespawn -= OnPlayerRespawn;
    }

    private void OnGoalReached()
    {
        rendererObject.enabled = false;
        colliderObject.enabled = false;
    }

    private void OnRestartLevel()
    {
        rendererObject.enabled = true;
        colliderObject.enabled = true;
    }

    private void OnPlayerRespawn()
    {
        recorder.Reset();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            t = Mathf.PingPong(Time.time * speed, 1.0f);

            this.transform.position = Vector3.Lerp(originPos, destination.position, t);
        }
    }
}
