using System;
namespace API.Models
{
    public class Service
    {
        public int id { get; set; }
        public DateTime heureLivraison { get; set; }
        public int Statut { get; set; }
        public int chambreId { get; set; }
    }
}