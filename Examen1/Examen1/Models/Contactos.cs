using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Examen1.Models
{
    [Table("Contactos")]
    public class Contactos
    { 
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public string nombres { get; set; }

        [MaxLength(100)]
        public string apellidos { get; set; }

        [NotNull, Unique]
        public string telefono { get; set; }

        [NotNull]
        public int edad { get; set; }


        [NotNull]
        public string pais { get; set; }

        
        public string nota { get; set; }

        public double latitud { get; set; }

        public double longitud { get; set; }

        public byte[] foto { get; set; }
        
    }
}
