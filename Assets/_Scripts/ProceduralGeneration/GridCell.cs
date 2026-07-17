using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private string _wallSeed;
    [SerializeField] private int _coordX;
    [SerializeField] private int _coordZ;

    // public GridCell(string newWallSeed, int newCoordX, int newCoordZ)
    // {
    //     _wallSeed = newWallSeed;
    //     _coordX = newCoordX;
    //     _coordZ = newCoordZ;
    // }

    public void GridCellSetup(string newWallSeed, int newCoordX, int newCoordZ)
    {
        _wallSeed = newWallSeed;
        _coordX = newCoordX;
        _coordZ = newCoordZ;
    }
}
