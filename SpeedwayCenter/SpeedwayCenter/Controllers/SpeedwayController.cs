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
using SpeedwayCenter.Models.Entity_Framework;
using SpeedwayCenter.Models.Repository;
using Image = System.Drawing.Image;

namespace SpeedwayCenter.Controllers
{
    public class SpeedwayController : Controller
    {
        private readonly IRepository<Rider> _repository;

        public SpeedwayController(IRepository<Rider> repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        {
            var records = _repository.GetAll();
            return View(records);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        
        [HttpPost]
        public RedirectToRouteResult Add(Rider rider, HttpPostedFileBase file)
        {
            if (rider == null)
            {
                throw new ArgumentNullException("Rider is null");
            }
            if (file != null)
            {
                HttpServerUtilityBase server = HttpContext.Server;
                var serverPath = $"~/Photos/{rider.GetHashCode()}.png";
                var path = server.MapPath(serverPath);
                file.SaveAs(path);
                rider.Image = serverPath;
            }
            _repository.Add(rider);
            _repository.Save();
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Delete(int id)
        {
            var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
            if (entity != null)
            {
                RemovePhoto(entity);
                _repository.Delete(entity);
                _repository.Save();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
            return View(entity);
        }

        [HttpPost]
        public RedirectToRouteResult Edit(Rider rider, HttpPostedFileBase file, bool? remove)
        {
            if (remove == true)
            {
                RemovePhoto(rider);
                rider.Image = string.Empty;
            }
            if (file != null)
            {
                RemovePhoto(rider);
                var serverPath = $"~/Photos/{rider.GetHashCode()}.png";
                var path = HttpContext.Server.MapPath(serverPath);
                file.SaveAs(path);
                rider.Image = serverPath;
            }
            _repository.Edit(rider);
            _repository.Save();
            return RedirectToAction("Index");
        }

        private static void RemovePhoto(Rider rider)
        {
            if (System.IO.File.Exists(rider.Image))
            {
                System.IO.File.Delete(rider.Image);
            }
        }
    }
}