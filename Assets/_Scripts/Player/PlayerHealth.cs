using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private float _currentPlayerHealth;
    private Transform _currentRespawnPoint;

    [SerializeField] private float _maxPlayerHealth;
    void Start()
    {
        _currentPlayerHealth = _maxPlayerHealth;
        _currentRespawnPoint = SceneManager.Instance.GetPlayerSpawnPoint();
    }

    void Update()
    {
        
    }

    public float GetCurrentPlayerHealth()
    {
        return _currentPlayerHealth;
    }

    public void HealPlayerHealth(float healAmount)
    {
        if(_currentPlayerHealth + healAmount > _maxPlayerHealth)
        {
            _currentPlayerHealth = _maxPlayerHealth;
        } 
        else
        {
            _currentPlayerHealth += healAmount;
        }
    }

    public void DamagePlayerHealth(float damageAmount)
    {
        _currentPlayerHealth -= damageAmount;
        if(_currentPlayerHealth <= 0.0f)
        {
            //die here
            Debug.Log("player died");
            PlayerRespawn();
        }   
    }

    public void PlayerDie()
    {
        Debug.Log("dead");
        //Destroy(GameObject.Find("Player(Clone)"));
    }

    public void PlayerRespawn()
    {
        Debug.Log("respawn");
        PlayerManager.Instance.GetPlayerMovement().SetNewPlayerPosition(_currentRespawnPoint);
        HealPlayerHealth(999f);
    }

    public void SetCurrentRespawnPoint(Transform newRespawnPoint)
    {
        _currentRespawnPoint = newRespawnPoint;
    }
}
