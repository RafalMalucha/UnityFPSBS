using UnityEngine;
using TMPro;

public class UI_LevelInfoDisplay : MonoBehaviour
{
    public TextMeshProUGUI EnemiesLeftText;
    public TextMeshProUGUI CollectiblesText;
    private int _enemiesLeft;
    private int _allCollectibles;

    void Update()
    {
        EnemiesLeftText.text = $"Enemies Left: {_enemiesLeft}";
        CollectiblesText.text = $"Collectibles: {PlayerManager.Instance.GetPlayerInteract().GetCollectiblesFound()} / {_allCollectibles}"; 
    }

    public void SetNewEnemiesLeft(int newEnemiesLeft)
    {
        _enemiesLeft = newEnemiesLeft;
    }

    public void SetAllCollectibles(int numOfAllCollectibles)
    {
        _allCollectibles = numOfAllCollectibles;
    }
}
