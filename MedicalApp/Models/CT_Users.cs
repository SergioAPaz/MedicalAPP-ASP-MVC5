//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MedicalApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CT_Users
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public Nullable<int> Rol { get; set; }
        public string Name { get; set; }
        public string BornDate { get; set; }
        public string Password { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
    
        public virtual CT_Roles CT_Roles { get; set; }
    }
}
