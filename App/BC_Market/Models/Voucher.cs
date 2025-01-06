using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents a voucher in the market.
    /// </summary>
    public class Voucher
    {
        /// <summary>
        /// Gets or sets the unique identifier for the voucher.
        /// </summary>
        public int VoucherId { get; set; }

        /// <summary>
        /// Gets or sets the name of the voucher.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the voucher.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the discount percentage of the voucher.
        /// </summary>
        public int Percent { get; set; }

        /// <summary>
        /// Gets or sets the discount amount of the voucher.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Gets or sets the condition to use the voucher.
        /// </summary>
        public double Condition { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the voucher.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the validation date of the voucher.
        /// </summary>
        public DateTime Validate { get; set; }

        /// <summary>
        /// Gets or sets the rank identifier required to use the voucher.
        /// </summary>
        public string RankId { get; set; }

        /// <summary>
        /// Creates a new voucher with the specified details.
        /// </summary>
        /// <param name="name">The name of the voucher.</param>
        /// <param name="description">The description of the voucher.</param>
        /// <param name="percent">The discount percentage of the voucher.</param>
        /// <param name="amount">The discount amount of the voucher.</param>
        /// <param name="condition">The condition to use the voucher.</param>
        /// <param name="stock">The stock quantity of the voucher.</param>
        /// <param name="validate">The validation date of the voucher.</param>
        /// <param name="rankId">The rank identifier required to use the voucher.</param>
        /// <returns>A new instance of the <see cref="Voucher"/> class.</returns>
        public Voucher CreateVoucher(string name, string description, int percent, int amount, double condition, int stock, DateTime validate, string rankId)
        {
            return new Voucher
            {
                Name = name,
                Description = description,
                Percent = percent,
                Amount = amount,
                Condition = condition,
                Stock = stock,
                Validate = validate,
                RankId = rankId
            };
        }

        /// <summary>
        /// Checks if the total amount meets the condition to use the voucher.
        /// </summary>
        /// <param name="total">The total amount to check.</param>
        /// <returns>True if the total meets the condition, otherwise false.</returns>
        public bool isCondition(double total)
        {
            return total >= Condition;
        }
    }
}
