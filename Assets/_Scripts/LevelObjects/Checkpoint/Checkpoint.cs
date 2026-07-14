using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private GameObject _checkpointNewRespawnPoint;
    private bool _wasTriggered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player" && !_wasTriggered)
        {
            _wasTriggered = !_wasTriggered;
            PlayerManager.Instance.GetPlayerHealth().SetCurrentRespawnPoint(_checkpointNewRespawnPoint.transform);
        }
    }
}
