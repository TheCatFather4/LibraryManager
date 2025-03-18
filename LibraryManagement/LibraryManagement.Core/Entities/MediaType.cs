using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Core.Entities
{
    public class MediaType
    {
        public int MediaTypeID { get; set; }
        public string MediaTypeName { get; set; }

        public List<Media> Medias { get; set; }
    }
}
