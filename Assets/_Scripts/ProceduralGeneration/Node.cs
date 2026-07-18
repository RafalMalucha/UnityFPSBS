using UnityEngine;

public class Node
{
    public Vector2Int GridPosition;
    //public Vector3 WorldPosition;
    public int RandomAdditionalCost;

    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost + RandomAdditionalCost;

    public Node Parent;

    public Node(Vector2Int gridPos)
    {
        GridPosition = gridPos;
        RandomAdditionalCost = Random.Range(0, 100);
    }
}
