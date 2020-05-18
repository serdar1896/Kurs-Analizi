using KursAnaliz.Business.Infrastructure;
using KursAnaliz.Data.DataContext;
using KursAnaliz.Data.Model;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace KursAnaliz.Business.Repository
{
    public class KursiyerlerRepository : IBaseRepository<Kursiyerler>

        {
            private readonly KursContext _context = new KursContext();
            public int Count()
            {
                return _context.Kursiyerler.Count();
            }

            public void Delete(int id)
            {
                var kursiyer = GetById(id);
                if (kursiyer != null)
                {
                    _context.Kursiyerler.Remove(kursiyer);
                }


            }

            public IEnumerable<Kursiyerler> GetAll()
            {
                return _context.Kursiyerler.Select(x => x);
            }

            public Kursiyerler GetById(int id)
            {
                return _context.Kursiyerler.FirstOrDefault(x => x.KursiyerId == id);
            }


            public void Insert(Kursiyerler obj)
            {
                obj.KursiyerAd = obj.KursiyerAd.ToUpper();
                obj.KursiyerSoyad = obj.KursiyerSoyad.ToUpper();
                _context.Kursiyerler.Add(obj);
            }

            public void Save()
            {
                _context.SaveChanges();
            }

            public void Update(Kursiyerler obj)
            {
                obj.KursiyerAd = obj.KursiyerAd.ToUpper();
                obj.KursiyerSoyad = obj.KursiyerSoyad.ToUpper();
                _context.Kursiyerler.AddOrUpdate(obj);
            }
        }
    }
