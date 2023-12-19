using AutoMapper;
using EcoMonitor.Model;
using EcoMonitor.Model.APIResponses;
using EcoMonitor.Model.DTO.Company;
using EcoMonitor.Model.DTO.EnvFactor;
using EcoMonitor.Model.Interfaces;
using EcoMonitor.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Net;

namespace EcoMonitor.Controllers
{
    public abstract class BasicCRUDController<T, U, UDTO, UCreateDTO, UUpdateDTO> : 
        Controller where T : IRepository<U> where U : class, IEntityWithId where UDTO : class where UCreateDTO : class where UUpdateDTO : class
    {
        protected APIResponse _response;
        protected readonly T _repository;
        protected readonly IMapper _mapper;

        public BasicCRUDController(T repository)
        {
            _response = new();
            _repository = repository;
            _mapper = new MapperConfiguration(options =>
            {
                options.CreateMap<U, UDTO>().ReverseMap();
                options.CreateMap<U, UCreateDTO>().ReverseMap();
                options.CreateMap<U, UUpdateDTO>().ReverseMap();
            }
            ).CreateMapper();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<APIResponse>> GetAll()
        {
            try
            {
                IEnumerable<U> entities = await _repository.GetAllAsync();
                _response.Result = _mapper.Map<IEnumerable<UDTO>>(entities);
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
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }


        [HttpGet("id:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<APIResponse>> Get(int id)
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
                U model = await _repository.GetAsync(c => c.id == id);
                _response.Result = _mapper.Map<UDTO>(model);
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
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<APIResponse>> Create([FromBody] UCreateDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var modelError in ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        _response.ErrorMessages.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }
            try
            {
                U model = _mapper.Map<U>(createDTO);

                await _repository.CreateAsync(model);
                _response.Result = _mapper.Map<UDTO>(model);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtAction("Get", new { id = model.id }, _response);
            }
            catch (DbUpdateException ex)
            {
                MySqlException innerException = ex.InnerException as MySqlException;
                if (innerException != null && (innerException.Number == 1062))
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{typeof(U)} with this name already exists");
                    return Conflict(_response);
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }


        [HttpDelete("id:int")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<APIResponse>> Delete(int id)
        {
            if (id < 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add($"{typeof(U)} id cannot be less than 0!");
                return BadRequest(_response);
            }
            try
            {
                var model = await _repository.GetAsync(u => u.id == id);
                if (model == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _repository.RemoveAsync(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public virtual async Task<ActionResult<APIResponse>> Update([FromBody] UUpdateDTO updateDTO)
        {
            if (!ModelState.IsValid)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                foreach (var modelError in ModelState.Values)
                {
                    foreach (ModelError error in modelError.Errors)
                    {
                        _response.ErrorMessages.Add(error.ErrorMessage);
                    }
                }
                return BadRequest(_response);
            }
            try
            {
                U model = _mapper.Map<U>(updateDTO);

                if (await _repository.GetAsync(c => c.id == model.id, false) == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                await _repository.UpdateAsync(model);
                return NoContent();

            }
            catch (DbUpdateException ex)
            {
                MySqlException innerException = ex.InnerException as MySqlException;
                if (innerException != null && (innerException.Number == 1062))
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.IsSuccess = false;
                    _response.ErrorMessages.Add($"{typeof(U)} with this name already exists");
                    return Conflict(_response);
                }
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return StatusCode(500, _response);
        }
    }
}
