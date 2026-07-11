using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private float _healAmount;

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            Debug.Log("Healing for " + _healAmount);
            PlayerManager.Instance.GetPlayerHealth().HealPlayerHealth(_healAmount);
            Destroy(transform.gameObject);
        }
    }
}
