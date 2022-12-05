using System.ComponentModel.DataAnnotations;

namespace TestAPI.ViewModels
{
    public class ColorViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
