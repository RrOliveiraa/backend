 using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlavorsOfOliveira.Repository.Interfaces;
using FlavorsOfOliveira.Repository.Implementations;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class AdminController : ControllerBase
	{
		private readonly FlavorsOfOliveiraDBContext _context;
		private readonly IUserRepository _userRepository;
  private readonly IRecipeRepository _recipeRepository;

		public AdminController(FlavorsOfOliveiraDBContext context, IUserRepository userRepository, IRecipeRepository recipeRepository)
		{
			_context = context;
			_userRepository = userRepository;
			_recipeRepository = recipeRepository;
		}




		[HttpGet]
		public List<Admin> GetAll()
		{
			return GetAll();
		}

		[HttpGet("{Id}")]
		public Admin GetById(int Id)
		{
			return GetById(Id);
		}

		[HttpPost]
		public Admin Save(Admin admin)
		{
			return Save(admin);
		}

		[HttpDelete("{Id}")]
		public void Remove(int Id)
		{
			Remove(Id);
		}


		[HttpPost("Login")]
		
		public IActionResult Login([FromBody] Admin admin)
		{
			var authenticatedAdmin = _context.Admins.SingleOrDefault(a => a.UserName == admin.UserName && a.Password == admin.Password);

			if (authenticatedAdmin != null)
			{
				// Admin autenticado com sucesso
				return Ok("Login successfully! Welcome!");
			}

			// Credenciais inválidas
			return Unauthorized("Login failed, try again!");
		}

		[Authorize(Roles = "Admin")]
		[HttpGet("Users")]
		

		public IActionResult GetRegisteredUsers()
		{
			var users = _userRepository.GetAll();

			return Ok(users);
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("Users/{userId}/Block")]
		

		public IActionResult BlockUser(int userId)
		{
			var user = _userRepository.GetById(userId);
			if (user == null)
			{
				return NotFound($"User with ID {userId} not found.");
			}

			user.IsBlocked = true;
			_userRepository.Update(user);

			return Ok($"User with ID {userId} has been blocked.");
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("Recipes/{recipeId}/Approve")]
		

		public IActionResult ApproveRecipe(int recipeId)
		{
			var recipe = _recipeRepository.GetById(recipeId);
			if (recipe == null)
			{
				return NotFound();
			}

			recipe.IsApprovedByAdmin = true; // Aprova a receita
			_recipeRepository.Update(recipe);

			return Ok($"Recipe with ID {recipeId} has been approved.");
		}

		[Authorize(Roles = "Admin")]
		[HttpPut("Recipes/{recipeId}/Reject")]
		

		public IActionResult RejectRecipe(int recipeId)
		{
			var recipe = _recipeRepository.GetById(recipeId);
			if (recipe == null)
			{
				return NotFound();
			}

			recipe.IsApprovedByAdmin = false; // Rejeita a receita
			_recipeRepository.Update(recipe);

			return Ok($"Recipe with ID {recipeId} has been rejected.");
		}


		[Authorize(Roles = "Admin")]
		[HttpPut("EditRecipe/{recipeId}")]
		

		public IActionResult EditRecipe(int recipeId, [FromBody] Recipe updatedRecipe)
		{
			// Verifique se a receita existe
			var existingRecipe = _recipeRepository.GetById(recipeId);
			if (existingRecipe == null)
			{
				return NotFound($"Recipe with ID {recipeId} not found");
			}
			else
			{

				// Atualize os detalhes da receita existente com os novos detalhes
				existingRecipe.Title = updatedRecipe.Title;
				existingRecipe.Description = updatedRecipe.Description;
				existingRecipe.Ingredients = updatedRecipe.Ingredients;
				existingRecipe.Duration = updatedRecipe.Duration;
				existingRecipe.Difficulty = updatedRecipe.Difficulty;
				existingRecipe.IsApprovedByAdmin = updatedRecipe.IsApprovedByAdmin; // Talvez o admin queira alterar isso

				// Salve as alterações no repositório
				_recipeRepository.Update(existingRecipe);

				return Ok("Recipe updated successfully");

			}
		}
  
	



	}
}
  


