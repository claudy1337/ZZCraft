//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZZCraft.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CraftRes
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CraftRes()
        {
            this.CraftDrop = new HashSet<CraftDrop>();
        }
    
        public int id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public int idRecipe { get; set; }
        public string img { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CraftDrop> CraftDrop { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
