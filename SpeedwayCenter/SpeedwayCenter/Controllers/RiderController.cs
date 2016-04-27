using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Ninject.Infrastructure.Language;
using SpeedwayCenter.ORM.Models;
using SpeedwayCenter.ORM.Repository;
using SpeedwayCenter.ViewModels.Rider;

namespace SpeedwayCenter.Controllers
{
    public class RiderController : Controller
    {
        private readonly IQueryRepository<Rider> _repository;
        private const int Take = 10;

        public RiderController(IQueryRepository<Rider> repository)
        {
            _repository = repository;
        }

        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var records = _repository
                .GetAll()
                .Select(r => new
                {
                    r.Name,
                    r.Forname,
                    r.BirthDate,
                    r.Country
                }).ToList();

            if (!string.IsNullOrEmpty(searchValue))
            {
                records = records.Where(a => $"{a.Name} {a.Forname}".ToLower().Contains(searchValue.ToLower())).ToList();
            }

            var viewModel = records.Skip((page - 1) * Take).Take(Take).Select(r => new RiderIndexViewModel(
                    r.Name,
                    r.Forname,
                    r.BirthDate.ToShortDateString(),
                    r.Country))
            .ToEnumerable();

            ViewBag.NumberOfPages = (int)Math.Ceiling((decimal)records.Count / Take);
            ViewBag.Page = page;

            return View(viewModel);
        }

        //[HttpGet]
        //public ActionResult Add()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public RedirectToRouteResult Add(Rider rider, HttpPostedFileBase file)
        //{
        //    if (rider == null)
        //    {
        //        throw new ArgumentNullException("Rider is null");
        //    }
        //    if (file != null)
        //    {
        //        HttpServerUtilityBase server = HttpContext.Server;
        //        var serverPath = $"~/Photos/{rider.GetHashCode()}.png";
        //        var path = server.MapPath(serverPath);
        //        file.SaveAs(path);
        //        rider.Image = serverPath;
        //    }
        //    _repository.Add(rider);
        //    _repository.Save();
        //    return RedirectToAction("Index");
        //}

        //public RedirectToRouteResult Delete(int id)
        //{
        //    var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
        //    if (entity != null)
        //    {
        //        RemovePhoto(entity);
        //        _repository.Delete(entity);
        //        _repository.Save();
        //    }
        //    return RedirectToAction("Index");
        //}

        //public ActionResult Details(int id)
        //{
        //    var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
        //    return View(entity);
        //}

        //[HttpGet]
        //public ActionResult Edit(int id)
        //{
        //    var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
        //    return View(entity);
        //}

        //[HttpPost]
        //public RedirectToRouteResult Edit(Rider rider, HttpPostedFileBase file, bool? remove)
        //{
        //    if (remove == true)
        //    {
        //        RemovePhoto(rider);
        //        rider.Image = string.Empty;
        //    }
        //    if (file != null)
        //    {
        //        RemovePhoto(rider);
        //        var serverPath = $"~/Photos/{rider.GetHashCode()}.png";
        //        var path = HttpContext.Server.MapPath(serverPath);
        //        file.SaveAs(path);
        //        rider.Image = serverPath;
        //    }
        //    _repository.Edit(rider);
        //    _repository.Save();
        //    return RedirectToAction("Index");
        //}

        //private static void RemovePhoto(Rider rider)
        //{
        //    if (System.IO.File.Exists(rider.Image))
        //    {
        //        System.IO.File.Delete(rider.Image);
        //    }
        //}
    }
}