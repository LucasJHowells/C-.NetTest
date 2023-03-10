using System;
namespace BodyTracker.Models
{
	public class DailyWeight
	{

        public int id { get; set; }

        public DateOnly date { get; set; }

        public double weight { get; set; }

        public double lossgain { get; set; }

        public double totalloss { get; set; }

        public double target { get; set; }

        public double deviation { get; set; }

        public double bmi { get; set; }

        public DailyWeight()
		{
            this.totalloss = 173 - weight;

		}
	}
}

