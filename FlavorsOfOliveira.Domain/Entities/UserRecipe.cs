using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Domain.Entities
{
	public class UserRecipe
	{
   public int Id { get; set; }

   public User User { get; set; }
   public Recipe Recipe { get; set; }
   public int UserId { get; set;}
   public int RecipeId { get; set; }
   public decimal Rating { get; set; }
   public string Comments { get; set; }


	}
}
