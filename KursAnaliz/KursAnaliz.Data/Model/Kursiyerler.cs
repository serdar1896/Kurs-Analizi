using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KursAnaliz.Data.Model
{
    public class Kursiyerler
    {
        [Key]
        public int KursiyerId { get; set; }

        [Required(ErrorMessage = "{0} Bos gecilmez")]       
        public string KursiyerAd { get; set; }

        [Required(ErrorMessage = "{0} Bos gecilmez")]
        public string KursiyerSoyad { get; set; }


        [DataType(DataType.Date)]
        [DisplayName("Katılım Tarihi")]
        public DateTime KatilimTarihi { get; set; }


        [Required]
        [DisplayName("Kurs Adı")]
        public int KursId { get; set; }
        public virtual Kurslar Kurslar { get; set; }
    }
}
