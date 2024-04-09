using FlavorsOfOliveira.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class RecipeController : ControllerBase
	{
		[HttpGet]
		public List<Recipe> GetAll()
		{
			return GetAll();
		}

		[HttpGet("{Id}")]
		public Recipe GetById(int Id)
		{
			return GetById(Id);
		}

		[HttpPost]
		public Recipe Save(Recipe recipe)
		{
			return Save(recipe);
		}

		[HttpDelete("{Id}")]
		public void Remove(int Id)
		{
			Remove(Id);
		}
	}
}
