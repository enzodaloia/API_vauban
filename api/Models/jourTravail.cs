using System;
namespace API.Models
{
	public class jourTravail
	{
        public int Id { get; set; }
        public int user_Id { get; set; }
        public DateTime work_day_start { get; set; }
        public DateTime work_day_end { get; set; }
    }
}

