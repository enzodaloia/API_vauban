using System;
namespace API.Models
{
	public class Facturation
	{
        public int Id { get; set; }
        public int numeroFacture { get; set; }
        public int UserId { get; set; }
        public int prix { get; set; }
        public DateTime createdAt { get; set; }
    }
}