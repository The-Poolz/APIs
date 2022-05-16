namespace QuickSQL
{
    public readonly struct Operator
    {
        public Operator(OperatorNames name, string value)
        {
            Name = name;
            Value = value;
        }

        public OperatorNames Name { get; }
        public string Value { get; }
    }
}
