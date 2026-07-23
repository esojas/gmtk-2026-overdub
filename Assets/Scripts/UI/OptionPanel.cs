using UnityEngine;
using UnityEngine.UI;

public class OptionPanel : MonoBehaviour
{
    [Header("SliderParamaters")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider sFXSlider;

    [Header("ReturnParameters")]
    [SerializeField] private Button returnButton;
    [SerializeField] private GameObject pausedPanel;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sensitivitySlider.value = PlayerSettings.Sensitivity;

        returnButton.onClick.AddListener(ReturnToPausePanel);

        sensitivitySlider.onValueChanged.AddListener(OnSenitivityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSFXChange(float value)
    {

    }

    private void OnAudioChanged(float value)
    {

    }

    private void OnSenitivityChange(float value)
    {
        PlayerSettings.Sensitivity = value;
    }

    private void ReturnToPausePanel()
    {
        pausedPanel.SetActive(true);
        gameObject.SetActive(false);
    }
}
