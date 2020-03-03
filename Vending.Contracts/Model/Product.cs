namespace Vending.Contracts.Model
{
    public class Product
    {
        public int Id { get; }
        public string Name { get; }
        public int Price { get; }
        public int Portions { get; }

        public Product(int id, string name, int price, int portions)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.Portions = portions;
        }
    }
}