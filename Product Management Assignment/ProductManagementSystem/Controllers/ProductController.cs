using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using ProductManagementSystem.Models;

namespace ProductManagementSystem.Controllers
{
    public class ProductController : Controller
    {
        string baseUrl = "https://localhost:44378/api/ProductApi";
        HttpClient hc = new HttpClient();
        ProductManagementEntities db = new ProductManagementEntities();

        [Authorize]
        // GET: Product
        public ActionResult Index(string SearchBy,string search,int? page,string sortBy)
        {
            IEnumerable<ProductTbl> productObj = null;
            ProductTbl obj = new ProductTbl();

            hc.BaseAddress = new Uri(baseUrl);
            var consumeapi = hc.GetAsync("ProductApi");
            consumeapi.Wait();

            var readData = consumeapi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<IList<ProductTbl>>();
                displayData.Wait();

                productObj = displayData.Result;
            }

            ViewBag.SortProductNameParameter = string.IsNullOrEmpty(sortBy) ? "Name des" : "";
            ViewBag.SortProductCategoryParameter = string.IsNullOrEmpty(sortBy) ? "Category des" : "Category";
            
            var products = db.ProductTbls.AsQueryable();

            // search by ProductCategory and ProductName
            if(SearchBy == "ProductCategory")
            {
                products = products.Where(x => x.ProductCategory == search || search == null);
            }
            else
            {
                products = products.Where(x => x.ProductName.StartsWith(search) || search == null);
            }

            //Sort by Product Name and Category
            switch (sortBy)
            {
                case "Name des":
                    products = products.OrderByDescending(x => x.ProductName);
                    break;

                case "Category des":
                    products = products.OrderByDescending(x => x.ProductCategory);
                    break;

                case "Category":
                    products = products.OrderBy(x => x.ProductCategory);
                    break;

                default:
                    products = products.OrderBy(x => x.ProductName);
                    break;
            }

            return View(products.ToPagedList(page ?? 1,5));
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(HttpPostedFileBase SmallImage, HttpPostedFileBase LargeImage, ProductTbl product)
        {
            String filename = Path.GetFileName(SmallImage.FileName);
            String path = Path.Combine(Server.MapPath("~/uploadImages/"), filename);
            product.SmallImage = "~/uploadImages/" + filename;

            String filename1 = Path.GetFileName(LargeImage.FileName);
            String path1 = Path.Combine(Server.MapPath("~/uploadImages/"), filename1);
            product.LargeImage = "~/uploadImages/" + filename1;

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44378/api/ProductApi/");

            var insertrecord = hc.PostAsJsonAsync<ProductTbl>("ProductApi", product);
            insertrecord.Wait();

            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
               SmallImage.SaveAs(path);
               LargeImage.SaveAs(path1);
               TempData["inserted product successfully"] = "New Product Added Successfully!!";
               return RedirectToAction("Index");
            }
            else
            {
               TempData["failed to add product"] = "Failed To Add New Product !! Try Again";
               return RedirectToAction("Create");
            }
        }
            
       [Authorize]
        public ActionResult Details(int id)
        {
            ProductModel productObj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44378/api/ProductApi");

            var consumeApi = hc.GetAsync("ProductApi?id=" + id.ToString());
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<ProductModel>();
                displayData.Wait();
                productObj = displayData.Result;
            }
            return View(productObj);
        }

        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProductModel productObj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44378/api/ProductApi");

            var consumeApi = hc.GetAsync("ProductApi?id=" + id.ToString());
            consumeApi.Wait();

            var readData = consumeApi.Result;
            if (readData.IsSuccessStatusCode)
            {
                var displayData = readData.Content.ReadAsAsync<ProductModel>();
                displayData.Wait();
                productObj = displayData.Result;
                TempData["imgpath"] = productObj.SmallImage;
                TempData["imgpath1"] = productObj.LargeImage;

            }
            return View(productObj);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase SmallImage,HttpPostedFileBase LargeImage,ProductTbl product)
        {
            if (SmallImage != null && LargeImage != null  )
            {
                String filename = Path.GetFileName(SmallImage.FileName);
                String filename1 = Path.GetFileName(LargeImage.FileName);

                String path = Path.Combine(Server.MapPath("~/uploadImages/"), filename);
                String path1 = Path.Combine(Server.MapPath("~/uploadImages/"), filename1);

                product.SmallImage = "~/uploadImages/" + filename;
                product.LargeImage = "~/uploadImages/" + filename1;
                                               
                string oldImgPath = Request.MapPath(TempData["imgpath"].ToString());
                string oldImgPath1 = Request.MapPath(TempData["imgpath1"].ToString());

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44378/api/ProductApi/");

                var insertrecord = hc.PutAsJsonAsync<ProductTbl>("ProductApi", product);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    SmallImage.SaveAs(path);
                    LargeImage.SaveAs(path1);
                    if (System.IO.File.Exists(oldImgPath) && System.IO.File.Exists(oldImgPath1) )
                    {
                        System.IO.File.Delete(oldImgPath);
                        System.IO.File.Delete(oldImgPath1);

                    }
                            TempData["update data msg"] = "Data Updated Successfully!!";
                            return RedirectToAction("Index");
                        }
                                   
            }
            else
            {
                product.SmallImage = TempData["imgpath"].ToString();
                product.LargeImage = TempData["imgpath1"].ToString();

                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44378/api/ProductApi");

                var insertrecord = hc.PutAsJsonAsync<ProductTbl>("ProductApi", product);
                insertrecord.Wait();

                var savedata = insertrecord.Result;
                if (savedata.IsSuccessStatusCode)
                {
                    TempData["update data msg"] = "Data Updated Successfully!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["msg"] = "Product Record Not Updated.Try Again!!";
                }
            }
            return View(product);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Delete(IEnumerable<int> id)
        {
            int delsuccessfully = 0, delfailed = 0;
            foreach (var item in id)
            {
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44378/api/ProductApi");
                var delrecord = hc.DeleteAsync("ProductApi/" + item);

                delrecord.Wait();
                var dispalydata = delrecord.Result;
                if (dispalydata.IsSuccessStatusCode)
                {
                    delsuccessfully++;
                }
                else
                {
                    delfailed++;
                }
            }
            if (delsuccessfully == id.Count())
            {
                TempData["successcount"] = delsuccessfully + " records deleted successfully";
            }
            else
            {
                TempData["failedcount"] = delfailed + " records failed to delete";
            }

            return RedirectToAction("Index");
        }
    }
}