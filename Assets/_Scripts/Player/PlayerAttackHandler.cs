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

    public void SpawnRocket(Transform _rocketSpawnPoint, Vector3 _rocketTarget)
    {
        var rocket = Instantiate(
            _rocketPrefab, 
            PlayerManager.Instance.GetPlayerInventory().GetWeaponHolder().transform.position, 
            PlayerManager.Instance.GetPlayerInventory().GetWeaponHolder().transform.rotation * Quaternion.Euler(0, 90 ,0)
        );

        Instantiate(
            _hitMarker,
            _rocketTarget,
            Quaternion.identity
        ); 

        rocket.GetComponent<RocketBehavior>().SetTarget(_rocketTarget);       
        //rocket.SetTarget(_rocketTargetr);
    }
}
