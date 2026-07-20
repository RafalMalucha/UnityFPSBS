// using System;
// using UnityEngine;

// [ExecuteInEditMode]
// public class GridGenerator : MonoBehaviour
// {
//     [SerializeField] private GameObject _floor;
//     [SerializeField] private GameObject _wall;
//     [SerializeField] private int _gridSizeX;
//     [SerializeField] private int _gridSizeZ;
//     [SerializeField] private int _gridOffset;

//     private Vector3 wallNorthPositionOffset = new Vector3(0, 3, 10);
//     private Quaternion wallNorhtRotation = Quaternion.identity;

//     private Vector3 wallEastPositionOffset = new Vector3(10, 3, 0);
//     private Quaternion wallEastRotation = Quaternion.identity * Quaternion.Euler(0, 90, 0);

//     private Vector3 wallSouthPositionOffset = new Vector3(0, 3, -10);
//     private Quaternion wallSouthRotation = Quaternion.identity;

//     private Vector3 wallWestPositionOffset = new Vector3(-10, 3, 0);
//     private Quaternion wallWestRotation = Quaternion.identity * Quaternion.Euler(0, 90, 0);

//     private void OnValidate() {
//         BuildGrid();
//     }

//     void Start()
//     {
//         BuildGrid();
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

//         var counter = 0;

//         for(int x = 0; x < _gridSizeX; x++)
//         {
//             for(int z = 0; z < _gridSizeZ; z++)
//             {

//                 Vector3 position = new Vector3(x * _gridOffset, 0, z * _gridOffset);

//                 GameObject floor = Instantiate(_floor, position, Quaternion.identity);

//                 floor.transform.SetParent(this.transform);
//                 floor.transform.name = "floor_" + x + "_" + z;

//                 int wallSeed = (int)Math.Floor(UnityEngine.Random.Range(0.0f, 15.0f));
//                 string wallSeedBinary = Convert.ToString(wallSeed, 2);
//                 wallSeedBinary = "0000" + wallSeedBinary;
//                 wallSeedBinary = wallSeedBinary.Substring(wallSeedBinary.Length - 4);

//                 //floor.GetComponent<GridCell>().GridCellSetup(string newWallSeed, int newCoordX, int newCoordZ)

//                 if(wallSeedBinary[0] - '0' == 1 | z == _gridSizeZ - 1)
//                 {
//                     SpawnNorthWall(floor);
//                 }
//                 if(wallSeedBinary[1] - '0' == 1 | x == _gridSizeX - 1)
//                 {
//                     SpawnEastWall(floor);
//                 }
//                 if(wallSeedBinary[2] - '0' == 1 | z == 0)
//                 {
//                     SpawnSouthWall(floor);
//                 }
//                 if(wallSeedBinary[3] - '0' == 1 | x == 0)
//                 {
//                     SpawnWestWall(floor);
//                 }

//                 counter++;
//             }
//         }
//     }

//     private void SpawnNorthWall(GameObject floor)
//     {
//         GameObject wallNorth = Instantiate(_wall, floor.transform.position + wallNorthPositionOffset, wallNorhtRotation);
//         wallNorth.transform.SetParent(floor.transform);
//         wallNorth.transform.name = "wall_north";
//     }

//     private void SpawnEastWall(GameObject floor)
//     {
//         GameObject wallEast = Instantiate(_wall, floor.transform.position + wallEastPositionOffset, wallEastRotation);
//         wallEast.transform.SetParent(floor.transform);
//         wallEast.transform.name = "wall_east";
//     }

//     private void SpawnSouthWall(GameObject floor)
//     {
//         GameObject wallSouth = Instantiate(_wall, floor.transform.position + wallSouthPositionOffset, wallSouthRotation);
//         wallSouth.transform.SetParent(floor.transform);
//         wallSouth.transform.name = "wall_south";
//     }

//     private void SpawnWestWall(GameObject floor)
//     {
//         GameObject wallWest = Instantiate(_wall, floor.transform.position + wallWestPositionOffset, wallWestRotation);
//         wallWest.transform.SetParent(floor.transform);
//         wallWest.transform.name = "wall_west";
//     }
// }
