using System;
namespace API.Models
{
	public class Menu
	{
        public int Id { get; set; }
        public int prix { get; set; }
        public string description { get; set; }
        public DateTime createdAt { get; set; }
    }
}

