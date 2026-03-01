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
        _movePositionTransform = transform;
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
            Debug.Log("Selecting new destination " + transform.name);

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
}
