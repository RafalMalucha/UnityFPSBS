using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class ArenaStart : MonoBehaviour
{

    [SerializeField] private GameObject[] _doors; 
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _enemySpawnPoints;
    
    private BoxCollider _boxCollider;
    private bool _wasTriggered = false;
    private bool _arenaComplete = false;
    void Awake()
    {
        _boxCollider = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_wasTriggered && SceneEnemyManager.Instance.GetAmountOfEnemiesAlive() == 0 && !_arenaComplete)
        {
            Debug.Log("dupa");
            EndArena();
            _arenaComplete = true;
        }
    }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player" && !_wasTriggered)
        {
            _wasTriggered = !_wasTriggered;
            StartArena();
        }
    }

    private void StartArena()
    {
        Debug.Log("arena start");

        foreach(GameObject door in _doors)
        {
            ButtonDoorInteract _buttonDoorInteract = door.GetComponent<ButtonDoorInteract>();
            if(_buttonDoorInteract.GetIsOpen())
            {
                _buttonDoorInteract.DoorClose();
            }  
            if(_buttonDoorInteract.GetIsActive())
            {
                _buttonDoorInteract.SetIsActive(false);
            }      
        }

        for(int i = 0; i < _enemies.Length; i++)
        {
            Instantiate(_enemies[i], _enemySpawnPoints[i].transform.position, Quaternion.identity);
        }

        SceneEnemyManager.Instance.UpdateListOfAliveEnemies();
    }

    private void EndArena()
    {
        Debug.Log("arena end");
        foreach(GameObject door in _doors)
        {
            ButtonDoorInteract _buttonDoorInteract = door.GetComponent<ButtonDoorInteract>();

            _buttonDoorInteract.DoorOpen();
            _buttonDoorInteract.SetIsActive(true);  
        }
    }
}
