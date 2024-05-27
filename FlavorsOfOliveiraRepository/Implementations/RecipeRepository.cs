using FlavorsOfOliveira.Data.Context;
using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Implementations
{
	public class RecipeRepository : IRecipeRepository
	{
		private readonly DbSet<Recipe> _dbSet;
		private readonly FlavorsOfOliveiraDBContext _flavorsOfOliveiraDBContext;
  
  
		public RecipeRepository(FlavorsOfOliveiraDBContext flavorsOfOliveiraDBContext)
		{
			_dbSet = flavorsOfOliveiraDBContext.Set<Recipe>();
			_flavorsOfOliveiraDBContext = flavorsOfOliveiraDBContext;
			
		}

		public Recipe Add(Recipe recipe)
		{
			_dbSet.Add(recipe);
			_flavorsOfOliveiraDBContext.SaveChanges();
			return recipe;
		}

		public List<Recipe> GetAll()
		{
			return _dbSet.Include(r => r.Ingredients)
                .ToList();
		}

		public Recipe GetById(int Id)
		{
			return _dbSet.FirstOrDefault(recipe => recipe.Id == Id);
		}

		public void Remove(Recipe recipe)
		{
			_dbSet.Remove(recipe);
			_flavorsOfOliveiraDBContext.SaveChanges();
		}

		public Recipe Update(Recipe recipe)
		{
			_dbSet.Update(recipe);
			_flavorsOfOliveiraDBContext.SaveChanges();
			return recipe;
		}

		public bool ExistsByTitle(string title)
		{
			bool exists = _dbSet.Any(r => r.Title == title);
			return exists;
		}

		public List<Recipe> GetPendingRecipes()
		{
			// procura todas as receitas com status "pendente de aprovação"
			return _dbSet.Where(r => !r.IsApprovedByAdmin)
                .Include(r => r.Ingredients)
                .ToList();
		}

		public Recipe GetByTitle(string title)
		{
			return _dbSet.FirstOrDefault(r => r.Title == title);
		}



	
	}
}
