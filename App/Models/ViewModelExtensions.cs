using rp.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Models
{
    public static class ViewModelExtensions
    {
        #region Enum
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
    }
}
