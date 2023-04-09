using DapperNetCore.Data;
using DapperNetCore.DTOS;
using DapperNetCore.Models;

namespace DapperNetCore.Repositories
{
	public interface ICompanyRepository
	{
		public Task<IEnumerable<Company>> GetCompanies();
		public Task<Company> GetCompany(int id);
		public Task<Company> CreateCompany(CompanyForCreationDto company);

	}
}
