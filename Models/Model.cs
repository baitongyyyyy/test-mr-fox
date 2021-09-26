using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Model
    {
        public string ReservedName { get; set; }
        public string ConcertName { get; set; }
    }

    public class WordModel
    {
        public string Word { get; set; }
        public int? Order { get; set; }

    }
}
