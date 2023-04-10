using DapperNetCore.DTOS;
using DapperNetCore.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DapperNetCore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CompaniesController : ControllerBase
	{
		private readonly ICompanyRepository _companyRepository;

		public CompaniesController(ICompanyRepository companyRepository)
		{
			_companyRepository = companyRepository;
		}

		[HttpGet("GetCompanies")]
		public async Task<IActionResult> GetCompanies()
		{
			try
			{
				var companies = await _companyRepository.GetCompanies();
				return Ok(companies);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCompany(int id)
		{
			try
			{
				var company = await _companyRepository.GetCompany(id);
				if (company == null)
					return NotFound();
				return Ok(company);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateCompany(CompanyForCreationDto company)
		{
			try
			{
				await _companyRepository.CreateCompany(company);
				return RedirectToAction("GetCompanies");
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCompany(int id)
		{
			try
			{
				var company = await _companyRepository.GetCompany(id);
				if (company == null)
					return NotFound();

				await _companyRepository.DeleteCompany(id);
				return NoContent();
			}
			catch (Exception ex)
			{

				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCompany(int id, CompanyUpdateDto companyUpdateDto)
		{
			try
			{
				var company = await _companyRepository.GetCompany(id);
				if (company == null)
					return NotFound();
				await _companyRepository.UpdateCompany(id, companyUpdateDto);
				return NoContent();
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}

		[HttpGet("EmployeeId/{id}")]
		public async Task<IActionResult> GetCompanyForEmployee(int id)
		{
			try
			{
				var company = await _companyRepository.GetCompanyByEmployeeId(id);
				if (company == null)
					return NotFound();

				return Ok(company);
			}
			catch (Exception ex)
			{
				//log error
				return StatusCode(500, ex.Message);
			}
		}
	}
}
