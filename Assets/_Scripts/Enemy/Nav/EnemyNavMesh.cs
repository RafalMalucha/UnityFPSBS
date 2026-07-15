using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{

    [SerializeField] private GameObject _currentDestination;
    [SerializeField] private GameObject _rocketPrefab;
    private GameObject[] _navPointsOfInterest;
    private Transform _movePositionTransform;
    private NavMeshAgent _navMeshAgent;

    private void Awake() 
    {
        //_navCombatPointsOfInterest.
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _movePositionTransform = transform;
    }

    private void Start()
    {
        //StartCoroutine(SetRandomDestination());

        switch (GetComponent<SingleEnemyManager>().GetEnemyType().name)
        {
            case "Fodder":
                Debug.Log("Fodder");
                StartCoroutine(FodderEnemyNav());
                break;
            case "BigGuy":
                Debug.Log("BigGuy");
                StartCoroutine(BigGuyEnemyNav());
                break;
            case "FlyingPeteball":
                Debug.Log("FlyingPeteball");
                StartCoroutine(FlyingPeteballEnemyNav());
                break;
            default:
                Debug.Log("no matching enemy type");
                StartCoroutine(SetRandomDestination());
                break;
        }
    }

    private void Update()
    {
        _navMeshAgent.destination = _movePositionTransform.position;
    }

    IEnumerator SetRandomDestination()
    {
        while (true)
        {
            Debug.Log("Selecting new destination " + gameObject.name);

            Vector3 newPosition = new Vector3(this.transform.position.x + Random.Range(-5.0f, 5.0f), 0, this.transform.position.z + Random.Range(-5.0f, 5.0f));

            _currentDestination.transform.position = newPosition;
            _currentDestination.transform.name = "Target_"+transform.name;

            if (!GameObject.Find("Target_"+transform.name+"(Clone)"))
            {
                Instantiate(_currentDestination, newPosition, Quaternion.identity);
                GameObject navPoint = GameObject.Find("Target_"+transform.name+"(Clone)");
                _movePositionTransform = navPoint.transform;
            } 
            else
            {
                GameObject navPoint = GameObject.Find("Target_"+transform.name+"(Clone)");
                navPoint.transform.position = newPosition;
                _movePositionTransform = navPoint.transform;
            }

            float newRandomWaitTime = Random.Range(5.0f, 20.0f);

            yield return new WaitForSeconds(newRandomWaitTime);
        }
    }

    IEnumerator FodderEnemyNav()
    {
        while (true)
        {
            //Debug.Log("Selecting new destination from PoIs " + gameObject.name);

            _movePositionTransform = _navPointsOfInterest[Random.Range(0, _navPointsOfInterest.Length)].transform;

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
        _movePositionTransform = PlayerManager.Instance.transform;
    }

    IEnumerator FlyingPeteballEnemyNav()
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

    IEnumerator BigGuyEnemyNav()
    {
        while (true)
        {
            //Debug.Log("Selecting new destination from PoIs " + gameObject.name);

            _movePositionTransform = _navPointsOfInterest[Random.Range(0, _navPointsOfInterest.Length)].transform;

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
        _movePositionTransform = PlayerManager.Instance.transform;
    }

    public Transform GetCurrentDestination()
    {
        return _movePositionTransform;
    }

    public void SetPointsOfInterestArray(GameObject[] navPointsOfInterest)
    {
        _navPointsOfInterest = navPointsOfInterest;
    }
}
