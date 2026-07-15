using System.Collections;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [SerializeField] private SingleEnemyManager _singleEnemyManager;
    [SerializeField] private ParticleSystem _bloodSplatter;

    private void Awake()
    {
        _singleEnemyManager = transform.GetComponent<SingleEnemyManager>();
    }
    public void OnHit(float baseDamage)
    {
        _singleEnemyManager.OnHit(baseDamage);
        var splatter = Instantiate(_bloodSplatter, transform.position, transform.rotation * Quaternion.Euler(0, 180 ,0));
        //StartCoroutine(SpawnAndDestroyBloodSplatter());
    }

    IEnumerator SpawnAndDestroyBloodSplatter()
    {
        var splatter = Instantiate(_bloodSplatter, transform.position, transform.rotation * Quaternion.Euler(0, 180 ,0));
        yield return new WaitForSeconds(2);
        Destroy(splatter);
    }
}
