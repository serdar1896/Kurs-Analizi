using KursAnaliz.Business.Repository;
using KursAnaliz.Data;
using KursAnaliz.Data.Model;
using KursAnaliz.Views.ViewModel;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace KursAnaliz.Controllers
{
    public class KursController : Controller
    {
    
        private readonly KurslarRepository _kurslarRepository;

        private readonly KursiyerlerRepository _kursiyerlerRepository;
        public KursController(KurslarRepository kurslarRepository, KursiyerlerRepository kursiyerlerRepository)
        {
            _kurslarRepository = kurslarRepository;
            _kursiyerlerRepository = kursiyerlerRepository;
        }


        public ActionResult Index()
        {
            var kurslist = _kurslarRepository.GetAll().ToList();
            var kursiyertoplam = _kursiyerlerRepository.GetAll().ToList().Count;

            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach (Kurslar item in kurslist)
            {
                int kurskursiyer = _kursiyerlerRepository.GetAll().Where(x => x.KursId == item.KursId).ToList().Count;
                dataPoints.Add(new DataPoint(item.KursAdi, (double)(100 * kurskursiyer) / kursiyertoplam));
            }

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View(kurslist);
        }
 
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kurslar kurslar)
        {
            if (!ModelState.IsValid)
            {
                return View(kurslar);
            }
            _kurslarRepository.Insert(kurslar);
            _kurslarRepository.Save();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kurs = _kurslarRepository.GetById(id.Value);
            if (kurs == null)
            {
                return HttpNotFound();
            }
            return View(kurs);
        }
        [HttpPost]
        public ActionResult Edit(Kurslar kurs)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _kurslarRepository.Update(kurs);
            _kurslarRepository.Save();
            return RedirectToAction("Index");
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kurs = _kurslarRepository.GetById(id.Value);
            if (kurs == null)
            {
                return HttpNotFound();
            }
            var kursiyer = _kursiyerlerRepository.GetAll().Where(x => x.KursId == id.Value).OrderBy(y=>y.KatilimTarihi).ToList();
            var tarih = kursiyer.Select(x => x.KatilimTarihi).Distinct();
            int[] sayi = new int[tarih.Count()];
            int i = 0;
            string[] t = new string[tarih.Count()];
            List<DataPoint> dataPoints1 = new List<DataPoint>();
            foreach (var item in tarih)
            {
                sayi[i] = kursiyer.Where(y => y.KatilimTarihi == item).Count();
                t[i] = item.ToShortDateString().ToString();
                dataPoints1.Add(new DataPoint(item.ToShortDateString().ToString(), kursiyer.Where(y => y.KatilimTarihi == item).Count()));
                i++;

            }
            ViewBag.DataPoints1 = JsonConvert.SerializeObject(dataPoints1);

            var pageModel = new KursPageModel
            {
                Kurs = kurs,
                Tarih = t,
                Sayi = sayi,


            };

            return View(pageModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var kurs = _kurslarRepository.GetById(id.Value);
            if (kurs == null)
            {
                return HttpNotFound();
            }
            return View(kurs);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _kurslarRepository.Delete(id);
            _kurslarRepository.Save();
            return RedirectToAction("Index");
        }
    }
}

