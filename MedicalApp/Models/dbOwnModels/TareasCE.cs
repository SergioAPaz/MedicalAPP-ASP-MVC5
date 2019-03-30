using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.dbOwnModels
{
    public class TareasCE
    {

        public int id { get; set; }
        public Nullable<System.DateTime> Fecha { get; set; }
        public int Asignador { get; set; }
        [Required]
        public string TituloTarea { get; set; }

        [DataType(DataType.MultilineText)]
        [Required]
        public string Descripcion { get; set; }
        public int Asignado { get; set; }

        [Required]
        public Nullable<System.DateTime> FechaLimite { get; set; }
        public Nullable<int> Vencida { get; set; }
        public Nullable<int> Terminada { get; set; }
        public Nullable<System.DateTime> FechaDeTermino { get; set; }
        public string Adjunto { get; set; }
        public string ComentarioDeCierre { get; set; }
        public string EvidenciaDeCierreAdjunto { get; set; }

        public virtual CT_Users CT_Users { get; set; }
        public virtual CT_Users CT_Users1 { get; set; }

    }
}