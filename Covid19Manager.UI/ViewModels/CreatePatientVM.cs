using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Covid19Manager.UI.ViewModels
{
    public class CreatePatientVM
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [RegularExpression(@"\d{11}", ErrorMessage = "OIB mora sadržavati 11 znamenki.")]
        public long OIB { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [DisplayName("Ime")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [DisplayName("Prezime")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [DisplayName("Adresa samoizolacije")]
        public string IsolationAddress { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [Range(-90, 90, ErrorMessage = "Latituda mora biti između -90 i 90.")]
        [DisplayName("Latituda")]
        public double? IsolationLat { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [Range(-180, 180, ErrorMessage = "Longituda mora biti između -180 i 180.")]
        [DisplayName("Longituda")]
        public double? IsolationLong { get; set; }

    }
}
