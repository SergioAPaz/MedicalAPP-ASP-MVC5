using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.dbOwnModels
{
    public class TareasProgramadasCE
    {
        public int id { get; set; }
        public Nullable<System.DateTime> FechaDeCreacion { get; set; }
        public int Asignador { get; set; }
        [Required]
        public string TituloTarea { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Descripcion { get; set; }
        public int Asignado { get; set; }
        public System.DateTime FechaDeProximoEventoJustOneEvent { get; set; }
        public System.DateTime FechaDeProximoEventoMultipleEvents { get; set; }
        public string FrecuenciaEventos { get; set; }
        public string FrecuenciaRecurrencia { get; set; }
        public string Finalizada { get; set; }

        public virtual CT_Users CT_Users { get; set; }
        public virtual CT_Users CT_Users1 { get; set; }
    }
}