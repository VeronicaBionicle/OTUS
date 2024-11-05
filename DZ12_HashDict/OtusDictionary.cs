using System.Data.SqlTypes;

namespace DZ12_HashDict
{
    public class OtusDictionary <T>
    {
        private const uint _initSize = 32; // начальный размер словаря

        private uint _size;
        private List<int> _keys;
        private T? [] _values;
        public uint Size { get { return _size; } }

        private static int GetHash(int key, uint size)
        {
           // var hash = key * 2654435761 & (size - 1); // Хэш с использованием простых чисел, подходит для хэш-таблиц с размерностью 2^n
            var hash = (1 + key % 19 + key * 11) & ((int)size - 1); // "плохая" хэш-функция, чтобы проверить обработку коллизий
            return hash%(int)size;
        }

        private bool KeyExists(int key) 
        {
            return _keys.Exists(k => (k == key));
        }

        public OtusDictionary() 
        {
            _size = _initSize;
            _values = new T?[_size];
            _keys = new List<int>();
        }

        private void CheckKey(int key) 
        {
            if (key < 0)
            {
                throw new ArgumentException($"Invalid key {key}: must be greater than or equal to zero");
            }
            if (key >= _size) 
            {
                throw new ArgumentException($"Invalid key {key}: must be less than size {_size} of dictionary");
            }
        }

        public void Add(int key, T? value)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"Argument {nameof(value)} can't be null.");
            }

            CheckKey(key);

            var hash = GetHash(key, _size);

            if (KeyExists(key)) // если ключ уже есть, подменяем значение 
            {
                _values[hash] = value; // новый элемент вставляется по "старому" индексу
            }
            else // ключ новый, будем проверять хэш
            {
                if (_values[hash] != null) // если такой хэш уже есть, надо устранять коллизию
                {
                    ExtendDictionary();   // расширяем словарь
                    this.Add(key, value); // пытаемся снова вставить значение
                }
                else // коллизии нет, просто вставляем
                {
                    _keys.Add(key);  
                    _values[hash] = value;
                }
            }
        }

        public T Get(int key)
        {
            CheckKey(key);

            if (!KeyExists(key))
            {
                throw new ArgumentException($"No such key: {key}");
            }

            var hash = GetHash(key, _size);
            var value = _values[hash]!;

            return value;
        }

        public T this[int key]
        {
            get => Get(key);
            set => Add(key, value);
        }

        private void ExtendDictionary()
        {
            // Расширяемся в 2 раза
            var oldSize = _size;
            _size *= 2;

            var oldValues = _values;
            _values = new T?[_size];

            var oldKeys = _keys;
            _keys = new List<int>();

            // Пересчитываем хэши и перетаскиваем данные
            for (int i = 0; i < oldSize; ++i)
            {
                var oldHash = GetHash(i, oldSize);
                if (oldValues[oldHash] != null && oldKeys.Exists(k => (k == i))) // тут и не было данных, пропускаем
                {
                    var newHash = GetHash(i, _size);
                    _values[newHash] = oldValues[oldHash];
                    _keys.Add(i);
                }
            }
        }
    }
}
