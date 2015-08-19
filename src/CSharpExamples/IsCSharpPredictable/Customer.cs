namespace CSharpExamples.IsCSharpPredictable
{
    class Customer
    {
        public Customer()
        {
        }

        public Customer(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public Address Address { get; set; }

    }

    class Address
    {
        public string Street { get; set; }
        public string Country { get; set; }

    }

}
