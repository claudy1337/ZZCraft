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
    
    public partial class UserInvent
    {
        public int id { get; set; }
        public Nullable<int> idCraftDrop { get; set; }
        public Nullable<int> idInitialDrop { get; set; }
    
        public virtual CraftDrop CraftDrop { get; set; }
    }
}
