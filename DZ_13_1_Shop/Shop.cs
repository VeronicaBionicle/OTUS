using System.Collections.ObjectModel;

namespace DZ_13_SpecialCollections
{
    public class Shop
    {
        private readonly ObservableCollection<Item> _items;
        private int _id;
        public void Add() 
        {
            string itemName = $"Товар от {DateTime.Now}";
            _items.Add(new Item(_id, itemName));
            ++_id;
        }
        public void Remove(int id)
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
                if (_items[i].Id == id)
                {
                    _items.RemoveAt(i);
                }
            }
        }
        public void AddCustomer(Customer customer) 
        {
            _items.CollectionChanged += customer.OnItemChanged;
        }
        public Shop() 
        {
            _items = new ObservableCollection<Item>();
            _id = 0;
        }
    }
}
