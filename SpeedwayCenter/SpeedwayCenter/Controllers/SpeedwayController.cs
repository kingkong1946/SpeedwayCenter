using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
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
        public ActionResult Add(Rider rider, HttpPostedFileBase file)
        {
            if (rider == null)
            {
                throw new ArgumentNullException("Rider is null");
            }
            if (file != null)
            {
                //    var formatPosition = file.FileName.IndexOf('.');
                //    var format = file.FileName.Substring(formatPosition);
                //    var path = HttpContext.Server.MapPath("~/Photos/" + rider.Id + format);
                var serverPath = $"~/Photos/{rider.GetHashCode()}.png";
                HttpServerUtilityBase server = HttpContext.Server;
                var path = server.MapPath(serverPath);
                file.SaveAs(path);
                //using (var image = Image.FromStream(file.InputStream))
                //{
                //    image.Save(path, ImageFormat.Png);
                //}
                rider.Image = serverPath;
            }
            _repository.Add(rider);
            _repository.Save();
            var records = _repository.GetAll();
            return View("Index", records);
        }

        public ActionResult Delete(int id)
        {
            var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
            if (entity == null)
            {
                var records = _repository.GetAll(); // TODO the same code in two lines - refactor
                return View("Index", records);
            }
            //else // TODO Uncommnet when error page is completed
            {
                if (System.IO.File.Exists(entity.Image))
                {
                    System.IO.File.Delete(entity.Image);
                }
                _repository.Delete(entity);
                _repository.Save();
                var records = _repository.GetAll();
                return View("Index", records);
            }
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
        public ActionResult Edit(Rider rider, HttpPostedFileBase file)
        {
            if (file != null)
            {
                //TODO Put this code to privates methods
                if (System.IO.File.Exists(rider.Image))
                {
                    System.IO.File.Delete(rider.Image);
                }
                var serverPath = $"~/Photos/{rider.GetHashCode()}.png";
                var Path = HttpContext.Server.MapPath(serverPath);
                file.SaveAs(Path);
                rider.Image = serverPath;
            }
            _repository.Edit(rider);
            _repository.Save();
            var records = _repository.GetAll();
            return View("Index", records);
        }
    }
}