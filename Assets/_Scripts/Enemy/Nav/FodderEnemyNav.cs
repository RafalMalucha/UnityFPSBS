using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FodderEnemyNav : MonoBehaviour
{
    [SerializeField] private GameObject _rocketPrefab;
    private EnemyNavMesh _enemyNavMesh;
    private NavMeshAgent _navMeshAgent;

    private void Awake() {
        _enemyNavMesh = GetComponent<EnemyNavMesh>();
    }

    public IEnumerator FodderNav()
    {

        while (true)
        {
            var navPoIs = _enemyNavMesh.GetNavPointsOfInterest();

            _enemyNavMesh.SetCurrentDestination(navPoIs[Random.Range(0, navPoIs.Length)].transform);

            float newRandomWaitTime = Random.Range(3.0f, 7.0f);
            StartCoroutine(FodderAttackTry());

            yield return new WaitForSeconds(newRandomWaitTime);
        }
    }

    IEnumerator FodderAttackTry()
    {
        Debug.Log("Fodder Attack Try");

        float attackChance = Random.Range(0.0f, 6.0f);
        if(attackChance >= 3.0f)
        {
            float newRandomAttackDelayTime = Random.Range(0.0f, 0.25f);
            yield return new WaitForSeconds(newRandomAttackDelayTime);
            FodderAttack();
        }
        if(attackChance <= 2.0f)
        {
            float newRandomAttackDelayTime = Random.Range(0.0f, 0.25f);
            yield return new WaitForSeconds(newRandomAttackDelayTime);
            FodderTryForMelee();
        }

        yield return null;
    }

    private void FodderAttack()
    {
        Debug.Log("Fodder Attacked");

        var rocket = Instantiate(
            _rocketPrefab, 
            transform.position, 
            transform.rotation * Quaternion.Euler(0, 180 ,0)
        );

    }

    private void FodderTryForMelee()
    {
        _enemyNavMesh.SetCurrentDestination(PlayerManager.Instance.transform);
    }
}
