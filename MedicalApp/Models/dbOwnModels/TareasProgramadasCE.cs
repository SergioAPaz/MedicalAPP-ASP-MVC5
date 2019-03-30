using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.dbOwnModels
{
    public class TareasProgramadasCE
    {
        public int id { get; set; }
        public Nullable<System.DateTime> FechaDeCreacion { get; set; }
        public int Asignador { get; set; }
        public string TituloTarea { get; set; }
        public string Descripcion { get; set; }
        public int Asignado { get; set; }
        public Nullable<System.DateTime> FechaDeProximoEvento { get; set; }
        public string Frecuencia { get; set; }
        public string Finalizada { get; set; }

        public virtual CT_Users CT_Users { get; set; }
        public virtual CT_Users CT_Users1 { get; set; }
    }
}