using FlavorsOfOliveira.Domain.Entities;
using FlavorsOfOliveira.Repository.Interfaces;
using FlavorsOfOliveira.Services.Interfaces;

namespace FlavorsOfOliveira.Services.Implementations
{
	public class AdminService : IAdminService
 { 
  private readonly IAdminRepository _adminRepository;
   public AdminService(IAdminRepository adminRepository)
   {
     _adminRepository = adminRepository;
  
   }

   public List<Admin> GetAll()
		 {
			return _adminRepository.GetAll();
		 }

		 public Admin GetById(int id)
		 {
			  return _adminRepository.GetById(id);
		 }

		 public void Remove(int id)
		 {
					 Admin admin = _adminRepository.GetById(id);
			   _adminRepository.Remove(admin);
		 }

		 public Admin Save(Admin admin)
		 {
		   if (admin.Id == 0)
     {
				return _adminRepository.Add(admin);
  
		  	}
     else
     { 
        return _adminRepository.Update(admin);
    
     }
   
		 }
  
  
	}
}
