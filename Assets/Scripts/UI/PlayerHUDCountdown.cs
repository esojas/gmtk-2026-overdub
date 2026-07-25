using TMPro;
using UnityEngine;

public class PlayerHUDCountdown : MonoBehaviour
{
    private TextMeshProUGUI m_TextMeshProUGUI;

    [SerializeField] private PlayerDeath playerDeathScript;

    private float maxTimeRemaining;

    private void Start()
    {
        m_TextMeshProUGUI = GetComponent<TextMeshProUGUI>();

        if (playerDeathScript != null)
        {
            maxTimeRemaining = playerDeathScript.timeRemaining;
        }
    }

    void Update()
    {
        if (playerDeathScript != null && m_TextMeshProUGUI != null)
        {
            m_TextMeshProUGUI.text = playerDeathScript.timeRemaining.ToString("F1");
        }
    }
}
