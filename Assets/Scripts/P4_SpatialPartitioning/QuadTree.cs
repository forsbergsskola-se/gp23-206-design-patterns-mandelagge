using System.Collections.Generic;
using UnityEngine;

namespace P4_SpatialPartitioning
{
    public class QuadTree
    {
        private const int maxObjectsPerNode = 5;
        private const int maxLevels = 6;

        private int level;
        private List<GameObject> objects;
        public Bounds bounds;
        public QuadTree[] nodes;

        public QuadTree(int level, Bounds bounds)
        {
            this.level = level;
            objects = new List<GameObject>();
            this.bounds = bounds;
            nodes = new QuadTree[4];
        }

        public void Clear()
        {
            objects.Clear();

            for (int i = 0; i < nodes.Length; i++)
            {
                if (nodes[i] != null)
                {
                    nodes[i].Clear();
                    nodes[i] = null;
                }
            }
        }

        private void Split()
        {
            float subWidth = bounds.size.x / 2f;
            float subHeight = bounds.size.y / 2f;
            float x = bounds.center.x;
            float y = bounds.center.y;

            nodes[0] = new QuadTree(level + 1, new Bounds(new Vector3(x + subWidth / 2, y + subHeight / 2, 0), new Vector3(subWidth, subHeight, 0)));
            nodes[1] = new QuadTree(level + 1, new Bounds(new Vector3(x - subWidth / 2, y + subHeight / 2, 0), new Vector3(subWidth, subHeight, 0)));
            nodes[2] = new QuadTree(level + 1, new Bounds(new Vector3(x - subWidth / 2, y - subHeight / 2, 0), new Vector3(subWidth, subHeight, 0)));
            nodes[3] = new QuadTree(level + 1, new Bounds(new Vector3(x + subWidth / 2, y - subHeight / 2, 0), new Vector3(subWidth, subHeight, 0)));
        }

        private int GetIndex(GameObject obj)
        {
            int index = -1;
            float verticalMidpoint = bounds.center.y;

            bool topQuadrant = obj.transform.position.y > verticalMidpoint;
            bool bottomQuadrant = obj.transform.position.y < verticalMidpoint;

            float horizontalMidpoint = bounds.center.x;

            if (obj.transform.position.x < horizontalMidpoint && obj.transform.position.x < horizontalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 1;
                }
                else if (bottomQuadrant)
                {
                    index = 2;
                }
            }
            else if (obj.transform.position.x > horizontalMidpoint)
            {
                if (topQuadrant)
                {
                    index = 0;
                }
                else if (bottomQuadrant)
                {
                    index = 3;
                }
            }

            return index;
        }

        public void Insert(GameObject obj)
        {
            if (nodes[0] != null)
            {
                int index = GetIndex(obj);

                if (index != -1)
                {
                    nodes[index].Insert(obj);
                    return;
                }
            }

            objects.Add(obj);
            
            if (objects.Count > maxObjectsPerNode && level < maxLevels)
            {
                if (nodes[0] == null)
                {
                    Split();
                }

                int i = 0;
                while (i < objects.Count)
                {
                    int index = GetIndex(objects[i]);
                    if (index != -1)
                    {
                        nodes[index].Insert(objects[i]);
                        objects.RemoveAt(i);
                    }
                    else
                    {
                        i++;
                    }
                }
            }
        }

        public List<GameObject> Retrieve(List<GameObject> returnObjects, GameObject obj)
        {
            int index = GetIndex(obj);
            if (index != -1 && nodes[0] != null)
            {
                nodes[index].Retrieve(returnObjects, obj);
            }

            returnObjects.AddRange(objects);

            return returnObjects;
        }

        
        
        
        
    }
}