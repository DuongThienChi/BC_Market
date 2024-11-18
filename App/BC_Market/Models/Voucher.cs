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
    public class Voucher
    {
        public string VoucherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Percent { get; set; }
        public int Amount { get; set; }
        public double Condition { get; set; }
        public int Stock { get; set; }
        public DateTime Validate { get; set; }
        public string RankId { get; set; }

        public Voucher CreateVoucher(string name, string description, string percent, int amount, double condition, int stock, DateTime validate, string rankId)
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
    }
}
