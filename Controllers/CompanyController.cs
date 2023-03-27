using Microsoft.AspNetCore.Mvc;
using MyAPI.Contracts.Query;
using MyAPI.Contracts.Requests;
using MyAPI.Services.Interfaces;

namespace MyAPI.Controllers
{
    [Route("[Controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger _logger;

        private ICompanyService _companyService;
        //todo lägg till nlogg: fil logging vad som går rätt 

        public CompanyController(ICompanyService companyService, ILogger<CompanyController> logger)
        {
            _companyService = companyService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var company = await _companyService.Get(id);
            if (company == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to get company");
            }
            return Ok(company);

        }


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CompanyQuery query)
        {
            var company = await _companyService.Get(query);
            if (company == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to get company");
            }
            return Ok(company);

        }

        [HttpPost]
        public async Task<IActionResult> Create(CompanyRequest companyRequest)
        {
            var company = await _companyService.Create(companyRequest);
            if (company == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to create company");
            }
            return Ok(company);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute]Guid id, CompanyRequest companyRequest)
        {
            var result = await _companyService.Update(id, companyRequest);
            if (result == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to update company");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var company = await _companyService.Delete(id);
            if (company == null)
            {
                _logger.LogInformation("Failed");
                return NotFound("Failed to delete company");
            }
            return Ok(company);
        }


    }
}
