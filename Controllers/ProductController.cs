using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VismaOfficeProduct.Constant;
using VismaOfficeProduct.Models;

namespace VismaOfficeProduct.Controllers
{
    public class ProductController : Controller
    {
        private ProductDBContext db = new ProductDBContext();
        //
        // GET: /Product/
        // Send product model joint with rebate model to index view
        public ActionResult Index()
        {
            var productWithRebate = db.Products.Include(product => product.Rebate);
            return View(productWithRebate.ToList());
        }


        //
        // GET: /Product/Create
        // Call Create view
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Product/Create
        // Get product properties with related information about the rebate and create the product
        // Called by Jquery post
        [HttpPost]
        public ActionResult Create(Product product)
        {
            Product Product = new Product();
            string Title = Request.Params["Title"];
            string Description = Request.Params["Description"];
            int Price = Int32.Parse(Request.Params["Price"]);
            string RebateType = Request.Params["RebateType"];
            int Percentage = new int();
            int Quantity = new int();
            DateTime DateFrom = new DateTime();
            DateTime DateTo = new DateTime();
            if (RebateType != Constant.RebateType.None) { Percentage = Int32.Parse(Request.Params["Percentage"]); }
            if (RebateType == Constant.RebateType.Volume) { Quantity = Int32.Parse(Request.Params["Quantity"]); }
            if (RebateType == Constant.RebateType.Date)
            {
                DateFrom = Convert.ToDateTime(Request.Params["DateFrom"]);
                DateTo = Convert.ToDateTime(Request.Params["DateTo"]);
            }

            PopulateProductProperties(Product, Title, Description, Price, RebateType, Percentage, Quantity, DateFrom, DateTo);
        
            db.Products.Add(Product);
            db.SaveChanges();
            return View(Product);
        }

        //
        // GET: /Product/Edit/5
        // Call eidt view by passing product model
        public ActionResult Edit(int id = 0)
        {
            Product product = db.Products.Include(p => p.Rebate).Single(x => x.ProductId == id);
            //Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5
        // Get product id and properties with related information about the rebate, and edit the product
        // Called by Jquery post
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            int productId = Int32.Parse(Request.Params["ProductId"]);
            Product Product = db.Products.Include(p => p.Rebate).Single(x => x.ProductId == productId);
            string Title = Request.Params["Title"];
            string Description = Request.Params["Description"];
            int Price = Int32.Parse(Request.Params["Price"]);
            string RebateType = Request.Params["RebateType"];
            int Percentage = new int();
            int Quantity = new int();
            DateTime DateFrom = new DateTime();
            DateTime DateTo = new DateTime();
            if (RebateType != Constant.RebateType.None) { Percentage = Int32.Parse(Request.Params["Percentage"]); }
            if (RebateType == Constant.RebateType.Volume) { Quantity = Int32.Parse(Request.Params["Quantity"]); }
            if (RebateType == Constant.RebateType.Date)
            {
                DateFrom = Convert.ToDateTime(Request.Params["DateFrom"]);
                DateTo = Convert.ToDateTime(Request.Params["DateTo"]);
            }

            PopulateProductProperties(Product, Title, Description, Price, RebateType, Percentage, Quantity, DateFrom, DateTo);
            
            db.SaveChanges();
            return View(Product);
        }

        // Populate Product properties before returning it to Edit or Create method
        private void PopulateProductProperties(Product model,string title, string description,int price,string rebateType,int percentage,int quantity,DateTime dateFrom,DateTime dateTo)
        {
            model.Title = title;
            model.Description = description;
            model.Price = price;
            model.RebateType = rebateType;
            if (rebateType != Constant.RebateType.None)
            {
                Rebate Rebate = new Rebate();
                if (rebateType.Equals(Constant.RebateType.Volume))
                {
                    RebateVolume RebateVolume = new Models.RebateVolume();
                    RebateVolume.Quantity = quantity;
                    Rebate = RebateVolume;
                }
                else if ((rebateType.Equals(Constant.RebateType.Date)))
                {
                    RebateDate RebateDate = new Models.RebateDate();
                    RebateDate.DateFrom = dateFrom;
                    RebateDate.DateTo = dateTo;
                    Rebate = RebateDate;
                }
                else
                {
                    RebateDeal RebateDeal = new RebateDeal();
                    Rebate = RebateDeal;
                }
                Rebate.Percentage = percentage;
                model.Rebate = Rebate;
            }
        }

        //
        // GET: /Product/Delete/5
        // Call delete view by passing product model
        public ActionResult Delete(int id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5
        // Delete product and call Index view
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Include(p => p.Rebate).Single(x=>x.ProductId==id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // Call input fields for RebateDeal or RebateDate or RebateVolume model as partial view
        // Called by JQuery Get request and based on selected item in dropdownlist
        #region
        public ActionResult RebateDealCreatePartial()
        {
            RebateDeal RebateDealModel = new RebateDeal();
            return PartialView(RebateDealModel);
        }

        public ActionResult RebateVolumeCreatePartial()
        {
            RebateVolume RebateVolumeModel = new RebateVolume();
            return PartialView(RebateVolumeModel);
        }

        public ActionResult RebateDateCreatePartial()
        {
            RebateDate RebateDateModel = new RebateDate();
            return PartialView(RebateDateModel);
        }
        public ActionResult RebateDealEditPartial()
        {
            RebateDeal RebateDealModel = new RebateDeal();
            return PartialView(RebateDealModel);
        }

        public ActionResult RebateVolumeEditPartial()
        {
            RebateVolume RebateVolumeModel = new RebateVolume();
            return PartialView(RebateVolumeModel);
        }

        public ActionResult RebateDateEditPartial()
        {
            RebateDate RebateDateModel = new RebateDate();
            return PartialView(RebateDateModel);
        }
        #endregion
     
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}