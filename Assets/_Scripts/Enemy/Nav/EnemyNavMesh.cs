using System.Collections;
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
                FodderEnemyNav _fodderNav = GetComponent<FodderEnemyNav>();
                _fodderNav.StartCoroutine(_fodderNav.FodderNav());
                break;
            case "BigGuy":
                Debug.Log("BigGuy");
                BigGuyEnemyNav _bigGuyNav = GetComponent<BigGuyEnemyNav>();
                _bigGuyNav.StartCoroutine(_bigGuyNav.BigGuyNav());
                break;
            case "FlyingPeteball":
                Debug.Log("FlyingPeteball");
                FlyingPeteballEnemyNav _flyingPeteballNav = GetComponent<FlyingPeteballEnemyNav>();
                _flyingPeteballNav.StartCoroutine(_flyingPeteballNav.FlyingPeteballNav());
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

    public Transform GetCurrentDestination()
    {
        return _movePositionTransform;
    }

    public void SetCurrentDestination(Transform destination)
    {
        _movePositionTransform = destination;
    }

    public void SetPointsOfInterestArray(GameObject[] navPointsOfInterest)
    {
        _navPointsOfInterest = navPointsOfInterest;
    }

    public GameObject[] GetNavPointsOfInterest()
    {
        return _navPointsOfInterest;
    }
}
