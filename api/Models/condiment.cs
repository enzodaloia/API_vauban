using System;
namespace API.Models
{
	public class Condiment
	{
        public int Id { get; set; }
        public int IdPlat { get; set; }
        public string nom { get; set; }
        public int quantite { get; set; }
        public DateTime createdAt { get; set; }
    }
}