using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class Quadtree
    {
        private readonly int maxObjects;
        private readonly int maxLevels;
        private readonly int level;
        private readonly List<GameObject> objects;
        private readonly Rectangle bounds;
        private Quadtree[] nodes;

        public Quadtree(int level, Rectangle bounds, int maxObjects = 10, int maxLevels = 5)
        {
            this.level = level;
            this.bounds = bounds;
            this.maxObjects = maxObjects;
            this.maxLevels = maxLevels;
            this.objects = new List<GameObject>();
            this.nodes = null;
        }

        public void Clear()
        {
            objects.Clear();
            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    node.Clear();
                }
                nodes = null;
            }
        }

        private void Split()
        {
            int subWidth = bounds.Width / 2;
            int subHeight = bounds.Height / 2;
            int x = bounds.X;
            int y = bounds.Y;

            nodes = new Quadtree[4];
            nodes[0] = new Quadtree(level + 1, new Rectangle(x + subWidth, y, subWidth, subHeight));
            nodes[1] = new Quadtree(level + 1, new Rectangle(x, y, subWidth, subHeight));
            nodes[2] = new Quadtree(level + 1, new Rectangle(x, y + subHeight, subWidth, subHeight));
            nodes[3] = new Quadtree(level + 1, new Rectangle(x + subWidth, y + subHeight, subWidth, subHeight));
        }

        private int GetIndex(GameObject obj)
        {
            int index = -1;
            double verticalMidpoint = bounds.X + (bounds.Width / 2);
            double horizontalMidpoint = bounds.Y + (bounds.Height / 2);

            bool topQuadrant = (obj.Transform.Position.Y < horizontalMidpoint && obj.Transform.Position.Y + obj.Size.Y < horizontalMidpoint);
            bool bottomQuadrant = (obj.Transform.Position.Y > horizontalMidpoint);

            if (obj.Transform.Position.X < verticalMidpoint && obj.Transform.Position.X + obj.Size.X < verticalMidpoint)
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
            else if (obj.Transform.Position.X > verticalMidpoint)
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
            if (nodes != null)
            {
                int index = GetIndex(obj);

                if (index != -1)
                {
                    nodes[index].Insert(obj);
                    return;
                }
            }

            objects.Add(obj);

            if (objects.Count > maxObjects && level < maxLevels)
            {
                if (nodes == null)
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
            if (index != -1 && nodes != null)
            {
                nodes[index].Retrieve(returnObjects, obj);
            }

            returnObjects.AddRange(objects);

            return returnObjects;
        }
    }
}
