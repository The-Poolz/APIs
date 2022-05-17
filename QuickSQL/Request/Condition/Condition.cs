namespace QuickSQL
{
    public class Condition
    {
        public string ParamName { get; set; }
        public OperatorName Operator { get; set; }
        public string ParamValue { get; set; }
    }
}
