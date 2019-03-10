using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedicalApp.Models.dbOwnModels
{
    public class ViewModel
    {
        public TareasCE TareasFC { get; set; }
        public IEnumerable<MedicalApp.Models.Tareas> TareasIE { get; set; }


        public string FulluserName { get; set; }
        public string UserName { get; set; }
        public int PkUser { get; set; }
    }
}