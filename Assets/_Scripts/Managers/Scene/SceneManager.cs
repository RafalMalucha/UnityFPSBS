using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public static SceneManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private SceneEnemyManager _sceneEnemyManager;
    [SerializeField] private SoundscapeManager _soundscapeManager;
    
    private void Awake()
    {
        Instance = this;
        Instantiate(_player, _playerSpawnPoint.position, Quaternion.identity);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform GetPlayerSpawnPoint()
    {
        return _playerSpawnPoint;
    }
}
