using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    [Header("Prefab to Instantiate")]
    [SerializeField] private GameObject replayObjectPrefab;

    //[Header("Camera Targeting")]
    //[SerializeField] private bool newCameraTarget = false;

    public Queue<ReplayData> recordingQueue { get; private set; }

    private List<Recording> recordings;
    private bool isDoingReplay = false;
    //private int replayPlayed = 0;

    private void Awake()
    {
        recordingQueue = new Queue<ReplayData>();
        recordings = new List<Recording>();
    }

    private void Start()
    {
        // subscribe to events
        GameEventsManager.Instance.onGoalReached += OnGoalReached;
        GameEventsManager.Instance.onRestartLevel += OnRestartLevel;
        GameEventsManager.Instance.onPlayerStartLevel += StartLevel;
    }

    private void OnDestroy()
    {
        // unsubscribe from events
        GameEventsManager.Instance.onGoalReached -= OnGoalReached;
        GameEventsManager.Instance.onRestartLevel -= OnRestartLevel;
        GameEventsManager.Instance.onPlayerStartLevel -= StartLevel;
    }

    private void OnGoalReached()
    {
        //Start next level and move ts somehwere else.
    }

    private void StartLevel()
    {
        StartReplay();
    }

    private void OnRestartLevel()
    {
        Reset();
    }

    private void Update()
    {
        if (!isDoingReplay)
        {
            return;
        }

        if (PausedControl.isPaused)
        {
            return;
        }

        bool anyRecordingHasMoreFrames = false;
        foreach (Recording recording in recordings)
        {
            if (recording.PlayNextFrame())
            {
                anyRecordingHasMoreFrames = true;
            }
        }

        // check if we're finished, so we can restart
        if (!anyRecordingHasMoreFrames)
        {
            isDoingReplay = false;
        }
    }

    public void RecordReplayFrame(ReplayData data)
    {
        recordingQueue.Enqueue(data);
    }

    private void StartReplay()
    {
        isDoingReplay = true;
        foreach (Recording recording in recordings)
        {
            recording.RestartFromBeginning();
        }

        AddRecording();

        foreach (Recording recording in recordings)
        {
            recording.InstantiateReplayObject(replayObjectPrefab);
        }

    }

    private void AddRecording()
    {
        if (recordingQueue.Count == 0)
        {
            return; // nothing was recorded, no point creating an empty Recording
        }

        // add the recording
        recordings.Add(new Recording(recordingQueue));
        // reset the current recording queue for next time
        recordingQueue.Clear();
    }

    private void RestartReplay()
    {
        isDoingReplay = true;
        foreach (Recording recording in recordings)
        {
            recording.RestartFromBeginning();

            if (recording.replayObject == null)
            {
                recording.InstantiateReplayObject(replayObjectPrefab);
            }
        }
    }

    public void Reset()
    {
        isDoingReplay = false;
        // reset the recorder to a clean slate
        recordingQueue.Clear();
        // cleanup replay objects
        foreach (Recording recording in recordings)
        {
            recording.DestroyReplayObjectIfExist();
        }
        // re-initialize the recordings
        recordings = new List<Recording>();
    }

    public void StartNewRecording()
    {
        AddRecording();
    }

}