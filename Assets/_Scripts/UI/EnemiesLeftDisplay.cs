using UnityEngine;
using TMPro;

public class EnemiesLeftDisplay : MonoBehaviour
{
    public TextMeshProUGUI EnemiesLeftText;
    private int _enemiesLeft;

    void Update()
    {
        EnemiesLeftText.text = $"Enemies Left: {_enemiesLeft}";
    }

    public void SetNewEnemiesLeft(int newEnemiesLeft)
    {
        _enemiesLeft = newEnemiesLeft;
    }


}