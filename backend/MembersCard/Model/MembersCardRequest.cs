using System.ComponentModel.DataAnnotations;

namespace MembersCard.Model
{
    public class MembersCardRequest
    {
        [Required]
        public string Mode { get; set; }

        public string IdToken { get; set; }
        public string Language { get; set; }
    }
}
