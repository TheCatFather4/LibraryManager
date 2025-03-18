using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class TopMediaItem
    {
        public int MediaID { get; set; }
        public string Title { get; set; }
        public int CheckoutCount { get; set; }
    }
}
