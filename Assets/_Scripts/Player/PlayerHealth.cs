using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    private float _currentPlayerHealth;
    [SerializeField] private float _maxPlayerHealth;
    void Start()
    {
        _currentPlayerHealth = _maxPlayerHealth;
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
        }   
    }
}
