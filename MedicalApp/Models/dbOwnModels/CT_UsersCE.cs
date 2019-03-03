using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MedicalApp.Models.dbOwnModels
{
    public class CT_UsersCE
    {
       
        public int id { get; set; }
        public string UserName { get; set; }
        public Nullable<int> Rol { get; set; }
        public string Name { get; set; }
        public string BornDate { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }


        public virtual CT_Roles CT_Roles { get; set; }
    }


   
}