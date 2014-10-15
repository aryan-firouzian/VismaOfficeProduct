using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//Use code first to define the rebateDate model and inherit from parrent
namespace VismaOfficeProduct.Models
{
    [Table("tblRebateDate")]
    public class RebateDate : Rebate
    {
        [DataType(DataType.Date)]
        public DateTime DateFrom { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateTo { get; set; }

        public override int calculate(Product product, int OrderQuantity)
        {
            int FinalPrice = 0;
            if (DateTime.Now > DateFrom && DateTime.Now < DateTo)
            {
                FinalPrice = ((product.Price) * (100 - Percentage) / 100) * OrderQuantity;
            }
            else
            {
                FinalPrice = (product.Price) * OrderQuantity;
            }
            return FinalPrice;
        }
    }
}