using UnityEngine;

public class PlayerAttackHandler : MonoBehaviour
{
    //Singleton
    public static PlayerAttackHandler Instance;

    [SerializeField]    private GameObject _rocketPrefab;
    [SerializeField]    private GameObject _hitMarker;

    private void Awake() 
    {
        Instance = this; 
    }
    
    public void SpawnRocket()
    {
        var rocket = Instantiate(
            _rocketPrefab, 
            PlayerManager.Instance.GetPlayerInventory().GetWeaponHolder().GetComponentInChildren<T00bBehavior>().GetRocketSpawnPoint().position, 
            PlayerManager.Instance.GetPlayerInventory().GetWeaponHolder().transform.rotation * Quaternion.Euler(0, 90 ,0)
        );
    }
}
