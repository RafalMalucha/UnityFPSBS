using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class BakeNavmeshOnLevelLoad : MonoBehaviour
{
    private NavMeshSurface _navMeshSurface;
    void Awake()
    {
        _navMeshSurface = GetComponent<NavMeshSurface>();
        _navMeshSurface.BuildNavMesh();
    }


}
