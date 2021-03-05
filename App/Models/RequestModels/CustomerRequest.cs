using System.ComponentModel.DataAnnotations;

namespace rp.Accounting.App.Models.RequestModels
{
    public class CustomerRequest
    {
        [Required(ErrorMessage = "Du måste ange förnamn, för företag anges hela företagsnamnet som förnamn.")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required(ErrorMessage = "Du måste ange Email.")]
        public string Email { get; set; }
        public bool Active { get; set; } = true;
        [Required(ErrorMessage = "Du måste ange vilken typ av kund.")]
        public string Type { get; set; }
        public string HourlyFee { get; set; }

        public bool TypeIsPrivate => Type == "Privat";
        public bool TypeIsValid => Type == "Privat" || Type == "Företag";
    }
}
