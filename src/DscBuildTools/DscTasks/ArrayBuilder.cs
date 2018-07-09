using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DscBuildTools.DscTasks
{
    public class ArrayBuilder<T>
    {
        private List<T> _list;
        public ArrayBuilder()
        {
            _list = new List<T>();
        }

        public void Add(T item)
        {
            _list.Add(item);
        }

        public T[] ToArray()
        {
            return _list.ToArray();
        }

        public static implicit operator T[](ArrayBuilder<T> other)
        {
            return other.ToArray();
        }
    }
}
