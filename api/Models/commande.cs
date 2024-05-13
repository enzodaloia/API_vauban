using System;
namespace API.Models
{
	public class Commande
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MenuId { get; set; }
        public int statut { get; set; }
        public DateTime createdAt { get; set; }
    }
}

