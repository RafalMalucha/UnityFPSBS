using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class FlyingPeteballEnemyNav : MonoBehaviour
{
    [SerializeField] private GameObject _rocketPrefab;
    private EnemyNavMesh _enemyNavMesh;

    private void Awake() {
        _enemyNavMesh = GetComponent<EnemyNavMesh>();
    }

    public IEnumerator FlyingPeteballNav()
    {
        while (true)
        {
            //Debug.Log("Selecting new destination from PoIs " + gameObject.name);

            //_movePositionTransform = _navPointsOfInterest[Random.Range(0, _navPointsOfInterest.Length)].transform;

            float newRandomWaitTime = Random.Range(3.0f, 7.0f);
            StartCoroutine(FlyingPeteballAttackTry());

            yield return new WaitForSeconds(newRandomWaitTime);
        }
    }

    IEnumerator FlyingPeteballAttackTry()
    {
        Debug.Log("FlyingPeteball Attack Try");

        float attackChance = Random.Range(0.0f, 6.0f);
        if(attackChance >= 2.0f)
        {
            float newRandomAttackDelayTime = Random.Range(0.0f, 0.25f);
            yield return new WaitForSeconds(newRandomAttackDelayTime);
            FlyingPeteballAttack();
        }

        yield return null;
    }

    private void FlyingPeteballAttack()
    {
        Debug.Log("FlyingPeteball Attacked");

        transform.LookAt(PlayerManager.Instance.transform);

        var rocket = Instantiate(
            _rocketPrefab, 
            transform.position, 
            transform.rotation
        );
    }
}
