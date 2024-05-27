using FlavorsOfOliveira.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using FlavorsOfOliveira.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlavorsOfOliveira.Domain.Entities.Recipe;
using System.Reflection.Emit;

namespace FlavorsOfOliveira.Data.Context
{
	public class FlavorsOfOliveiraDBContext : DbContext
	{
		public DbSet<Admin> Admins { get; set; }
		public DbSet<User> Users { get; set; }
		public DbSet<Recipe> Recipes { get; set; }
		public DbSet<UserRecipe> UserRecipes { get; set; }
		public DbSet<Ingredient> Ingredients { get; set; }
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{


			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("server=localhost; database=FlavorsOfOliveiraDB; integrated security=true;",
																				b => b.MigrationsAssembly("FlavorsOfOliveira"));
			}
		}
  
	

	}
}