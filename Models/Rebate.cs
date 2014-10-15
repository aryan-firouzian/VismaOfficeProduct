using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

/* Use code first to define the rebate model and generate a database
   Calculate method is called by anthoer method of product to return final price
   The rebate model is designed by TPT approach with rebate base type, it separate tables for separate type of rebate
   It is more like inheritance and normalized schema
   It is an abstract method, the method is called just when product has rebate
   So, it will be overridden by its childeren
 */
namespace VismaOfficeProduct.Models
{
    [Table("tblRebate")]
    public class Rebate
    {
        [Key,ForeignKey("Product")]
        public int RebateId { get; set; }
        public int Percentage { get; set; }

        public Product Product { get; set; }

        public virtual int calculate(Product product, int OrderQuantity)
        {
            throw new NotImplementedException();
        }
    }
}