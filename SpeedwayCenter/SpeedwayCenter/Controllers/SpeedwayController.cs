using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SpeedwayCenter.Models.Entity_Framework;
using SpeedwayCenter.Models.Repository;

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
        public ActionResult Add(Rider rider)
        {
            _repository.Add(rider);
            _repository.Save();
            var records = _repository.GetAll();
            return View("Index", records);
        }
        
        public ActionResult Delete(int id)
        {
            var entity = _repository.FindBy(rider => rider.Id == id).FirstOrDefault();
            _repository.Delete(entity);
            _repository.Save();
            var records = _repository.GetAll();
            return View ("Index", records);
        }
    }
}