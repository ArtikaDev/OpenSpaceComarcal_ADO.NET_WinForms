//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OpenSpaceComarcal.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class inscripcion
    {
        public int id { get; set; }
        public int id_alumno { get; set; }
        public int id_instancia { get; set; }
        public Nullable<System.DateTime> fecha_inscripcion { get; set; }
        public Nullable<System.DateTime> fecha_expedicion { get; set; }
        public Nullable<bool> apto { get; set; }
        public string cod_factura { get; set; }
        public Nullable<int> id_empresa { get; set; }
    
        public virtual alumno alumno { get; set; }
        public virtual instancia instancia { get; set; }
        public virtual empresa empresa { get; set; }
    }
}
