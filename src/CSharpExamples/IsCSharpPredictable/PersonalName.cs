namespace CSharpExamples.IsCSharpPredictable
{
    class PersonalName
    {
        public PersonalName(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public override int GetHashCode()
        {
            return this.FirstName.GetHashCode() + this.LastName.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return this.Equals(other as PersonalName);
        }

        public bool Equals(PersonalName other)
        {
            if ((object)other == null)
            {
                return false;
            }
            return this.FirstName == other.FirstName && this.LastName == other.LastName;
        }
    }


}
