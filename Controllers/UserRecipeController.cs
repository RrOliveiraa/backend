﻿using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlavorsOfOliveira.Controllers
{
	[Route("FlavorsOfOliveiraapi/[controller]")]
	[ApiController]
	public class UserRecipeController : ControllerBase
	{

		private readonly IUserRecipeRepository _userRecipeRepository;

		public UserRecipeController(IUserRecipeRepository userRecipeRepository)
		{
			_userRecipeRepository = userRecipeRepository;
		}


		[HttpGet]
		public List<UserRecipe> GetAll()
		{
			return GetAll();
		}

		[HttpGet("{Id}")]
		public UserRecipe GetById(int Id)
		{
			return GetById(Id);
		}

		[HttpPost]
		public UserRecipe Save(UserRecipe userrecipe)
		{
			return Save(userrecipe);
		}

		[HttpDelete("{Id}")]
		public void Remove(int Id)
		{
			Remove(Id);
		}
	}
}
