using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Model
    {
        [Required]
        public string ReservedName { get; set; }
        [Required]
        public string ConcertName { get; set; }
    }

    public class WordModel
    {
        public string Word { get; set; }
        public int? Order { get; set; }

    }
}
