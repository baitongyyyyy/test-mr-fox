using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ConcertTicket
    {
        public int Id { get; set; }
        public int? ConcertId { get; set; }
        public int? StatusId { get; set; }
        public DateTime? ReservedDate { get; set; }
        public string ReservedBy { get; set; }
    }
}
