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



		[HttpPost]
		public Admin Save(Admin admin)
		{
			return Save(admin);
		}

		[HttpDelete("{Id}")]
		public IActionResult Remove(int Id)
		{
			// Verifica se o usuário com o ID fornecido existe
			var user = _userRepository.GetById(Id);
			if (user == null)
			{
				return NotFound("User not found");
			}

			try
			{
				// Remove o usuário
				_userRepository.Remove(user);
				return Ok("User removed successfully");
			}
			catch (Exception ex)
			{
				// Handle exception
				return StatusCode(500, "Failed to remove user");
			}
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

		
		[HttpGet("Users")]
		

		public IActionResult GetRegisteredUsers()
		{
			var users = _userRepository.GetAll();

			return Ok(users);
		}



		[HttpPost("BlockUser")]
		public IActionResult BlockUser(int Id)
		{
			var user = _userRepository.GetById(Id);
			if (user == null)
			{
				return NotFound($"User with ID {Id} not found.");
			}

			user.IsBlocked = true;
			_userRepository.Update(user);

			return Ok($"User with ID {Id} has been blocked.");
		}



		[HttpPost("AproveRecipe")]
		public IActionResult ApproveRecipe(int Id)
		{
			var recipe = _recipeRepository.GetById(Id);
			if (recipe == null)
			{
				return NotFound();
			}

			recipe.IsApprovedByAdmin = true; // Aprova a receita
			_recipeRepository.Update(recipe);

			return Ok($"Recipe with ID {Id} has been approved.");
		}



		[HttpPost("RejectRecipe")]
		public IActionResult RejectRecipe(int Id)
		{
			var recipe = _recipeRepository.GetById(Id);
			if (recipe == null)
			{
				return NotFound();
			}

			recipe.IsApprovedByAdmin = false; // Rejeita a receita
			_recipeRepository.Update(recipe);

			return Ok($"Recipe with ID {Id} has been rejected.");
		}


		
		
  [HttpPost("EditRecipe")]
		public IActionResult EditRecipe(int Id, [FromBody] Recipe updatedRecipe)
		{
			// Verifique se a receita existe
			var existingRecipe = _recipeRepository.GetById(Id);
			if (existingRecipe == null)
			{
				return NotFound($"Recipe with ID {Id} not found");
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

		[HttpGet("PendingRecipes")]
		public IActionResult GetPendingRecipes()
		{
			// Busque todas as receitas pendentes de aprovação
			var pendingRecipes = _recipeRepository.GetPendingRecipes();

			// Verifique se existem receitas pendentes
			if (pendingRecipes == null || pendingRecipes.Count == 0)
			{
				return NotFound("No pending recipes found");
			}

			// Retorne a lista de receitas pendentes
			return Ok(pendingRecipes);
		}

  


	}
}
  


