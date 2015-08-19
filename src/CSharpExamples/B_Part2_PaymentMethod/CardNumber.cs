using System;

namespace CSharpExamples.PaymentMethod
{
    internal class CardNumber : IEquatable<CardNumber>
    {
        public CardNumber(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }

        public override string ToString()
        {
            return this.Value;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as CardNumber);
        }

        public bool Equals(CardNumber other)
        {
            if (other == null) { return false;}
           
            return this.Value.Equals(other.Value);
        }
    }
}