using UnityEngine;
using TMPro;

public class UI_PlayerInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI HealthText;
    private float _currentSpeed;
    private float _currentHealth;

    void Update()
    {
        SpeedText.text = $"Speed: {_currentSpeed}";
        HealthText.text = $"HP: {_currentHealth}";
    }

    public void SetNewCurrentSpeed(float newCurrentSpeed)
    {
        _currentSpeed = newCurrentSpeed;
    }

    public void SetNewCurrentHealth(float newCurrentHealth)
    {
        _currentHealth = newCurrentHealth;
    }

}
