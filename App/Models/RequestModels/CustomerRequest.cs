namespace rp.Accounting.App.Models.RequestModels
{
    public class CustomerRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public string Type { get; set; }
        public double? HourlyFee { get; set; }
    }
}
