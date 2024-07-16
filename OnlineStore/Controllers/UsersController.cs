using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineStore.Data.UOW;
using OnlineStore.Dtos;
using OnlineStore.Helpers;
using OnlineStore.Helpers.QueryParams;
using OnlineStore.Helpers.Validator;
using OnlineStore.Models;
using Serilog;

namespace OnlineStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UsersController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserForListDto>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<UserForListDto>>> GetUsers([FromQuery]UserParams userParams)
        {
            var users = await _unitOfWork.UserRepository.GetUsers(userParams);
            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
            
            // add pagination header
            Response.AddPagination(users.CurrentPage, users.PageSize,
                users.TotalCount, users.TotalPages);
            Log.Information("Sending Users Data {@usersToReturn}", usersToReturn);
            return Ok(usersToReturn);
        }

        // GET: api/Users/5
        [HttpGet]
        [Route("{id:int}")]
        //[ProducesResponseType(typeof(UserForDetailedDto), (int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<UserForDetailedDto>> GetUser(int id)
        {
            var user = await _unitOfWork.UserRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            var userToReturn = _mapper.Map<UserForDetailedDto>(user);

            return Ok(userToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]UserForCreationDto userForCreationDto)
        {

            AddUserValidator validator = new AddUserValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(userForCreationDto);

            if (!result.IsValid)
            {
                //return StatusCode(StatusCodes.Status400BadRequest, result.Errors);
                return Validate(result);
            }

           var user = await _unitOfWork.UserRepository.Add(_mapper.Map(userForCreationDto, new User()));

            if (user == null)
            {
                return NotFound();
            }

            if (await _unitOfWork.Complete())
                return CreatedAtRoute("GetUser", new { id = user.Id }, user);

            throw new Exception("Adding user failed on save");
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody]UserForUpdateDto userForUpdateDto)
        {
            UserValidator validator = new UserValidator();
            FluentValidation.Results.ValidationResult result = validator.Validate(userForUpdateDto);

            if (!result.IsValid)
            {
                return Validate(result);
            }

            var user = await _unitOfWork.UserRepository.GetAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            _mapper.Map(userForUpdateDto, user);

            if (await _unitOfWork.Complete())
                return Ok("User was successfully updated");

            throw new Exception($"Updating user {id} failed on save");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {

            await _unitOfWork.UserRepository.Delete(id);

            if (await _unitOfWork.Complete())
                return Ok("User was successfully deleted!");

            throw new Exception($"Deleting user {id} failed on delete");
        }

        [HttpGet("gender")]
        public async Task<IActionResult> GetNumOfUsersByGender()
        {
            var users = await _unitOfWork.UserRepository.GetNumOfUsersByGender();

            return Ok(users);
        }
        [HttpGet("no-order")]
        public async Task<IActionResult> UsersNeverOrdered()
        {
            var res = await _unitOfWork.UserRepository.UsersNeverOrdered();
            return Ok(res);
        }

        [HttpGet("most-order")]
        public async Task<IActionResult> UsersMostOrdered()
        {
            var res = await _unitOfWork.UserRepository.UsersMostOrdered();
            return Ok(res);
        }
    }
}