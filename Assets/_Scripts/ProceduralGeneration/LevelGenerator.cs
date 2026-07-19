using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridManager _grid;
    [SerializeField] private Pathfinder _pathfinder;

    [Header("Rooms")]
    [SerializeField] private GameObject[] _corridorRooms;
    [SerializeField] private GameObject _testRoom;
    [SerializeField] private GameObject _entryAndExitRoom;
    [SerializeField] private GameObject _cornerRoom;
    [SerializeField] private GameObject _longRoom;
    [SerializeField] private GameObject _arenaRoom;

    [Header("Path")]
    [SerializeField] private List<Node> _path;

    private List<Node> _allOccupiedNodes = new List<Node>();

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
        _allOccupiedNodes = new List<Node>();
        GenerateLevel();
    }

    public List<RoomType> GenerateLevel()
    {
        List<RoomType> generatedRoomTypes = new List<RoomType>();
        _allOccupiedNodes = new List<Node>();

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
            _allOccupiedNodes.Add(_path[i]);
        }

        ReplaceLongStraights(generatedRoomTypes, _path);

        return generatedRoomTypes;
    }

    public void ReplaceLongStraights(List<RoomType> generatedRoomTypes, List<Node> _path)
    {
        for(int i = 1; i < generatedRoomTypes.Count - 1; i++)
        {
            if(generatedRoomTypes[i - 1] == RoomType.Straight && generatedRoomTypes[i] == RoomType.Straight && generatedRoomTypes[i + 1] == RoomType.Straight)
            {
                int random = Random.Range(0, 2);

                generatedRoomTypes[i - 1] = RoomType.None;
                generatedRoomTypes[i + 1] = RoomType.None;

                if(random != 0)
                {
                    generatedRoomTypes[i] = RoomType.Straight_Long;
                } 
                else
                {
                    if(CheckIfArenaAbleToSpawn(_path[i - 1], _path[i]))
                    {
                        generatedRoomTypes[i] = RoomType.Arena;
                        ReserveNodesForArena(_path[i - 1], _path[i]);
                    } else
                    {
                        generatedRoomTypes[i] = RoomType.Straight_Long;
                    }
                }
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

                    int randomRoom = Random.Range(0, _corridorRooms.Length);

                    Quaternion straitghtRoomRotation = GetRoomRotation(_path[i - 1], _path[i], _path[i + 1]);
                    GameObject room = Instantiate(_corridorRooms[randomRoom], pos, straitghtRoomRotation);
                    room.name = "Room_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    room.transform.SetParent(this.transform);
                    break;

                case RoomType.Straight_Long:
                    Quaternion longStraightRotation = GetRoomRotation(_path[i - 1], _path[i], _path[i + 1]);

                    GameObject longRoom = Instantiate(_longRoom, pos, longStraightRotation);
                    longRoom.name = "LongRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    longRoom.transform.SetParent(this.transform);
                    break;

                case RoomType.Arena:
                    GameObject arenaRoom = Instantiate(_arenaRoom, pos, Quaternion.identity);
                    arenaRoom.name = "ArenaRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    arenaRoom.transform.SetParent(this.transform);
                    break;

                default:
                    Debug.Log("no matching room type");
                    break;
            }
        }
        Debug.Log("_path.Count");
        Debug.Log(_path.Count);
        Debug.Log("_allOccupiedNodes.Count");
        Debug.Log(_allOccupiedNodes.Count);
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

    private bool CheckIfArenaAbleToSpawn(Node prevNode, Node potentialSpawnNode)
    {
        if(prevNode.GridPosition.x == potentialSpawnNode.GridPosition.x)
        {
            bool ableToSpawn = (
                potentialSpawnNode.GridPosition.x - 1 >= 0 && 
                potentialSpawnNode.GridPosition.y + 1 < _grid.GetGridSizeZ() &&
                potentialSpawnNode.GridPosition.y - 1 >= 0 &&
                potentialSpawnNode.GridPosition.x + 1 < _grid.GetGridSizeX()
            );

            return ableToSpawn;
        } 
        if(prevNode.GridPosition.x == potentialSpawnNode.GridPosition.x)
        {
            bool ableToSpawn = (
                potentialSpawnNode.GridPosition.x - 1 >= 0 && 
                potentialSpawnNode.GridPosition.y + 1 < _grid.GetGridSizeZ() &&
                potentialSpawnNode.GridPosition.y - 1 >= 0 &&
                potentialSpawnNode.GridPosition.x + 1 < _grid.GetGridSizeX()
            );

            return ableToSpawn;
        } 
        return false;
    }

    private void ReserveNodesForArena(Node prevNode, Node arenaSpawnNode)
    {
        if(prevNode.GridPosition.x == arenaSpawnNode.GridPosition.x)
        {
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x - 1, arenaSpawnNode.GridPosition.y + 1));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x - 1, arenaSpawnNode.GridPosition.y));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x - 1, arenaSpawnNode.GridPosition.y - 1));

            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x + 1, arenaSpawnNode.GridPosition.y + 1));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x + 1, arenaSpawnNode.GridPosition.y));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x + 1, arenaSpawnNode.GridPosition.y - 1));
        } 
        if(prevNode.GridPosition.y == arenaSpawnNode.GridPosition.y)
        {
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x - 1, arenaSpawnNode.GridPosition.y + 1));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x, arenaSpawnNode.GridPosition.y + 1));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x + 1, arenaSpawnNode.GridPosition.y + 1));

            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x - 1, arenaSpawnNode.GridPosition.y - 1));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x, arenaSpawnNode.GridPosition.y - 1));
            _allOccupiedNodes.Add(_grid.GetNode(arenaSpawnNode.GridPosition.x + 1, arenaSpawnNode.GridPosition.y - 1));
        } 
    }

    public void SetNewPath(List<Node> newPath)
    {
        _path = newPath;
    }
}
