using System.Linq;

namespace DZ4_Stack
{
    public class Stack
    {
        /// <summary>
        /// Внутренний класс ноды
        /// </summary>
        private class StackItem
        {
            internal string value;
            internal StackItem? next;

            internal StackItem(string value)
            {
                this.value = value;
                this.next = null;
            }

        }

        private StackItem ? _head;
        private int _size;

        public int Size { get { return _size; } }
        public string ? Top
        {
            get
            {
                if (_size > 0 && _head != null)
                {
                    return _head.value;
                }
                return null;
            }
        }

        public void Add(string value)
        {
            var item = new StackItem(value);
            item.next = _head;
            _head = item;
            _size++;
        }

        public Stack(params string[] strings)
        {
            _head = null;
            _size = 0;
             foreach (string value in strings)
             {
                Add(value);
             }
        }

        public string Pop()
        {
            if (_size > 0 && _head != null)
            {
                var top = _head.value;
                _head = _head.next;
                _size--;
                return top;
            }
            else
            {
                var ex = new Exception("Стек пустой");
                throw ex;
            }
        }

        public static Stack Concat(params Stack[] stacks)
        {
            var result = new Stack();
            for (int i = 0; i < stacks.Length; ++i)
            {
                result.Merge(stacks[i]);
            }
            return result;

        }

        /// <summary>
        /// Выдает элементы стека от нижнему к верхнему через запятую
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var items = new List<string>();
            if (_head != null)
            {
                StackItem? item = _head;
                while (item.next != null)
                {
                    items.Add(item.value);
                    item = item.next;
                }
                items.Add(item.value);
            }
            items.Reverse();
            return $"{string.Join(", ", items)} <- верхний";
        }
    }
}
