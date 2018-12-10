using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeTalk.Models
{
    public class Talk
    {
        public int ProductID { get; set; }
        public string CoffeeName { get; set; }
        public string PicUrl { get; set; }
        public string Description { get; set; }
    }
}
