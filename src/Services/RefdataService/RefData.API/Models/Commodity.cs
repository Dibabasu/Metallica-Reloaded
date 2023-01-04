using System.ComponentModel.DataAnnotations;

namespace RefData.API.Models
{
    public class Commodity : BaseEntity
    {

        [Required]
        public string CommodityName { get; set; } = string.Empty;
        [Required]
        public string CommoditySymbol { get; set; } = string.Empty;
    }
}
