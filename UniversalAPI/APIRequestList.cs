using System.ComponentModel.DataAnnotations;

namespace UniversalApi
{
    public class APIRequestList
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Request { get; set; }
        [Required]
        public string Tables { get; set; }
        [Required]
        public string Columns { get; set; }
        public string JoinCondition { get; set; }
    }
}
