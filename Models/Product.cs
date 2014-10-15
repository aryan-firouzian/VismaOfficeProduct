using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/* Use code first to define the product model and generate a database
// Change on the model in the code lead to dropping existing database and generating new one
// Calculate method return integer value of final price
// If Product contains any rebate options then it get final price from calculate method of rebate
// If Product didn't contain rebate then it give final price based on number of items ordered
// Including business methods on DAO is not best practise,
// On the other hand, DTO is not best solution for MVC project */

namespace VismaOfficeProduct.Models
{
    [Table("tblProducts")]
    public class Product
    {
        public Product() {  }
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required and must be a number.")]
        public int Price { get; set; }
        public string RebateType { get; set; }

        public Rebate Rebate { get; set; }

        public int Calculate(int OrderQuantity)
        {
            int FinalPrice = 0;
            if(Rebate != null)
                FinalPrice = Rebate.calculate(this, OrderQuantity);
            else
                FinalPrice = (Price) * OrderQuantity;
            return FinalPrice;
        }
    }
}