using System;
namespace BodyTracker.Models
{
	public class StarterWeight
	{

		public int id { get; set; }

		public DateOnly startdate { get; set; }

		public double startweight { get; set; }

		public double startbmi { get; set; }

		public double targetweight { get; set; }

		public int height { get; set; }

		public StarterWeight()
		{
		}
	}
}

