using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcProjeKampi.Controllers
{
    public class StatisticsController : Controller
    {
        CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
        HeadingManager headingManager = new HeadingManager(new EfHeadingDal());
        WriterManager writerManager = new WriterManager(new EfWriterDal());
        public ActionResult Index()
        {
            // Toplam Kategori Sayısı
            var category = categoryManager.GetCategoryList().Count();
            ViewBag.Category = category;

            // Yazılım Kategorisine Ait Başlık Sayısı
            var software = headingManager.GetHeadingList().Where(x => x.Category.CategoryName == "Yazılım").Count();
            ViewBag.Software = software;

            // Yazar Adında 'a' Harfi Geçen Yazar Sayısı
            var writer = writerManager.GetWriterList().Where(x => x.WriterName.ToLower().Contains("a")).Count();
            ViewBag.Writer = writer;

            // En Fazla Başlığı Olan Kategori
            var heading = headingManager.GetHeadingList().Where(x => x.CategoryID == x.Category.CategoryID).GroupBy(x => x.Category.CategoryName).OrderByDescending(x => x.Count()).Select(x=>x.Key).FirstOrDefault();
            ViewBag.Heading = heading;

            // Kategori Tablosunda Durumu True Olanlar İle False Olanlar Arasındaki Sayısal Fark
            var categoryTrue = categoryManager.GetCategoryList().Where(x => x.CategoryStatus == true).Count();
            var categoryFalse = categoryManager.GetCategoryList().Where(x => x.CategoryStatus == false).Count();
            var categoryResult = categoryTrue - categoryFalse;
            ViewBag.CategoryResult = categoryResult;

            return View();
        }
    }
}