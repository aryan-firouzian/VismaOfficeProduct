using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


//Use code first to define the rebateDeal model and inherit from parrent
namespace VismaOfficeProduct.Models
{
    [Table("tblRebateDeal")]
    public class RebateDeal : Rebate
    {

        public override int calculate(Product product, int OrderQuantity)
        {
            int FinalPrice = ((product.Price) * (100 - Percentage) / 100) * OrderQuantity;
            return FinalPrice;
        }

    }
}