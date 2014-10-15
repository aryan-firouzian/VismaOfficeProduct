using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VismaOfficeProduct.Models;

namespace VismaOfficeProduct.Controllers
{
    public class CustomerController : Controller
    {
        private ProductDBContext db = new ProductDBContext();

        //
        // GET: /Customer/
        // Send product model joint with rebate model to index view
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Rebate);
            return View(products.ToList());
        }

        // A string indicates rebate information of product an send it to RebateInformationPartialView 
        // Call by render action
        public ActionResult RebateInformationPartialView(Product product)
        {
            string rebateInformation = "";
            if (product.RebateType == Constant.RebateType.Volume) 
            {
                // Find rebateVolume properties of the product
                RebateVolume rebateVolume = db.RebateVolumes.Single(x => x.RebateId == product.Rebate.RebateId);
                rebateInformation = product.Rebate.Percentage.ToString() + "% discount for buying more than " + rebateVolume.Quantity + " items";
            }
            else if (product.RebateType == Constant.RebateType.Date) 
            {
                // Find rebateDate properties of the product
                RebateDate rebateDate = db.RebateDates.Single(x => x.RebateId == product.Rebate.RebateId);
                if (DateTime.Now > rebateDate.DateFrom && DateTime.Now < rebateDate.DateTo)
                {
                    rebateInformation = product.Rebate.Percentage.ToString() + "% discount from " + rebateDate.DateFrom.ToShortDateString() + " until " + rebateDate.DateTo.ToShortDateString();
                }
                else
                {
                    rebateInformation = "No rebate";
                }
            }
            else if (product.RebateType == Constant.RebateType.Deal)
            {
                // Find rebateDeal properties of the product
                RebateDeal rebateDeal = db.RebateDeals.Single(x => x.RebateId == product.Rebate.RebateId);
                rebateInformation = product.Rebate.Percentage.ToString() + "% discount";
            }
            else { rebateInformation = "No rebate"; }
            ViewBag.rebateInformation = rebateInformation;
            return PartialView();
        }

        // Calculate final price of the product based on original price, number of ordered items and rebate properties
        // Send an string as final price to Calculate partial view
        // Call by jquery post
        public ActionResult Calculate(string productId, string orderedQuantity)
        {
            int FinalPrice=0;
            if (productId != "" && orderedQuantity != "")
            {
                int ProductId = Int32.Parse(productId);
                int OrderedQuantity = Int32.Parse(orderedQuantity);
                Product Product = db.Products.Include(p => p.Rebate).Single(x => x.ProductId == ProductId);
                FinalPrice = Product.Calculate(OrderedQuantity);
            }

            ViewBag.finalPrice = FinalPrice.ToString();
            return PartialView();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}