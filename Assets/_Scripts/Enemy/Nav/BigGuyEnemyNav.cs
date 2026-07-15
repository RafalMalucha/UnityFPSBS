using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BigGuyEnemyNav : MonoBehaviour
{
    private EnemyNavMesh _enemyNavMesh;
    private NavMeshAgent _navMeshAgent;

    private void Awake() {
        _enemyNavMesh = GetComponent<EnemyNavMesh>();
    }

    public IEnumerator BigGuyNav()
    {
        while (true)
        {
            var navPoIs = _enemyNavMesh.GetNavPointsOfInterest();

            _enemyNavMesh.SetCurrentDestination(navPoIs[Random.Range(0, navPoIs.Length)].transform);

            float newRandomWaitTime = Random.Range(5.0f, 12.0f);
            StartCoroutine(BigGuyAttackTry());

            yield return new WaitForSeconds(newRandomWaitTime);
        }
    }

    IEnumerator BigGuyAttackTry()
    {
        Debug.Log("BigGuy Attack Try");

        float attackChance = Random.Range(0.0f, 6.0f);
        // if(attackChance >= 3.0f)
        // {
        //     float newRandomAttackDelayTime = Random.Range(0.0f, 0.25f);
        //     yield return new WaitForSeconds(newRandomAttackDelayTime);
        //     FodderAttack();
        // }
        if(attackChance >= 2.0f)
        {
            float newRandomAttackDelayTime = Random.Range(0.0f, 0.25f);
            yield return new WaitForSeconds(newRandomAttackDelayTime);
            BigGuyTryForMelee();
        }

        yield return null;
    }

    public void BigGuyTryForMelee()
    {
        _enemyNavMesh.SetCurrentDestination(PlayerManager.Instance.transform);
    }
}
