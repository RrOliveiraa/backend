using FlavorsOfOliveira.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorsOfOliveira.Repository.Interfaces
{
	public interface IAdminRepository
	{
		List<Admin> GetAll();
  Admin GetById(int id);
  Admin Add(Admin admin);
  Admin Update(Admin admin);
  void Remove(Admin admin);
	}
}
