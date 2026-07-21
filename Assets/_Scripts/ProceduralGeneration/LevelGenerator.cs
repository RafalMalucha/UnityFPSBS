using System.Collections.Generic;
using System.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


//[ExecuteInEditMode]
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
    [SerializeField] private GameObject _twoByTwo;
    [SerializeField] private GameObject[] _arenaRooms;

    [Header("Path")]
    [SerializeField] private List<Node> _path = new List<Node>();

    [Header("Damage Floor")]
    [SerializeField] private GameObject _damageFloor;

    [Header("Death Trigger")]
    [SerializeField] private GameObject _deathTrigger;

    private List<Node> _allOccupiedNodes = new List<Node>();

    private void Awake()
    {
        _grid = GetComponent<GridManager>();
        _pathfinder = GetComponent<Pathfinder>();
        //_grid.GetNavSurface().BuildNavMesh();
    }

    private void Start()
    {
        //_grid.GetNavSurface().BuildNavMesh();
        _path = _pathfinder.GetCurrentPath();
        _allOccupiedNodes = new List<Node>();
        //GenerateLevel();
    }

    // private void OnValidate() 
    // {
    //     // #if UNITY_EDITOR
    //     // foreach(Transform child in transform)
    //     // {
    //     //     UnityEditor.EditorApplication.delayCall+=()=>
    //     //     {
    //     //         UnityEditor.Undo.DestroyObjectImmediate(child.gameObject);
    //     //     };
    //     // }
    //     // #endif
    //     _path = _pathfinder.GetCurrentPath();
    //     _allOccupiedNodes = new List<Node>();
    //     GenerateLevel();
    //     _grid.GetNavSurface().BuildNavMesh();
    // }

    public List<RoomType> GenerateLevel()
    {
        List<RoomType> generatedRoomTypes = new List<RoomType>();
        _allOccupiedNodes = new List<Node>();
        _path = _pathfinder.GetCurrentPath();

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

        ReplaceRepetition(generatedRoomTypes, _path);

        return generatedRoomTypes;
    }

    public void ReplaceRepetition(List<RoomType> generatedRoomTypes, List<Node> _path)
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
            if(generatedRoomTypes[i - 1] == RoomType.Corner && generatedRoomTypes[i] == RoomType.Corner && generatedRoomTypes[i + 1] == RoomType.Corner)
            {
                generatedRoomTypes[i - 1] = RoomType.None;
                generatedRoomTypes[i] = RoomType.TwoByTwo;
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

                    int randomCorridorRoom = Random.Range(0, _corridorRooms.Length);

                    Quaternion straitghtRoomRotation = GetRoomRotation(_path[i - 1], _path[i], _path[i + 1]);
                    GameObject room = Instantiate(_corridorRooms[randomCorridorRoom], pos, straitghtRoomRotation);
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
                    int randomArenaRoom = Random.Range(0, _arenaRooms.Length);

                    GameObject arenaRoom = Instantiate(_arenaRooms[randomArenaRoom], pos, Quaternion.identity);
                    arenaRoom.name = "ArenaRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    arenaRoom.transform.SetParent(this.transform);
                    break;

                case RoomType.TwoByTwo:

                    int maxX = 0;
                    int minX = int.MaxValue;
                    int maxY = 0;
                    int minY = int.MaxValue;

                    if(_path[i - 1].GridPosition.x > maxX)
                    {
                        maxX = _path[i - 1].GridPosition.x;
                    }
                    if(_path[i - 1].GridPosition.x < minX)
                    {
                        minX = _path[i - 1].GridPosition.x;
                    }
                    
                    if(_path[i - 1].GridPosition.y > maxY)
                    {
                        maxY = _path[i - 1].GridPosition.y;
                    }
                    if(_path[i - 1].GridPosition.y < minY)
                    {
                        minY = _path[i - 1].GridPosition.y;
                    }


                    if(_path[i].GridPosition.x > maxX)
                    {
                        maxX = _path[i].GridPosition.x;
                    }
                    if(_path[i].GridPosition.x < minX)
                    {
                        minX = _path[i].GridPosition.x;
                    }
                    
                    if(_path[i].GridPosition.y > maxY)
                    {
                        maxY = _path[i].GridPosition.y;
                    }
                    if(_path[i].GridPosition.y < minY)
                    {
                        minY = _path[i].GridPosition.y;
                    }


                    if(_path[i + 1].GridPosition.x > maxX)
                    {
                        maxX = _path[i + 1].GridPosition.x;
                    }
                    if(_path[i + 1].GridPosition.x < minX)
                    {
                        minX = _path[i + 1].GridPosition.x;
                    }
                    
                    if(_path[i + 1].GridPosition.y > maxY)
                    {
                        maxY = _path[i + 1].GridPosition.y;
                    }
                    if(_path[i + 1].GridPosition.y < minY)
                    {
                        minY = _path[i + 1].GridPosition.y;
                    }

                    Vector3 twoByTwoPos = pos + new Vector3(
                        7,
                        0,
                        7
                    );

                    GameObject twoByTwoRoom = Instantiate(_twoByTwo, twoByTwoPos, Quaternion.identity);
                    twoByTwoRoom.name = "TwoByTwoRoom_" + _path[i].GridPosition.x + "_" + _path[i].GridPosition.y;
                    twoByTwoRoom.transform.SetParent(this.transform);
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

        //_grid.GetNavSurface().BuildNavMesh();
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
