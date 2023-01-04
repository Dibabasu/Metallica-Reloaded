using System.ComponentModel.DataAnnotations;

namespace RefData.API.Models
{
    public class Location : BaseEntity
    {

        [Required]
        public string LocationIdentifier { get; set; } = string.Empty;
        [Required]
        public string LocationName { get; set; } = string.Empty;
    }
}