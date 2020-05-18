using KursAnaliz.Business.Repository;
using KursAnaliz.Data.Model;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KursAnaliz.Controllers
{
    public class KursiyerController : Controller
    {
        private readonly KursiyerlerRepository _kursiyerlerRepository;
        private readonly KurslarRepository _kurslarRepository;

        public KursiyerController(KursiyerlerRepository kursiyerlerRepository, KurslarRepository kurslarRepository)
        {
            _kursiyerlerRepository = kursiyerlerRepository;
            _kurslarRepository = kurslarRepository;
        }

        public ActionResult Index()
        {
            var kursiyerList = _kursiyerlerRepository.GetAll().ToList();
         
            return View(kursiyerList);
        }
        public ActionResult Create()
        {
            SetKursList();
            DateTime data = DateTime.Today;
            ViewBag.Bugun = data;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kursiyerler kursiyer)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            _kursiyerlerRepository.Insert(kursiyer);
            _kursiyerlerRepository.Save();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kursiyer = _kursiyerlerRepository.GetById(id.Value);
            if (kursiyer == null)
            {
                return HttpNotFound();
            }
            SetKursList(kursiyer.KursId);
        
            return View(kursiyer);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Kursiyerler kursiyer)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _kursiyerlerRepository.Update(kursiyer);
            _kursiyerlerRepository.Save();

           
            return RedirectToAction("Index");
        }
        private void SetKursList(object kurs = null)
        {
            var kursList = _kurslarRepository.GetAll().ToList();
            var selectList = new SelectList(kursList, "KursId", "KursAdi", kurs);
            ViewData.Add("KursId", selectList); 

        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kursiyer = _kursiyerlerRepository.GetById(id.Value);
            if (kursiyer == null)
            {
                return HttpNotFound();
            }
            return View(kursiyer);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kursiyer = _kursiyerlerRepository.GetById(id.Value);
            if (kursiyer == null)
            {
                return HttpNotFound();
            }
            return View(kursiyer);
        }
        [HttpPost, ValidateAntiForgeryToken, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _kursiyerlerRepository.Delete(id);
            _kursiyerlerRepository.Save();
            return RedirectToAction("Index");
        }
    }
}