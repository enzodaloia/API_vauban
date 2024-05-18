using System;
namespace API.Models
{
	public class Contact
	{
        public int id { get; set; }
        public string email { get; set; }
        public string sujet { get; set; }
        public string description { get; set; }
        public string lastname { get; set; }
        public string firstname { get; set; }
        public DateTime createdAt { get; set; }
        public int severite { get; set; }
    }
}

