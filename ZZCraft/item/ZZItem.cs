using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZZCraft.item
{
    class ZZItem
    {
        public ZZItem(string name, string photo)
        { 
            Name = name;
            Photo = photo;
        }
        public string Name { get; set; }

        public string Photo { get; set; }
    }
}
