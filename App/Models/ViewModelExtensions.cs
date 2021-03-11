using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Models.RequestModels;
using rp.Accounting.Domain;
using System;
using System.Linq;

namespace rp.Accounting.App.Models
{
    public static class ViewModelExtensions
    {
        #region DomainEnum
        /// <summary>
        /// CustomerType to presentable string
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string CustomerTypeToString(this CustomerType type)
            => type switch
            {
                CustomerType.Private => "Privat",
                CustomerType.Company => "Företag",
                _ => ""
            };

        public static CustomerType StringToCustomerType(this string s)
            => s switch
            {
                "Privat" => CustomerType.Private,
                "Företag" => CustomerType.Company,
                _ => throw new FormatException()
            };
        #endregion

        #region Customer Mapping
        public static CustomerInfo ToDto(this Customer c)
        => new CustomerInfo
        {
            Id = c.Id.ToString(),
            Active = c.Active,
            Address = c.Address,
            Email = c.Email,
            FirstName = c.FirstName,
            LastName = c.LastName,
            HourlyFee = c.HourlyFee,
            Type = c.Type.CustomerTypeToString()
        };

        public static CustomerRequest ToRequest(this CustomerInfo c)
        => new CustomerRequest
        {
            Active = c.Active,
            Address = c.Address,
            Email = c.Email,
            FirstName = c.FirstName,
            LastName = c.LastName,
            HourlyFee = c.HourlyFee.ToString() ?? "",
            Type = c.Type
        };

        public static Customer ToDomain(this CustomerRequest c)
        {
            var domainEntity = new Customer(c.FirstName, c.Type.StringToCustomerType())
            {
                Active = c.Active,
                Address = c.Address,
                Email = c.Email
            };
            domainEntity.SetLastName(c.LastName);
            if (double.TryParse(c.HourlyFee, out double hourly)) domainEntity.UpdateHourlyPrice(hourly);
            return domainEntity;
        }
        #endregion

        #region PrivatBillingBase Mapping
        public static PrivateBillingInfo ToDto(this PrivateBilling billingBase)
            => new PrivateBillingInfo
            {
                Id = billingBase.Id,
                Date = billingBase.Date,
                Items = billingBase.Items.Select(i => i.ToDto()).ToList()
            };

        public static PrivateBillingItemInfo ToDto(this PrivateBillingItem bb)
            => new PrivateBillingItemInfo
            {
                Id = bb.Id,
                PrivateBillingId = bb.Id,
                CustomerId = bb.Customer.Id,
                FirstName = bb.Customer.FirstName,
                LastName = bb.Customer.LastName,
                WeeksAttended = bb.WeeksAttended,
                AmountOccassions = bb.AmountOccassions.ToString(),
                HoursPerVisit = bb.HoursPerVisit,
                TotalHours = bb.TotalHours.ToString(),
                PricePerHour = bb.PricePerHour,
                ExVAT = bb.ExVAT,
                IncVAT = bb.IncVAT,
                AfterRUT = bb.AfterRUT
            };
        #endregion

        #region CompanyBillingBase Mapping
        public static CompanyBillingInfo ToDto(this CompanyBilling billingBase)
            => new CompanyBillingInfo
            {
                Id = billingBase.Id,
                Date = billingBase.Date,
                Items = billingBase.Items.Select(i => i.ToDto()).ToList()
            };

        public static CompanyBillingItemInfo ToDto(this CompanyBillingItem bb)
            => new CompanyBillingItemInfo
            {
                Id = bb.Id,
                CompanyBillingId = bb.Id,
                CustomerId = bb.Customer.Id,
                Name = bb.Customer.FirstName,
                Email = bb.Customer.Email,
                Notes = bb.Notes,
                ExVAT = bb.ExVAT.ToString(),
                IncVAT = bb.IncVAT
            };
        #endregion
    }
}
