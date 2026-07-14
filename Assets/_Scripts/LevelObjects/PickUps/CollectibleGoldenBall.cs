using UnityEngine;

public class CollectibleGoldenBall : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Found Golden Ball");
            PlayerManager.Instance.GetPlayerInteract().CollectibleFound();
            Destroy(transform.gameObject);
        }
    }
}
