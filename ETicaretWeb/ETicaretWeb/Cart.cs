//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETicaretWeb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Cost { get; set; }
        public int Count { get; set; }
    
        public virtual Product Product { get; set; }
        public virtual User User { get; set; }
    }
}
