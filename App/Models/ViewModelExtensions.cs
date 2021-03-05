using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Models.RequestModels;
using rp.Accounting.Domain;
using System;

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
    }
}
