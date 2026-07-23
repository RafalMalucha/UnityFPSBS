using UnityEngine;

public class PlayerPause : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerManager.Instance.GetPauseInputAction().WasPressedThisFrame())
        {
            PauseMenu.Instance.PauseGame();
        }
    }
}
