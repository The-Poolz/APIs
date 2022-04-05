using System.ComponentModel.DataAnnotations;

namespace UniversalAPI
{
    /// <summary>
    /// API request model.
    /// </summary>
    public class Request
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string SelectedTables { get; set; }
        [Required]
        public string SelectedColumns { get; set; } = "*";
        public string JoinCondition { get; set; }
    }
}
