//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace test.GitHub.Entity_Framework.trunk.Entity_Framework_Basics.Problem_03
{
    using System;
    using System.Collections.Generic;
    
    public partial class WorkHour
    {
        public int id { get; set; }
        public Nullable<int> employeeID { get; set; }
        public Nullable<System.DateTime> onDate { get; set; }
        public string task { get; set; }
        public Nullable<int> hours { get; set; }
        public string comments { get; set; }
    
        public virtual Employee Employee { get; set; }
    }
}
