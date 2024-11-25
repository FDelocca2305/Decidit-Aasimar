using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Core
{
    public class ObjectPool<T> where T : class, new()
    {
        private readonly Stack<T> pool;
        private readonly Func<T> objectFactory;
        private readonly Action<T> resetAction;

        public ObjectPool(Func<T> objectFactory, Action<T> resetAction = null, int initialCapacity = 100)
        {
            this.pool = new Stack<T>(initialCapacity);
            this.objectFactory = objectFactory ?? (() => new T());
            this.resetAction = resetAction;

            for (int i = 0; i < initialCapacity; i++)
            {
                pool.Push(objectFactory());
            }
        }

        public T Get()
        {
            T obj = pool.Count > 0 ? pool.Pop() : objectFactory();
            resetAction?.Invoke(obj);
            return obj;
        }

        public void Release(T obj)
        {
            pool.Push(obj);
        }
    }
}
