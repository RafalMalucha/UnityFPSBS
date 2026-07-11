using UnityEngine;

public class DamagingFloorTrigger : MonoBehaviour
{

    [SerializeField] private float _damageTickCooldown = 0.25f;
    private static float _lastdamageTickTime = 0f;

    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            Debug.Log("start burning");
            _lastdamageTickTime = Time.time;
            PlayerManager.Instance.GetPlayerHealth().DamagePlayerHealth(5f);
            AudioManager.Instance.PlaySound(0);
        }
    }

    private void OnTriggerStay(Collider collider) 
    {
        if (collider.tag == "Player" && Time.time >= _lastdamageTickTime + _damageTickCooldown)
        {
            Debug.Log("keep burning");
            PlayerManager.Instance.GetPlayerHealth().DamagePlayerHealth(5f);
            AudioManager.Instance.PlaySound(0);
            _lastdamageTickTime = Time.time;
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            Debug.Log("stop burning");
            AudioManager.Instance.PlaySound(0);
        }
    }
}
