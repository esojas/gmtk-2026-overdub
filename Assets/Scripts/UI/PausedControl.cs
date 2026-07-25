using UnityEngine;
using UnityEngine.InputSystem;

public class PausedControl : MonoBehaviour
{
    public static PausedControl Instance { get; private set; }

    float previousTimeScale = 1f;
    public static bool isPaused = false;
    [SerializeField] private GameObject pauseMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerControl.Instance.OnPausePressed += TogglePause;
        PlayerControl.Instance.OnUnPausedPressed += TogglePause;

        Cursor.lockState = CursorLockMode.Locked; // temp
    }

    private void OnDisable()
    {
        PlayerControl.Instance.OnPausePressed -= TogglePause;
        PlayerControl.Instance.OnUnPausedPressed -= TogglePause;
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            isPaused = true;
            PlayerControl.inputActions.FindActionMap("Player").Disable();
            PlayerControl.inputActions.FindActionMap("UI").Enable();
            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Time.timeScale == 0)
        {
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            isPaused = false;
            PlayerControl.inputActions.FindActionMap("UI").Disable();
            PlayerControl.inputActions.FindActionMap("Player").Enable();
            pauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

}
