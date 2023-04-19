using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Services.ValidationServices.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {
        private IUserValidateErrors _usersValidateErrors;

        public UsersController(IUserValidateErrors usersValidateErrors)
        {
            _usersValidateErrors = usersValidateErrors;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <param name="userDto">User information to create</param>
        /// <returns>An instance of <see cref="Result"/></returns>
        /// <response code="201">Created successfully.</response>
        /// <response code="400">Bad request, invalid input information is supplied or there is inconsistency in the requested data</response
        [HttpPost]
        [Route("/create-user")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Result))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = null)]
        public async Task<ActionResult> CreateUser(User userDto)
        {
            var errors = await _usersValidateErrors.ValidateErrors(userDto);

            if (errors.Any())
                return new BadRequestObjectResult(new Result
                {
                    IsSuccess = false,
                    Errors = errors
                });
            ;

            var newUser = new User
            {
                Name = userDto.Name,
                Email = Utils.Utils.NormalizeEmail(userDto.Email),
                Address = userDto.Address,
                Phone = userDto.Phone,
                UserType = userDto.UserType,
                Money = userDto.Money
            };

            switch (newUser.UserType)
            {
                case "Normal":
                    if (userDto.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.12);
                        //If new user is normal and has more than USD100
                        newUser.Money = newUser.Money + (userDto.Money * percentage);
                    }
                    else if (userDto.Money < 100 && userDto.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        newUser.Money = newUser.Money + (userDto.Money * percentage);
                    }
                    break;
                case "SuperUser":
                    if (userDto.Money > 100)
                    {
                        var percentage = Convert.ToDecimal(0.20);
                        newUser.Money = newUser.Money + (userDto.Money * percentage);
                    }
                    break;
                case "Premium":
                    if (userDto.Money > 100)
                        newUser.Money = newUser.Money + (userDto.Money * 2);                    
                    break;
                default:
                    break;
            }            

            return new CreatedResult("CreateUser", new Result
            {
                IsSuccess = true,
                Errors = null
            });
        }
    }
    
}
