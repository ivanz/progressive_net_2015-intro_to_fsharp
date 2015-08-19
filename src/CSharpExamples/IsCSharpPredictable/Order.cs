namespace CSharpExamples.IsCSharpPredictable
{
    class Order
    {
        public Order(int id, string owner)
        {
            this.Id = id;
            this.Owner = owner;
        }

        public int Id { get; private set; }
        public string Owner { get; private set; }

    }
}
