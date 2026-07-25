using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausedPanel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button resumeButton;

    [SerializeField] private Button restartButton;

    [SerializeField] private Button optionButton;

    [SerializeField] private Button mainMenuButton;
    [Header("OptionPanel")]
    [SerializeField] private GameObject optionPanelGameobject;

    private void ResumeGame()
    {
        PausedControl.Instance.TogglePause();
    }

    private void RestartLevelGame()
    {
        GameEventsManager.Instance.RestartLevel();
        PausedControl.Instance.TogglePause();
        SceneManager.LoadScene("Level1");
    }

    private void OptionPanel()
    {
        optionPanelGameobject.SetActive(true);
        gameObject.SetActive(false);
    }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resumeButton.onClick.AddListener(ResumeGame);    
        restartButton.onClick.AddListener(RestartLevelGame);
        optionButton.onClick.AddListener(OptionPanel);
        mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
