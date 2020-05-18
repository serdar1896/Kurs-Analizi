using KursAnaliz.Data.Model;
using System.Collections.Generic;

namespace KursAnaliz.Views.ViewModel
{
    public class KursPageModel
    {
        public Kurslar Kurs { get; set; }
        public IEnumerable<int> Sayi { get; set; }
        public IEnumerable<string> Tarih { get; set; }
    }
}