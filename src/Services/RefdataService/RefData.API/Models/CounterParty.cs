using System.ComponentModel.DataAnnotations;

namespace RefData.API.Models
{
    public class CounterParty : BaseEntity
    {

        [Required]
        public string CounterPartyIdentifier { get; set; } = string.Empty;
        [Required]
        public string CounterPartyName { get; set; } = string.Empty;
    }
}
