using System;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private SceneEnemyManager _sceneEnemyManager;

    //[SerializeField] private SpeedDisplay _speedDisplay;
    [SerializeField] private EnemiesLeftDisplay _enemiesLeftDisplay;
    [SerializeField] private UI_PlayerInfoDisplay _playerInfoDisplay;
    void Start()
    {
        GameObject player = GameObject.Find("Player(Clone)");
        _playerManager = player.GetComponent<PlayerManager>();

        GameObject sceneEnemyManger = GameObject.Find("SceneEnemyManager");
        _sceneEnemyManager = sceneEnemyManger.GetComponent<SceneEnemyManager>();

        GameObject UI_PlayerInfo = GameObject.Find("UI_PlayerInfo");
        _playerInfoDisplay = UI_PlayerInfo.GetComponent<UI_PlayerInfoDisplay>();
    }

    // Update is called once per frame
    void Update()
    {
        _playerInfoDisplay.SetNewCurrentSpeed(_playerManager.GetPlayerMovement().GetCurrentSpeed());
        _playerInfoDisplay.SetNewCurrentHealth(_playerManager.GetPlayerHealth().GetCurrentPlayerHealth());
        _enemiesLeftDisplay.SetNewEnemiesLeft(_sceneEnemyManager.GetAmountOfEnemiesAlive());
    }

    
}
