using System.Collections.Specialized;

namespace DZ_13_SpecialCollections
{
    public class Customer 
    {
        public void OnItemChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
               case NotifyCollectionChangedAction.Add: // если добавление
                    if (e.NewItems is null) 
                    {
                        break;
                    }
                    foreach (var item in e.NewItems)
                    {
                        if (item is Item newItem)
                        {
                            Console.WriteLine($"Добавлен товар \"{newItem.Name}\": id = {newItem.Id}");
                        }
                    }
                    break;
                case NotifyCollectionChangedAction.Remove: // если удаление
                    if (e.OldItems is null)
                    {
                        break;
                    }
                    foreach (var item in e.OldItems)
                    {
                        if (item is Item oldItem)
                        {
                            Console.WriteLine($"Удален товар \"{oldItem.Name}\": id = {oldItem.Id}");
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
