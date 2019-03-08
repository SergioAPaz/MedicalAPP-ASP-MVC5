using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.dbOwnModels
{
    public class ViewModel
    {
        public Tareas TareasFC { get; set; }
        public IEnumerable<MedicalApp.Models.Tareas> TareasIE { get; set; }
       
        

    }
}