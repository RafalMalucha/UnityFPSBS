using UnityEngine;
using TMPro;

public class EnemyInformationManager : MonoBehaviour
{
    [SerializeField] private SingleEnemyManager _singleEnemyManager;
    [SerializeField] private EnemyNavMesh _enemyNavMesh;
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI enemyCurrentDestinationText;
    public TextMeshProUGUI enemyCurrentHealthText;

    private void Awake() { 
        enemyNameText.text = $"{transform.parent.gameObject.name}";
    }

    void Update()
    {
        Transform currentDestination = _enemyNavMesh.GetCurrentDestination();
        enemyCurrentDestinationText.text = $"{currentDestination.position}";
        enemyCurrentHealthText.text = $"{_singleEnemyManager.GetEnemyHealth()}";
    }
}
