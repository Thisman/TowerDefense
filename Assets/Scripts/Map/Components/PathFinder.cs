using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Zenject;

namespace Game.Map
{
    public class PathFinder
    {
        [Inject]
        private MapModel model;

        public List<Vector3> GenerateEnemiesPath()
        {
            // Convert world positions to grid positions
            Vector3 startPosition = model.EnemiesGatePosition;
            Vector3 targetPosition = model.CastlePosition;
            Vector3Int startGridPos = model.Map.WorldToCell(startPosition);
            Vector3Int targetGridPos = model.Map.WorldToCell(targetPosition);

            // Initialize open and closed sets
            HashSet<Vector3Int> closedSet = new();
            PriorityQueue<Node> openSet = new();
            Dictionary<Vector3Int, Node> allNodes = new();

            // Create the start node
            Node startNode = new(startGridPos, model.Map.GetCellCenterWorld(startGridPos), null, 0, GetHeuristic(startGridPos, targetGridPos));
            openSet.Enqueue(startNode);
            allNodes[startGridPos] = startNode;

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.Dequeue();

                // If we reached the target, reconstruct the path
                if (currentNode.Position == targetGridPos)
                {
                    return ReconstructPath(currentNode);
                }

                closedSet.Add(currentNode.Position);

                foreach (Vector3Int neighbor in GetNeighbors(currentNode.Position))
                {
                    if (closedSet.Contains(neighbor) || !model.IsWalkable(neighbor))
                    {
                        continue;
                    }

                    float gCost = currentNode.GCost + Vector3Int.Distance(currentNode.Position, neighbor);

                    if (!allNodes.TryGetValue(neighbor, out Node neighborNode))
                    {
                        neighborNode = new Node(neighbor, model.Map.GetCellCenterWorld(neighbor), currentNode, gCost, GetHeuristic(neighbor, targetGridPos));
                        allNodes[neighbor] = neighborNode;
                        openSet.Enqueue(neighborNode);
                    }
                    else if (gCost < neighborNode.GCost)
                    {
                        neighborNode.Parent = currentNode;
                        neighborNode.GCost = gCost;
                        neighborNode.FCost = gCost + neighborNode.HCost;

                        openSet.UpdatePriority(neighborNode);
                    }
                }
            }

            // Return null if no path found
            return new List<Vector3>();
        }

        private List<Vector3Int> GetNeighbors(Vector3Int position)
        {
            List<Vector3Int> neighbors = new List<Vector3Int>
        {
            position + Vector3Int.left,
            position + Vector3Int.right,
            position + Vector3Int.up,
            position + Vector3Int.down
        };

            return neighbors;
        }

        private float GetHeuristic(Vector3Int current, Vector3Int target)
        {
            return Mathf.Abs(current.x - target.x) + Mathf.Abs(current.y - target.y);
        }

        private List<Vector3> ReconstructPath(Node targetNode)
        {
            List<Vector3> path = new List<Vector3>();
            Node currentNode = targetNode;

            while (currentNode != null)
            {
                path.Add(currentNode.Center);
                currentNode = currentNode.Parent;
            }

            path.Reverse();

            return path;
        }

        private class Node
        {
            public Vector3Int Position { get; }
            public Vector3 Center { get; }
            public Node Parent { get; set; }
            public float GCost { get; set; }
            public float HCost { get; }
            public float FCost { get; set; }

            public Node(Vector3Int position, Vector3 center, Node parent, float gCost, float hCost)
            {
                Position = position;
                Center = center;
                Parent = parent;
                GCost = gCost;
                HCost = hCost;
                FCost = gCost + hCost;
            }
        }

        private class PriorityQueue<T> where T : Node
        {
            private readonly List<T> items = new();

            public int Count => items.Count;

            public void Enqueue(T item)
            {
                items.Add(item);
                items.Sort((x, y) => x.FCost.CompareTo(y.FCost));
            }

            public T Dequeue()
            {
                T first = items[0];
                items.RemoveAt(0);
                return first;
            }

            public void UpdatePriority(T item)
            {
                items.Sort((x, y) => x.FCost.CompareTo(y.FCost));
            }
        }
    }
}
