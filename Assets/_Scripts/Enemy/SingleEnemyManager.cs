using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class SingleEnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private float _enemyHealth;
    //[SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyNavMesh _enemyNavMesh;

    private void Awake()
    {
        _enemyHealth = _enemyType.health;
        _enemyNavMesh = GetComponent<EnemyNavMesh>();

        StartCoroutine(WaitForNav());
    }

    IEnumerator WaitForNav()
    {
        yield return new WaitForSeconds(1.25f);
        _enemyNavMesh = GetComponent<EnemyNavMesh>();
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnHit(float baseDamage)
    {
        float damageModifier = Random.Range(0.5f, 1.5f);
        float calculatedDamage = baseDamage * damageModifier;
        //Debug.Log(transform.name+" got hit for: "+calculatedDamage);
        ModifyEnemyHealth(calculatedDamage);
    }

    public void ModifyEnemyHealth(float calculatedDamage)
    {
        _enemyHealth -= calculatedDamage;
        if (_enemyHealth <= 0)
        {
            EnemyDie();
        }
    }

    private void EnemyDie()
    {
        Destroy(gameObject);
        //Debug.Log("die");
        //GetComponentInParent<SceneEnemyManager>().UpdateListOfAliveEnemies();
    }

    public float GetEnemyHealth()
    {
        return _enemyHealth;
    }

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
}
