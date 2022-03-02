namespace Interfaces.DBModel.Models
{
    public class APIRequestList
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public string Tables { get; set; }
        public string Columns { get; set; }
    }
}
