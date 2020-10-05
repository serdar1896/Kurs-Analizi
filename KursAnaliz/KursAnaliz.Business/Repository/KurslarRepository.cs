using KursAnaliz.Data.DataContext;
using KursAnaliz.Data.Model;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace KursAnaliz.Business.Repository
{
    public class KurslarRepository : IBaseRepository<Kurslar>
    {
        private readonly KursContext _context = new KursContext();
        public int Count()
        {
            return _context.Kurslar.Count();
        }

        public void Delete(int id)
        {
            var kurs = GetById(id);
            if (kurs != null)
            {
                _context.Kurslar.Remove(kurs);
            }

        }    

        public IEnumerable<Kurslar> GetAll()
        {
            return _context.Kurslar.Select(x => x);
        }

        public Kurslar GetById(int id)
        {
            return _context.Kurslar.FirstOrDefault(x => x.KursId == id);
        }


        public void Insert(Kurslar obj)
        {
            obj.KursAdi = obj.KursAdi.ToUpper();
            _context.Kurslar.Add(obj);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Kurslar obj)
        {
            obj.KursAdi = obj.KursAdi.ToUpper();
            _context.Kurslar.AddOrUpdate(obj);
        }
    }
}
