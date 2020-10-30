﻿using Data.ViewModel;
using System.Data.Entity;

namespace Repository
{
	public class LGAContext : DbContext
	{
		public LGAContext()
			// Passing connection string exists in presentation web config file
			: base("StringDBContext")
		{
			Database.SetInitializer(new Seeding());
        }

        public virtual DbSet<State> States { get; set; }
		public virtual DbSet<Location> Locations { get; set; }
		public virtual DbSet<Score> Scores { get; set; }
		public virtual DbSet<ScoreDetail> ScoreDetails { get; set; }
	}
}

