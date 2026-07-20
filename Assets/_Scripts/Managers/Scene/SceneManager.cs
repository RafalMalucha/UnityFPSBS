using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance;
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerSpawnPoint;
    [SerializeField] private SceneGridsManager _gridsManager;
    [SerializeField] private SceneEnemyManager _sceneEnemyManager;
    [SerializeField] private SoundscapeManager _soundscapeManager;
    [SerializeField] private GameObject[] _sceneCollectibleSecrets;
    
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

    public int GetNumberOfAllCollectibles()
    {
        return _sceneCollectibleSecrets.Length;
    }
}
