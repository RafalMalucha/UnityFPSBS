using UnityEngine;

public class Node
{
    public Vector2Int GridPosition;
    public Vector3 WorldPosition;

    public int gCost;
    public int hCost;
    public int fCost => gCost + fCost;

    public Node Parent;

    public Node(Vector2Int gridPos, Vector3 worldPos)
    {
        GridPosition = gridPos;
        WorldPosition = worldPos;
    }
}
