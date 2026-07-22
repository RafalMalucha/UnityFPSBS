using UnityEngine;

public class Arena : MonoBehaviour
{
    [Header("test")]
    [SerializeField] private GameObject[] _arenaEnemies;

    [Header("possibly deprecated")]
    [SerializeField] private GameObject[] _doors; 
    [SerializeField] private GameObject[] _enemies;

    [Header("arena setup")]
    [SerializeField] private GameObject[] _enemySpawnPoints;
    [SerializeField] private GameObject[] _enemyNavPointsOfInterest;
    [SerializeField] private Transform _arenaCompleteRespawnPoint;
    
    private BoxCollider _boxCollider;
    private bool _wasTriggered = false;
    private bool _arenaComplete = false;
    void Awake()
    {
        _boxCollider = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    // void Update()
    // {
    //     if(_wasTriggered && SceneEnemyManager.Instance.GetAmountOfEnemiesAlive() == 0 && !_arenaComplete)
    //     {
    //         EndArena();
    //         _arenaComplete = true;
    //     }
    // }

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            StartArena();
        }
    }

    private void OnTriggerExit(Collider collider) 
    {
        if (collider.tag == "Player")
        {
            SoundscapeManager.Instance.PlaySoundscape(0);
            PlayerManager.Instance.GetPlayerHealth().SetCurrentRespawnPoint(_arenaCompleteRespawnPoint);
        }
    }

    private void StartArena()
    {
        for(int i = 0; i < _arenaEnemies.Length; i++)
        {
            _arenaEnemies[i].SetActive(true);
            _arenaEnemies[i].GetComponent<EnemyNavMesh>().SetPointsOfInterestArray(_enemyNavPointsOfInterest);
        }

        SceneEnemyManager.Instance.UpdateListOfAliveEnemies();
        SoundscapeManager.Instance.PlaySoundscape((SoundscapeManager.SoundscapeType)1);
    }
}
