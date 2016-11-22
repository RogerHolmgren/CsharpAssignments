using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyList
{
    public class List<T>
    {
        private T[] _items;
        public int Count { get { return _items.Length; } }
        public T this[int index]
        {
            get
            {
                return _items[index];
            }
            set
            {
                if (index == _items.Length)
                {
                    Add(value);
                }
                else if (index < _items.Length)
                {
                    _items[index] = value;
                }
            }
        }

        public List()
        {
            _items = new T[0];
        }

        public void Add(T item)
        {
            Array.Resize(ref _items, _items.Length + 1);
            _items[_items.Length - 1] = item;
        }

        public IEnumerable<T> Where(Func<T, bool> pre)
        {
            for (int i = 0; i < this._items.Length; i++)
            {
                if (pre(this._items[i]))
                {
                    yield return _items[i];
                }
            }
        }
    }
}