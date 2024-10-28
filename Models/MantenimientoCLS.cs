using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AppUgel.Models
{
    public class MantenimientoCLS
    {
        public int idManteni {  get; set; }
        public int idEquipo { get; set; }
        public int idUser { get; set; }

        public string tipoManteni { get; set; }
        public string descriManteni { get; set; }
        public DateTime fechaMantei { get; set; }
        public float costoManten {  get; set; }
        

        //Datos llamados del Equipo
        
        public EquiposCLS Equipo { get; set; }

        public string NombreEqui { get; set; }
        public string SerieEqui { get; set; }
        public string AreaEqui { get; set; }

        ////Datos de Usuario
        //public string nombreUser {  get; set; }
    }
}
