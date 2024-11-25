using Game.Scripts;
using Game.Scripts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class GameObjectsManager
    {
        private List<GameObject> gameObjects = new List<GameObject>();
        private List<GameObject> objectsToAdd = new List<GameObject>();
        private List<GameObject> objectsToRemove = new List<GameObject>();

        public void Add(GameObject gameObject)
        {
            objectsToAdd.Add(gameObject);
        }

        public void Remove(GameObject gameObject)
        {
            objectsToRemove.Add(gameObject);
        }

        public void UpdateAll(float deltaTime)
        {
            gameObjects.AddRange(objectsToAdd);
            objectsToAdd.Clear();

            foreach (var obj in gameObjects)
            {
                if (obj.IsActive)
                {
                    obj.Update(deltaTime);
                }
                else
                {
                    objectsToRemove.Add(obj);
                }
            }

            foreach (var obj in objectsToRemove)
            {
                //if (obj is Enemy enemy)
                //{
                //    continue;
                //}

                gameObjects.Remove(obj);
            }

            objectsToRemove.Clear();
        }

        public void RenderAll()
        {
            foreach (var obj in gameObjects)
            {
                if (obj.IsActive && obj is IRenderizable renderizable)
                {
                    renderizable.Render();
                }
            }
        }

        public IEnumerable<GameObject> GetAllGameObjects()
        {
            return gameObjects;
        }

        public IEnumerable<GameObject> GetObjectsToAdd()
        {
            return objectsToAdd;
        }
    }
}
