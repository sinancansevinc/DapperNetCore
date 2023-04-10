using Dapper;
using DapperNetCore.Data;
using DapperNetCore.DTOS;
using DapperNetCore.Models;
using System.Data;

namespace DapperNetCore.Repositories
{
	public class CompanyRepository : ICompanyRepository
	{
		private readonly DapperContext _context;

		public CompanyRepository(DapperContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Company>> GetCompanies()
		{
			var query = "SELECT * FROM Companies";


			using (var connection = _context.CreateConnection())
			{
				var companies = await connection.QueryAsync<Company>(query);
				return companies.ToList();
			}
		}

		public async Task<Company> GetCompany(int id)
		{
			var query = "SELECT * FROM Companies WHERE Companies.Id = @Id";
			using (var connection = _context.CreateConnection())
			{
				var company = await connection.QuerySingleOrDefaultAsync<Company>(query, new { Id = id });
				return company;
			}
		}

		public async Task CreateCompany(CompanyForCreationDto company)
		{
			var query = "INSERT INTO Companies (Name, Address, Country) VALUES (@Name, @Address, @Country)";
			var parameters = new DynamicParameters();
			parameters.Add("Name", company.Name, DbType.String);
			parameters.Add("Address", company.Address, DbType.String);
			parameters.Add("Country", company.Country, DbType.String);
			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		public async Task UpdateCompany(int id, CompanyUpdateDto companyUpdateDto)
		{
			var query = "UPDATE Companies SET Name = @Name, Address = @Address, Country = @Country WHERE Id = @Id";
			var parameters = new DynamicParameters();

			parameters.Add("Id", id, DbType.Int32);
			parameters.Add("Name", companyUpdateDto.Name, DbType.String);
			parameters.Add("Address", companyUpdateDto.Address, DbType.String);
			parameters.Add("Country", companyUpdateDto.Country, DbType.String);

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, parameters);
			}
		}

		

		public async Task DeleteCompany(int id)
		{
			var query = "DELETE FROM Companies where Id = @Id";

			using (var connection = _context.CreateConnection())
			{
				await connection.ExecuteAsync(query, new { Id = id });
			}
		}

		public async Task<Company> GetCompanyByEmployeeId(int id)
		{
			var procedureName = "ShowCompanyForProvidedEmployeeId";
			var parameters = new DynamicParameters();
			parameters.Add("Id", id, DbType.Int32,ParameterDirection.Input);

			using (var connection = _context.CreateConnection())
			{
				var company = await connection.QueryFirstOrDefaultAsync<Company>
					(procedureName, parameters, commandType: CommandType.StoredProcedure);

				return company;
			}

		}
	}
}
