using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeedDisplay : MonoBehaviour
{
    public TextMeshProUGUI SpeedText;
    public PlayerMovement PlayerMovement; 

    void Update()
    {
        if (PlayerMovement != null && SpeedText != null)
        {
            float currentSpeed = PlayerMovement.GetCurrentSpeed();
            SpeedText.text = $"Speed: {currentSpeed}";
        }
    }
}