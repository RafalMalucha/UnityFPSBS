using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavMesh : MonoBehaviour
{

    [SerializeField] private GameObject _currentDestination;
    private Transform _movePositionTransform;
    private NavMeshAgent _navMeshAgent;

    private void Awake() 
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        StartCoroutine(SetRandomDestination());
    }

    private void Update()
    {
        _navMeshAgent.destination = _movePositionTransform.position;
    }

    IEnumerator SetRandomDestination()
    {
        while (true)
        {
            Debug.Log("Selecting new destination");

            Vector3 newPosition = new Vector3(this.transform.position.x + Random.Range(-5.0f, 5.0f), 0, this.transform.position.z + Random.Range(-5.0f, 5.0f));

            if (!GameObject.Find("NavMeshDestinationPoint(Clone)"))
            {
                Instantiate(_currentDestination, newPosition, Quaternion.identity);
                GameObject navPoint = GameObject.Find("NavMeshDestinationPoint(Clone)");
                _movePositionTransform = navPoint.transform;
            } 
            else
            {
                GameObject navPoint = GameObject.Find("NavMeshDestinationPoint(Clone)");
                navPoint.transform.position = newPosition;
                _movePositionTransform = navPoint.transform;
            }

            yield return new WaitForSeconds(5f);
        }
    }

    public Transform GetCurrentDestination()
    {
        return _movePositionTransform;
    }
}
