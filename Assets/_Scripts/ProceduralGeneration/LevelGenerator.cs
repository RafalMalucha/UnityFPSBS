using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridManager _grid;
    [SerializeField] private Pathfinder _pathfinder;

    [Header("Rooms")]
    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private GameObject _testRoom;

    [Header("Path")]
    [SerializeField] private List<Node> _path;

    private void OnValidate() 
    {
        foreach(Transform child in transform)
        {
            UnityEditor.EditorApplication.delayCall+=()=>
            {
                UnityEditor.Undo.DestroyObjectImmediate(child.gameObject);
            };
        }

        _path = _pathfinder.GetCurrentPath();
        BuildLevel();
    }

    public void BuildLevel()
    {
        foreach(Node node in _path)
        {
            GameObject room = Instantiate(_testRoom, new Vector3(node.WorldPosition.x, 0f, node.WorldPosition.z), Quaternion.identity);
            room.name = "room_" + node.GridPosition.x + "_" + node.GridPosition.y;
            room.transform.SetParent(this.transform);
        }
    }

    public void SetNewPath(List<Node> newPath)
    {
        _path = newPath;
    }
}
