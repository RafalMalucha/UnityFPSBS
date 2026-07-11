using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            PlayerManager.Instance.GetPlayerHealth().PlayerRespawn();
        }
    }
}
