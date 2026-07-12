using UnityEngine;

public class JumpFloorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            PlayerManager.Instance.GetPlayerMovement().jumpHeight = 5f;
            PlayerManager.Instance.GetPlayerMovement().jumpDuration = 1.25f;
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            PlayerManager.Instance.GetPlayerMovement().jumpHeight = 1.5f;
            PlayerManager.Instance.GetPlayerMovement().jumpDuration = 0.65f;
        }
    }
}
