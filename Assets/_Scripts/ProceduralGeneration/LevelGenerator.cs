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
    [SerializeField] private GameObject _longRoom;

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
        GenerateLevel();
    }

    public List<RoomType> GenerateLevel()
    {
        List<RoomType> generatedRoomTypes = new List<RoomType>();

        for(int i = 0; i < _path.Count; i++)
        {
            if(i == 0 || i == _path.Count - 1)
            {
                generatedRoomTypes.Add(RoomType.Entry_Exit);
            }
            else
            {
                RoomType type = GetRoomType(_path[i - 1], _path[i], _path[i + 1]);

                if(type == RoomType.Straight)
                {
                    generatedRoomTypes.Add(RoomType.Straight);
                }
                if(type == RoomType.Corner)
                {
                    generatedRoomTypes.Add(RoomType.Corner);
                }

            }
        }

        ReplaceLongStraights(generatedRoomTypes, _path);

        return generatedRoomTypes;
    }

    public void ReplaceLongStraights(List<RoomType> generatedRoomTypes, List<Node> _path)
    {
        for(int i = 1; i < generatedRoomTypes.Count - 1; i++)
        {
            Debug.Log(generatedRoomTypes[i]);

            if(generatedRoomTypes[i - 1] == RoomType.Straight && generatedRoomTypes[i] == RoomType.Straight && generatedRoomTypes[i + 1] == RoomType.Straight)
            {
                generatedRoomTypes[i - 1] = RoomType.None;
                generatedRoomTypes[i] = RoomType.Straight_Long;
                generatedRoomTypes[i + 1] = RoomType.None;
            } 
        }

        BuildLevel(generatedRoomTypes);
    }

    public void BuildLevel(List<RoomType> generatedRoomTypes)
    {
        for(int i = 0; i < generatedRoomTypes.Count; i++)
        {
            var pos = new Vector3(
                this.transform.position.x + (_path[i].GridPosition.x * _grid.GetCellSize()), 
                this.transform.position.y, 
                this.transform.position.z + (_path[i].GridPosition.y * _grid.GetCellSize())
            );

            switch (generatedRoomTypes[i])
            {
                case RoomType.Entry_Exit:
                    GameObject entryRoom = Instantiate(_entryAndExitRoom, pos, Quaternion.identity);
                    entryRoom.name = "entryAndExitRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    entryRoom.transform.SetParent(this.transform);
                    break;

                case RoomType.Corner:
                    GameObject cornerRoom = Instantiate(_entryAndExitRoom, pos, Quaternion.identity);
                    cornerRoom.name = "CornerRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    cornerRoom.transform.SetParent(this.transform);
                    break;

                case RoomType.Straight:
                    Quaternion straitghtRoomRotation = GetRoomRotation(_path[i - 1], _path[i], _path[i + 1]);
                    GameObject room = Instantiate(_testRoom, pos, straitghtRoomRotation);
                    room.name = "Room_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    room.transform.SetParent(this.transform);
                    break;

                case RoomType.Straight_Long:
                    Quaternion longStraightRotation = GetRoomRotation(_path[i - 1], _path[i], _path[i + 1]);
                    GameObject longRoom = Instantiate(_longRoom, pos, longStraightRotation);
                    longRoom.name = "LongRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    longRoom.transform.SetParent(this.transform);
                    break;

                default:
                    Debug.Log("no matching room type");
                    break;
            }
        }
    }

    private RoomType GetRoomType(Node previous, Node current, Node next)
    {
        bool horizontal = (previous.GridPosition.y == current.GridPosition.y) && (current.GridPosition.y == next.GridPosition.y);

        bool vertical = (previous.GridPosition.x == current.GridPosition.x) && (current.GridPosition.x == next.GridPosition.x);

        if(horizontal || vertical)
        {
            return RoomType.Straight;
        }
        return RoomType.Corner;
    }

    private Quaternion GetRoomRotation(Node previous, Node current, Node next)
    {
        bool horizontal = (previous.GridPosition.y == current.GridPosition.y) && (current.GridPosition.y == next.GridPosition.y);

        if (horizontal)
        {
            return Quaternion.Euler(0, 90, 0);
        }
        return Quaternion.identity;
    }

    public void SetNewPath(List<Node> newPath)
    {
        _path = newPath;
    }
}
