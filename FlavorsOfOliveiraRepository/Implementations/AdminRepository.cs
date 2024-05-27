using FlavorsOfOliveira.Data.Context;
using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Implementations
{
	public class AdminRepository : IAdminRepository
	{

		private readonly DbSet<Admin> _dbSet;
		private readonly FlavorsOfOliveiraDBContext _flavorsOfOliveiraDBContext;

		public AdminRepository(FlavorsOfOliveiraDBContext flavorsOfOliveiraDBContext)
		{
			 _dbSet = flavorsOfOliveiraDBContext.Set<Admin>();

		}
		public Admin Add(Admin admin)
		{
			_dbSet.Add(admin);
   _flavorsOfOliveiraDBContext.SaveChanges();
			return admin;
		}

		public List<Admin> GetAll()
		{
			return _dbSet.ToList();
		}

		public Admin GetById(int id)
		{
			return _dbSet.FirstOrDefault(p => p.Id == id);
		}

		public void Remove(Admin admin)
		{
			_dbSet.Remove(admin);
			_flavorsOfOliveiraDBContext.SaveChanges();
		}

		public Admin Update(Admin admin)
		{
			_dbSet.Update(admin);
			_flavorsOfOliveiraDBContext.SaveChanges();
			return admin;
		}
	}
}
