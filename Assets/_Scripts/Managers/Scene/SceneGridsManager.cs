using Unity.VisualScripting;
using UnityEngine;

public class SceneGridsManager : MonoBehaviour
{
    [SerializeField] private GridManager[] _gridsInScene;
    void Awake()
    {
        _gridsInScene = GetComponentsInChildren<GridManager>();
    }

    void Start()
    {
        foreach(GridManager gridManager in _gridsInScene)
        {
            gridManager.StartGridBuild();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
