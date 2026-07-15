using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemies;
    [SerializeField] private GameObject[] _enemySpawnPoints;

    private bool _wasTriggered = false; 

    private void OnTriggerEnter(Collider collider) 
    {
        if (collider.tag == "Player" && !_wasTriggered)
        {
            for(int i = 0; i < _enemies.Length; i++)
            {
                GameObject newEnemy = Instantiate(_enemies[i], _enemySpawnPoints[i].transform.position, Quaternion.identity);
                newEnemy.gameObject.name = _enemies[i].transform.name + i;
            }
            _wasTriggered = true;
        }
    }
}
