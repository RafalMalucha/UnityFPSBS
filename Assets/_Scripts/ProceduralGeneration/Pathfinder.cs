using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Pathfinder : MonoBehaviour
{
    [SerializeField] private GridManager _grid;

    [SerializeField] private List<Node> _openSet = new List<Node>();
    [SerializeField] private HashSet<Node> _closedSet = new HashSet<Node>();

    private void OnValidate() 
    {
        UnityEditor.EditorApplication.delayCall+=()=>
        {
            FindPath(_grid.GetNode(0, 0), _grid.GetNode(9, 9));
        };
    }
    
    public void FindPath(Node startNode, Node targetNode)
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
            _grid.SetCurrentNode(_openSet[0]);

            _openSet.Remove(currentNode);
            _closedSet.Add(currentNode);

            if(currentNode.GridPosition == _grid.GetExitPoint())
            {
                Debug.Log("great success path found");
            }

            foreach(Node neighbor in _grid.GetNeighborNodes(currentNode))
            {
                if(_closedSet.Contains(neighbor))
                    continue;

                if(!_openSet.Contains(neighbor))
                {
                    neighbor.Parent = currentNode;
                    _openSet.Add(neighbor);
                    Debug.Log("added " + neighbor.GridPosition);
                }
            }
        }
    }
}