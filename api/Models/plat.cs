using System;
namespace API.Models
{
	public class Plat
	{
        public int Id { get; set; }
        public int IdCondiment { get; set; }
        public string nom { get; set; }
        public int prix { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
    }
}

