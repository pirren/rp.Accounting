using System;

namespace rp.Accounting.App.Models.InfoModels
{
    public class CustomerInfo
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public double? HourlyFee { get; set; }
        public string Type { get; set; }
        public DateTime Registered { get; }
    }
}
