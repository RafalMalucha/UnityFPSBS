using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pathfinder : MonoBehaviour
{
    [SerializeField] private GridManager _grid;
    [SerializeField] private PathVisualizer _pv;

    [SerializeField] private List<Node> _openSet = new List<Node>();
    [SerializeField] private HashSet<Node> _closedSet = new HashSet<Node>();
    [SerializeField] private List<Node> _path = new List<Node>();

    private void OnValidate() 
    {
        UnityEditor.EditorApplication.delayCall+=()=>
        {
            FindPath(_grid.GetNode(0, 0), _grid.GetNode(9, 9));
        };
    }
    
    public List<Node> FindPath(Node startNode, Node targetNode)
    {
        _openSet.Add(startNode);
        Debug.Log(_openSet.Count);
        foreach(Node node in _openSet)
        {
            Debug.Log(node.GridPosition);
        }

        while (_openSet.Count > 0)
        {
            Node currentNode = _openSet[0];

            for (int i = 1; i < _openSet.Count; i++)
            {
                if (_openSet[i].fCost < currentNode.fCost)
                {
                    currentNode = _openSet[i];
                }
            }

            _openSet.Remove(currentNode);
            _closedSet.Add(currentNode);

            if(currentNode.GridPosition == _grid.GetExitPoint())
            {
                Debug.Log("great success path found");
                return RetracePath(startNode, targetNode);
            }

            foreach(Node neighbor in _grid.GetNeighborNodes(currentNode))
            {
                int new_gCost = currentNode.gCost + 10;

                if(_closedSet.Contains(neighbor))
                    continue;

                if(new_gCost < neighbor.gCost || !_openSet.Contains(neighbor))
                {
                    neighbor.gCost = new_gCost;
                    neighbor.hCost = GetDistance(neighbor, targetNode);

                    neighbor.Parent = currentNode;
                    _openSet.Add(neighbor);
                    Debug.Log("added " + neighbor.GridPosition);
                }
            }
        }

        Debug.Log(_closedSet.Count);
        return null;
    }

    private List<Node> RetracePath(Node startNode, Node targetNode)
    {
        Node currentNode = targetNode;

        while(currentNode != startNode)
        {
            _path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        _path.Add(startNode);
        _path.Reverse();

        foreach(Node node in _path)
        {
            Debug.Log(node.GridPosition);
        }

        _pv.SetNewPath(_path);

        return _path;
    }

    private int GetDistance(Node currentNode, Node targetNode)
    {
        int dx = Mathf.Abs(currentNode.GridPosition.x - targetNode.GridPosition.x);
        int dy = Mathf.Abs(currentNode.GridPosition.y - targetNode.GridPosition.y);

        return (dx + dy) * 10;
    }
}