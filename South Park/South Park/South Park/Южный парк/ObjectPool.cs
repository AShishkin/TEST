using System.Collections.Generic;

namespace South_Park
{
    class ObjectPool<T>
    {
        private List<T> list;
        /// <summary>
        /// Конструктор
        /// </summary>
        public ObjectPool()
        {
            list = new List<T>();
        }
        /// <summary>
        /// Добавляет обьект в коллекцию
        /// </summary>
        /// <param name="obj"></param>
        public void Add(T obj)
        {
            list.Add(obj);
            list.TrimExcess();
        }
        /// <summary>
        /// Возвращает колличестов обьектов в коллекции
        /// </summary>
        public int Count
        {
            get { return list.Count; }
        }

        public int Capacity
        {
            get { return list.Capacity; }
        }

        public T this[int Index] 
        {

            get { return list[Index]; }
            set { list[Index] = value; }

        }

        public void Clear()
        {
            list.Clear();
        }

    }
}
