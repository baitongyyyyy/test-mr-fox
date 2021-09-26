using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ShowDate { get; set; }
        public string Location { get; set; }
    }
}
