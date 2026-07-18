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
    [SerializeField] private GameObject _entryAndExitRoom;
    [SerializeField] private GameObject _cornerRoom;

    [Header("Path")]
    [SerializeField] private List<Node> _path;

    private void Awake()
    {
        _grid = GetComponent<GridManager>();
        _pathfinder = GetComponent<Pathfinder>();
    }

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
        for(int i = 0; i < _path.Count; i++)
        {
            var pos = new Vector3(
                this.transform.position.x + (_path[i].GridPosition.x * _grid.GetCellSize()), 
                this.transform.position.y, 
                this.transform.position.z + (_path[i].GridPosition.y * _grid.GetCellSize())
            );

            if(i == 0 || i == _path.Count - 1)
            {
                GameObject entryRoom = Instantiate(_entryAndExitRoom, pos, Quaternion.identity);
                entryRoom.name = "entryAndExitRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                entryRoom.transform.SetParent(this.transform);
            }
            else
            {
                if((_path[i - 1].GridPosition.y ==_path[i].GridPosition.y) && (_path[i].GridPosition.y == _path[i + 1].GridPosition.y))
                {
                    GameObject room = Instantiate(_testRoom, pos, Quaternion.identity * Quaternion.Euler(0, 90, 0));
                    room.name = "room_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    room.transform.SetParent(this.transform);
                } 
                else
                {
                    if((_path[i - 1].GridPosition.x ==_path[i].GridPosition.x) && (_path[i].GridPosition.x == _path[i + 1].GridPosition.x))
                    {
                        GameObject room = Instantiate(_testRoom, pos, Quaternion.identity);
                        room.name = "room_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                        room.transform.SetParent(this.transform);
                    } 
                    else
                    {
                        GameObject entryRoom = Instantiate(_entryAndExitRoom, pos, Quaternion.identity);
                        entryRoom.name = "entryAndExitRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                        entryRoom.transform.SetParent(this.transform);
                    }
                }
            }
        }
    }

    public void SetNewPath(List<Node> newPath)
    {
        _path = newPath;
    }
}
