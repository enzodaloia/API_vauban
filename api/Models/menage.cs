using System;
namespace API.Models
{
    public class Menage
    {
        public int id { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int Statut { get; set; }
        public int chambreId { get; set; }
    }
}

