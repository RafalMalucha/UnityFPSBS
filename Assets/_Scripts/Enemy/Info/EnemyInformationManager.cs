using UnityEngine;
using TMPro;
using System.Numerics;

public class EnemyInformationManager : MonoBehaviour
{
    [SerializeField] private EnemyNavMesh _enemyNavMesh;
    public TextMeshProUGUI enemyNameText;
    public TextMeshProUGUI enemyCurrentDestinationText;

    private void Awake() { 
        enemyNameText.text = $"{transform.root.gameObject.name}";
    }

    void Update()
    {
        Transform currentDestination = _enemyNavMesh.GetCurrentDestination();
        enemyCurrentDestinationText.text = $"{currentDestination.position}";
    }
}
