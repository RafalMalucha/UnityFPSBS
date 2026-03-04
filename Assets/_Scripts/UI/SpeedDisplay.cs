using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    public TextMeshProUGUI SpeedText;
    private float _currentSpeed;
    //public PlayerMovement PlayerMovement; 

    void Update()
    {
        //float currentSpeed = PlayerMovement.GetCurrentSpeed();
        SpeedText.text = $"Speed: {_currentSpeed}";
    }

    public void SetNewCurrentSpeed(float newCurrentSpeed)
    {
        _currentSpeed = newCurrentSpeed;
    }


}