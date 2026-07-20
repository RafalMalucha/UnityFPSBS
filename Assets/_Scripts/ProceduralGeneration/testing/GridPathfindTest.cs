// using System.Collections.Generic;
// using UnityEngine;

// [ExecuteInEditMode]
// public class GridPathfindTest : MonoBehaviour
// {
//     [SerializeField] private GameObject _cube;
//     [SerializeField] private int _gridSizeX;
//     [SerializeField] private int _gridSizeZ;
//     [SerializeField] private int _gridOffset;

//     [SerializeField] private bool _generatePath;

//     [SerializeField] private List<Vector2> cellsToSearch;
//     [SerializeField] private List<Vector2> searchedCells;
//     [SerializeField] private List<Vector2> finalPath;

//     private bool _pathGenerated;

//     private Dictionary<Vector2, Cell> cells;


//     private void OnValidate() {
//         BuildGrid();
//         FindPath(new Vector2(0, 0), new Vector2(_gridSizeX, _gridSizeZ));
//     }

//     void Start()
//     {
//         BuildGrid();
//         FindPath(new Vector2(0, 0), new Vector2(_gridSizeX, _gridSizeZ));
//     }

//     private void BuildGrid()
//     {
//         foreach(Transform child in transform)
//         {
//             UnityEditor.EditorApplication.delayCall+=()=>
//             {
//                 UnityEditor.Undo.DestroyObjectImmediate(child.gameObject);
//             };
//         }

//         cells = new Dictionary<Vector2, Cell>();

//         for(int x = 0; x < _gridSizeX; x++)
//         {
//             for(int y = 0; y < _gridSizeZ; y++)
//             {
//                 Vector2 pos = new Vector2(x, y);
//                 cells.Add(pos, new Cell(pos));

//                 Vector3 position = new Vector3(x * _gridOffset, 0, y * _gridOffset);

//                 GameObject cube = Instantiate(_cube, position, Quaternion.identity);

//                 cube.transform.SetParent(this.transform);
//                 cube.transform.name = "cube_" + x + "_" + y;
//             }
//         }
//     }

//     private void FindPath(Vector2 startPos, Vector2 endPos)
//     {
//         searchedCells = new List<Vector2>();
//         cellsToSearch = new List<Vector2>() {startPos};
//         finalPath = new List<Vector2>();

//         //Cell startCell = cells[startPos];
//         cells[startPos].gCost = 0;
//         cells[startPos].hCost = GetDistance(startPos, endPos);
//         cells[startPos].fCost = GetDistance(startPos, endPos);

//         while (cellsToSearch.Count > 0)
//         {
//             Vector2 cellToSearch = cellsToSearch[0];

//             foreach (Vector2 pos in cellsToSearch)
//             {
//                 Cell cell = cells[pos];
//                 if (cell.fCost < cells[cellToSearch].fCost || cell.fCost == cells[cellToSearch].fCost && cell.hCost < cells[cellToSearch].hCost)
//                 {
//                     cellToSearch = pos;
//                 }
//             }

//             cellsToSearch.Remove(cellToSearch);
//             searchedCells.Add(cellToSearch);

//             if (cellToSearch == endPos)
//             {
//                 Cell pathCell = cells[endPos];

//                 while (pathCell.position != startPos)
//                 {
//                     finalPath.Add(pathCell.position);
//                     pathCell = cells[pathCell.connection];
//                 }

//                 finalPath.Add(startPos);
//                 return;
//             }

//             SearchNeighboringCells(cellToSearch, endPos);
//         }
//     }

//     private void SearchNeighboringCells(Vector2 cellPos, Vector2 endPos)
//     {
//         for (float x = cellPos.x; x <= cellPos.x; x++)
//         {
//             for (float y = cellPos.y; y <= cellPos.y; y++)
//             {
//                 Vector2 neighborPos = new Vector2(x, y);

//                 if(cells.TryGetValue(neighborPos, out Cell cell) && !searchedCells.Contains(neighborPos))
//                 {
//                     int gCostToNeighbor = cells[cellPos].gCost + GetDistance(cellPos, neighborPos);

//                     if(gCostToNeighbor < cells[neighborPos].gCost)
//                     {
//                         Cell neighborCell = cells[neighborPos];

//                         neighborCell.connection = cellPos;
//                         neighborCell.gCost = gCostToNeighbor;
//                         neighborCell.hCost = GetDistance(neighborPos, endPos);
//                         neighborCell.fCost = neighborCell.gCost + neighborCell.hCost;

//                         if (!cellsToSearch.Contains(neighborPos))
//                         {
//                             cellsToSearch.Add(neighborPos);
//                         }
//                     }
//                 }
//             }
//         }
//     }

//     private int GetDistance(Vector2 pos1, Vector2 pos2)
//     {
//         Vector2Int dist = new Vector2Int(Mathf.Abs((int)pos1.x - (int)pos2.x), Mathf.Abs((int)pos1.y - (int)pos2.y));

//         int lowest = Mathf.Min(dist.x, dist.y);
//         int highest = Mathf.Max(dist.x, dist.y);

//         int horizontalMovesRequired = highest - lowest;

//         return lowest * 14 + horizontalMovesRequired * 10; // 14 diagonal move (sqrt(2) * 10)
//     }

//     public class Cell
//     {
//         public Vector2 position;
//         public GameObject cube;
//         public int fCost = int.MaxValue;
//         public int gCost = int.MaxValue;
//         public int hCost = int.MaxValue;
//         public Vector2 connection;
//         public bool isWall;

//         public Cell(Vector2 pos)
//         {
//             position = pos;
//         }
//     }
// }

