using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private SceneEnemyManager _sceneEnemyManager;

    [SerializeField] private SpeedDisplay _speedDisplay;
    [SerializeField] private EnemiesLeftDisplay _enemiesLeftDisplay;
    void Start()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        _playerManager = player.GetComponent<PlayerManager>();

        GameObject sceneEnemyManger = GameObject.Find("SceneEnemyManager");
        _sceneEnemyManager = sceneEnemyManger.GetComponent<SceneEnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _speedDisplay.SetNewCurrentSpeed(_playerManager.GetPlayerMovement().GetCurrentSpeed());
        _enemiesLeftDisplay.SetNewEnemiesLeft(_sceneEnemyManager.GetAmountOfEnemiesAlive());
    }

    
}
