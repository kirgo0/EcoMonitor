using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.DTO;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EcoMonitor.Controllers
{
    [ApiController]
    [Route("api/CompanyData")]
    public class CompanyDataController : Controller
    {
        protected APIResponse _response;
        private readonly ICompanyRepository _dbCompany;
        private readonly IMapper _mapper;

        public CompanyDataController(ICompanyRepository dbCompany, IMapper mapper)
        {
            _response = new();
            _dbCompany = dbCompany;
            _mapper = mapper;
        }


        [HttpGet(Name = "GetAllCompanies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetAllCompanies()
        {
            try
            {
                IEnumerable<Company> companies = await _dbCompany.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<CompanyDTO>>(companies);
                if (_response.Result != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }

        [HttpGet("id:int", Name = "GetCompany")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)] // ------------
        public async Task<ActionResult<APIResponse>> GetCompany(int id)
        {
            if (id < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("id cannot be less than 0!");
                return BadRequest(_response);
            }
            try
            {
                Company company = await _dbCompany.GetAsync(c => c.id == id);
                _response.Result = _mapper.Map<CompanyDTO>(company);
                if (_response.Result != null)
                {
                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }
                else
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
            }
            catch (Exception e)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { e.ToString() };
            }
            return _response;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)] //-------------
        public async Task<ActionResult<APIResponse>> CreateCompany([FromBody] CompanyCreateDTO createDTO)
        {
            if (createDTO == null || !ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ModelState.ToString());
                return BadRequest(_response);
            }
            try
            {
                //if (await _dbCompany.GetAsync(c => c.name.ToLower() == createDTO.name.ToLower()) != null)
                //{
                //    _response.StatusCode = HttpStatusCode.Conflict;
                //    _response.IsSuccess = false;
                //    _response.ErrorMessages.Add("Company already Exists!");
                //    return Conflict(_response);
                //}

                Company company = _mapper.Map<Company>(createDTO);

                await _dbCompany.CreateAsync(company);
                _response.Result = _mapper.Map<CompanyDTO>(company);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCompany", new { id = company.id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }


        [HttpDelete("id:int", Name = "DeleteCompany")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> DeleteCompany(int id)
        {
            if (id < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Factor id cannot be less than 0!");
                return BadRequest(_response);
            }
            try
            {
                var company = await _dbCompany.GetAsync(u => u.id == id);
                if (company == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _dbCompany.RemoveAsync(company);
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut(Name = "UpdateCompany")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<APIResponse>> UpdateCompany([FromBody] CompanyUpdateDTO updateDTO)
        {
            if (updateDTO == null || !ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add(ModelState.ToString());
                return BadRequest(_response);
            }
            try
            {
                Company model = _mapper.Map<Company>(updateDTO);

                if (await _dbCompany.GetAsync(c => c.id == model.id, false) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _dbCompany.UpdateAsync(model);
                return NoContent();

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
