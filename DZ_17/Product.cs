using System.ComponentModel.DataAnnotations.Schema;

namespace DZ_17
{
    public class Product
    {
        public int Id;
        public string Name;
        public string Description;
        public int StockQuantity;
        public double Price;
    }
}
