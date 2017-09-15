namespace AqlaEvents
{
    public struct MoneyParseResult
    {
        public int Amount { get; set; }
        public string Currency { get; set; }

        public MoneyParseResult(int amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public bool Equals(MoneyParseResult other)
        {
            return Amount == other.Amount && string.Equals(Currency, other.Currency);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is MoneyParseResult && Equals((MoneyParseResult)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Amount * 397) ^ (Currency != null ? Currency.GetHashCode() : 0);
            }
        }

        public static bool operator ==(MoneyParseResult left, MoneyParseResult right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(MoneyParseResult left, MoneyParseResult right)
        {
            return !left.Equals(right);
        }
    }
}