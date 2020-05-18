using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KursAnaliz.Data.Model
{
    public class Kurslar
    {
        [Key]
        public int KursId { get; set; }
        [Required(ErrorMessage = "{0} Bos gecilemez")]
        [DisplayName("Kurs Adı")]
        public string KursAdi { get; set; }
        public virtual ICollection<Kursiyerler> Kursiyerler { get; set; }
    }
}
