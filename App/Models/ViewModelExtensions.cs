using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.Domain;

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
        public static string EnumToString(this CustomerType type)
            => type switch
            {
                CustomerType.Private => "Privat",
                CustomerType.Company => "Företag",
                _ => ""
            };
        #endregion

        #region Customer
        public static CustomerInfo ToDto(this Customer c)
        => new CustomerInfo
        {
            Active = c.Active,
            Address = c.Address,
            Email = c.Email,
            FirstName = c.FirstName,
            LastName = c.LastName,
            HourlyFee = c.HourlyFee,
            Type = c.Type.EnumToString()
        };
        #endregion
    }
}
