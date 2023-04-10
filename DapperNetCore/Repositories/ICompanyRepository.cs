using DapperNetCore.Data;
using DapperNetCore.DTOS;
using DapperNetCore.Models;

namespace DapperNetCore.Repositories
{
	public interface ICompanyRepository
	{
		Task<IEnumerable<Company>> GetCompanies();
		Task<Company> GetCompany(int id);
		Task CreateCompany(CompanyForCreationDto company);
		Task UpdateCompany(int id, CompanyUpdateDto companyUpdateDto);
		Task DeleteCompany(int id);

		Task<Company> GetCompanyByEmployeeId(int id);
	}
}
