// using UnityEngine;
// using System.Collections.Generic;
// using UnityEngine.Experimental.AI;



// #if UNITY_EDITOR
// using UnityEditor;
// #endif

// [ExecuteInEditMode]
// public class GridPathfindTest2 : MonoBehaviour
// {
//     private Node[,] grid;

//     [Header("Grid Size")]
//     [SerializeField] private int _gridSizeX;
//     [SerializeField] private int _gridSizeZ;

//     [Header("Node/Cell")]
//     [SerializeField] private int _cellSize;

//     private Node currentNode;
//     private List<Node> currentNodeNeighbors;

//     void Start()
//     {
//         BuildGrid();
//     }

//     private void OnValidate() 
//     {
//         BuildGrid();
//         currentNode = GetNode(2, 2);
//         currentNodeNeighbors = GetNeighborNodes(currentNode);
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

//         grid = new Node[_gridSizeX, _gridSizeZ];

//         for (int x = 0; x < _gridSizeX; x++)
//         {
//             for (int y = 0; y < _gridSizeZ; y++)
//             {
//                 Vector3 worldPosition = new Vector3(x * _cellSize, 0f, y * _cellSize);

//                 grid[x, y] = new Node(new Vector2Int(x, y), worldPosition);
//             }
//         }
//     }

//     private void OnDrawGizmos()
//     {
//         if (grid == null)
//             return;

//         foreach (Node node in grid)
//         {
//             Gizmos.color = Color.white;
//             Gizmos.DrawWireCube(node.WorldPosition, new Vector3(_cellSize, 1.0f, _cellSize));
//             // if(!(node == currentNode))
//             // {
//             //     Gizmos.color = Color.white;
//             //     Gizmos.DrawWireCube(node.WorldPosition, new Vector3(_cellSize, 1.0f, _cellSize));
//             // } 
//             // else
//             // {
//             //     Gizmos.color = Color.green;
//             //     Gizmos.DrawWireCube(node.WorldPosition, new Vector3(_cellSize, 1.0f, _cellSize));
//             // }

//             #if UNITY_EDITOR
//             Handles.Label(node.WorldPosition, $"{node.GridPosition.x},{node.GridPosition.y}");
//             #endif
//         }

//         Gizmos.color = Color.green;
//         Gizmos.DrawCube(currentNode.WorldPosition, new Vector3(1.0f, 1.0f, 1.0f));

//         Gizmos.color = Color.orange;

//         foreach(Node neighbor in currentNodeNeighbors)
//         {
//             Gizmos.DrawCube(neighbor.WorldPosition, new Vector3(1.0f, 1.0f, 1.0f));
//         }
//     }

//     public Node GetNode(int x, int y)
//     {
//         if (x < 0 || x > _gridSizeX || y < 0 || y > _gridSizeZ)
//         {
//             return null;
//         }

//         return grid[x, y];
//     }

//     public List<Node> GetNeighborNodes(Node currentNode)
//     {
//         List<Node> neighborNodes = new List<Node>();

//         if(currentNode.GridPosition.x - 1 > 0 && currentNode.GridPosition.y < _gridSizeZ)
//         {
//             neighborNodes.Add(grid[currentNode.GridPosition.x - 1, currentNode.GridPosition.y]);
//         }
//         if(currentNode.GridPosition.x + 1 < _gridSizeX && currentNode.GridPosition.y < _gridSizeZ)
//         {
//             neighborNodes.Add(grid[currentNode.GridPosition.x + 1, currentNode.GridPosition.y]);
//         }
//         if(currentNode.GridPosition.x < _gridSizeX && currentNode.GridPosition.y - 1 > 0)
//         {
//             neighborNodes.Add(grid[currentNode.GridPosition.x, currentNode.GridPosition.y - 1]);
//         }
//         if(currentNode.GridPosition.x < _gridSizeX && currentNode.GridPosition.y + 1 < _gridSizeZ)
//         {
//             neighborNodes.Add(grid[currentNode.GridPosition.x, currentNode.GridPosition.y + 1]);
//         }

//         foreach(Node neighbor in neighborNodes)
//         {
//             Debug.Log(neighbor.GridPosition);
//         }

//         return neighborNodes;
//     }
// }
