using ProductManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProductManagementSystem.Controllers
{
    public class ProductApiController : ApiController
    {
        ProductManagementEntities db = new ProductManagementEntities();
        public IHttpActionResult GetProduct()
        {

            var results = db.ProductTbls.ToList();
            return Ok(results);
        }

        [HttpPost]
        public IHttpActionResult InsertProduct(ProductTbl product)
        {
            if (ModelState.IsValid)
            {
                db.ProductTbls.Add(product);
                db.SaveChanges();
            }
            return Ok();
        }

        public IHttpActionResult GetProductId(int id)
        {
            ProductModel productDetails = null;
            productDetails = db.ProductTbls.Where(x => x.Id == id).Select(x => new ProductModel()
            {
                Id = x.Id,
                ProductCategory = x.ProductCategory,
                ProductName = x.ProductName,
                ProductPrice = x.ProductPrice,
                ProductQuantity = x.ProductQuantity,
                ShortDesc = x.ShortDesc,
                LongDesc = x.LongDesc,
                SmallImage = x.SmallImage,
                LargeImage = x.LargeImage,
            }).FirstOrDefault<ProductModel>();
            if (productDetails == null)
            {
                return NotFound();
            }
            return Ok(productDetails);
        }
        public IHttpActionResult Put(ProductModel product)
        {
            var UpdateProduct = db.ProductTbls.Where(x => x.Id == product.Id).FirstOrDefault<ProductTbl>();
            if (UpdateProduct != null)
            {
                UpdateProduct.ProductCategory = product.ProductCategory;
                UpdateProduct.ProductName = product.ProductName;
                UpdateProduct.ProductQuantity = product.ProductQuantity;
                UpdateProduct.ProductPrice = product.ProductPrice;
                UpdateProduct.ShortDesc = product.ShortDesc;
                UpdateProduct.LongDesc = product.LongDesc;
                UpdateProduct.SmallImage = product.SmallImage;
                UpdateProduct.LargeImage = product.LargeImage;

                db.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var ProductDel = db.ProductTbls.Where(x => x.Id == id).FirstOrDefault();
            db.Entry(ProductDel).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok();
        }
    }

    


}
