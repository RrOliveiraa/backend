using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Domain.Entities
{
    public class User
    {
      public int Id { get; set; }
      public string Name { get; set; }
      public string Email { get; set; }
      
      [Column]
      public string UserName { get; set; }
      public string Password { get; set; }
		    public bool IsBlocked { get; set; }
		    public List<Recipe> FavoriteRecipes { get; set; }

	}
}
