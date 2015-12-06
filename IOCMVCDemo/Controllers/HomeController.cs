using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IOCMVCDemo.Repository;
using IOCMVCDemo.Models;

namespace IOCMVCDemo.Controllers
{
    public class HomeController : Controller
    {

        private readonly IGoldMedalWinnersRepository _repository;

        public HomeController() { }

        public HomeController(IGoldMedalWinnersRepository repository)
        {
            _repository = repository;
        }

        //
        // GET: /Home/

        public ActionResult Index(GoldMedalWinnersModel model)
        {
            ViewBag.Message = "Gold Medal Winners";
            model.Winners = _repository.GetAll();
            return View(model);
        }

    }
}
