using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FlavorsOfOliveira.Domain.Entities.Recipe;

namespace FlavorsOfOliveira.Domain.Entities
{
	public class Recipe
	{
		public int Id { get; set; }

		public int UserId { get; set; }
  public string UserName { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string Difficulty { get; set; }

		public string Duration { get; set; }
		public bool IsApprovedByAdmin { get; set; }
		public List<Ingredient> Ingredients { get; set; }






	}
}



	

