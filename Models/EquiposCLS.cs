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
        public string NombreEqui { get; set; } = null!;
        public string TipoEqui { get; set; } = null!;
        public string SerieEqui { get; set; } = null!;
        public string MarcaEqui { get; set; } = null!;
        public string ModeloEqui { get; set; } = null!;
        public string AreaEqui { get; set; } = null!;
        public DateTime? FechaAdquisicion { get; set; }
        public string EstadoEqui { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
    }
}
