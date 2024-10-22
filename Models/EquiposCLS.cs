using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppUgel.Models
{
    public class EquiposCLS
    {
        public int IdEquipo { get; set; }

        public string NombreEqui { get; set; }

        public string TipoEqui { get; set; }

        public string SerieEqui { get; set; }

        public string MarcaEqui { get; set; }

        public string ModeloEqui { get; set; }

        public string AreaEqui { get; set; } 

        public DateTime? FechaAdquisicion { get; set; }

        public string EstadoEqui { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
