using Unity.VisualScripting;
using UnityEngine;

public class MonkeyBarTrigger : MonoBehaviour
{

    private Vector3 _monkeyBarPlayerPositionOffset = new Vector3(0.25f, -1.45f, 0.5f);
    private bool _monkeyBarTriggeredNow = false;

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player" && !_monkeyBarTriggeredNow)
        {
            Debug.Log("monkeybar swing start");
            _monkeyBarTriggeredNow = true;
            PlayerManager.Instance.GetPlayerMovement().SetNewPlayerPosition(transform.position + _monkeyBarPlayerPositionOffset);
            StartCoroutine(PlayerManager.Instance.GetPlayerMovement().MonkeyBar());
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            Debug.Log("monkeybar swing exit");
            _monkeyBarTriggeredNow = false;
        }
    }
}
