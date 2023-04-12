using System;
using System.Collections.Generic;
using System.Text;
using SQLite;


namespace LRAD012023.Models
{
    public class Contactos
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [MaxLength(100)]
        public string nombres { get; set; }
        [MaxLength(8)]
        public string telefono { get; set; }

        [MaxLength(100)]
        public string apellidos { get; set;}
        public string sexo { get; set; }
        [MaxLength(2)]
        public string edad { get; set; }
        [MaxLength(800)]
        public string nota { get; set; }

        [MaxLength(300)]
        public string pais { get; set;}
        public String foto { get; set; }
    }
}
