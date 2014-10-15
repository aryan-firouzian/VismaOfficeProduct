using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

//Use code first to define the rebateVolume model and inherit from parrent
namespace VismaOfficeProduct.Models
{
    [Table("tblRebateVolume")]
    public class RebateVolume : Rebate
    {
        public int Quantity { get; set; }

        public override int calculate(Product product, int OrderQuantity)
        {
            int FinalPrice = 0;
            if (OrderQuantity >= Quantity)
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