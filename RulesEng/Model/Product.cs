namespace RulesEng.Model
{
    public class Product
    {
        public string Name { get; set; }

        public double InterstRate { get; set; }

        public bool Disqualified { get; set; }

        public Product() { }

        public Product(Product product)
        {
            this.Name = product.Name;
            this.InterstRate = product.InterstRate;
            this.Disqualified = product.Disqualified;
        }
    }
}
