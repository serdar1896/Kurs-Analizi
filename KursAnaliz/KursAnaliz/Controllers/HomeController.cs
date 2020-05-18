using KursAnaliz.Business.Repository;
using System.Linq;
using System.Web.Mvc;

namespace KursAnaliz.Controllers
{
    public class HomeController : Controller
    {
        private readonly KurslarRepository _kurslarRepository;
        private readonly KursiyerlerRepository _kursiyerlerRepository;
        public HomeController(KurslarRepository kurslarRepository, KursiyerlerRepository kursiyerlerRepository)
        {
            _kurslarRepository = kurslarRepository;
            _kursiyerlerRepository = kursiyerlerRepository;
        }

        public ActionResult Index()
        {
            var count = _kursiyerlerRepository.GetAll().ToList().Count();
            ViewBag.ToplamKursiyer = count;
            var kursList = _kurslarRepository.GetAll().ToList();
            return View(kursList);
        }

    }
}