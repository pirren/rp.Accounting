namespace rp.Accounting.App.Models.InfoModels
{
    public class PrivateBillingItemInfo
    {
        public int Id { get; set; }
        public int PrivateBillingId { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string WeeksAttended { get; set; }
        public string AmountOccassions { get; set; }
        public string HoursPerVisit { get; set; }
        public string TotalHours { get; set; }
        public double PricePerHour { get; set; }
        public double ExVAT { get; set; }
        public double IncVAT { get; set; }
        public double AfterRUT { get; set; }
    }
}
