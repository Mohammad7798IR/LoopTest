using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;
using System.Net;
using Test.Host.DTOs;
using Test.Host.Entities;
using Test.Host.Services.Interfaces;
using TestProject.Host.Context;

namespace Test.Host.Controllers
{

    public partial class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private static Logger _logger = LogManager.GetLogger("LoopTestLogRules");

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public partial class UserController
    {
        #region Get 

        [HttpGet(Name = "GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            _logger.Info("Finding Users");
            _logger.Info("Ip Address :" + Request.HttpContext.Connection.RemoteIpAddress);
            return Ok(await _userService.GetUsers());
        }


        [HttpGet]
        [Route("GetTheUser/{id?}")]
        public async Task<IActionResult> GetTheUser(string id)
        {
            if (ModelState.IsValid)
            {
                _logger.Info("Ip Address :" + Request.HttpContext.Connection.RemoteIpAddress);
                _logger.Info("Finding User");

                var user = await _userService.GetUser(id);

                if (user == null)
                {
                    _logger.Info("User Not Found");
                    return BadRequest();
                }
                _logger.Info("User Found");
                return Ok(user);
            }

            return BadRequest();
        }

        #endregion

        #region Post

        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] AddUserDTO addUserDTO)
        {

            if (ModelState.IsValid)
            {

                _logger.Info("Ip Address :" + Request.HttpContext.Connection.RemoteIpAddress);

                var result = await _userService.AddUser(addUserDTO);

                _logger.Info("Proccessing User Registration");

                switch (result)
                {
                    case AddUserResult.Success:
                        _logger.Info("User Has Been Created");
                        return Ok(new JsonResult(new { result = HttpStatusCode.OK, message = "با موفقیت ساخته شد" }));

                    case AddUserResult.NationalCodeExists:
                        _logger.Error("National Code Exists ");
                        return Ok(new JsonResult(new { result = HttpStatusCode.Conflict, message = "کد ملی قبلا ثبت شده است" }));


                    case AddUserResult.PhoneNumberExists:
                        _logger.Error("PhoneNumber Code Exists ");
                        return Ok(new JsonResult(new { result = HttpStatusCode.Conflict, message = "شماره تلفن قبلا ثبت شده است" }));

                    case AddUserResult.Failure:
                        _logger.Error("Unkown Error");
                        return Ok(new JsonResult(new { result = HttpStatusCode.InternalServerError, message = "مشکلی پیش امد" }));

                    default:
                        return Ok(new JsonResult(new { result = HttpStatusCode.OK, message = "با موفقیت ساخته شد" }));
                }

            }

            return Ok(new JsonResult(new { result = HttpStatusCode.BadRequest, message = "مشکلی پیش امد" }));
        }

        #endregion

        #region Put

        [HttpPut]
        [Route("UpdateUser/{id?}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserDTO updateUserDTO)
        {

            if (ModelState.IsValid)
            {
                _logger.Info("Ip Address :" + Request.HttpContext.Connection.RemoteIpAddress);

                var result = await _userService.UpdateUser(id, updateUserDTO);

                _logger.Info("Proccessing User Update");

                switch (result)
                {
                    case UpdateUserResult.Success:
                        _logger.Info("User Has Been Updated");
                        return Ok(new JsonResult(new { result = HttpStatusCode.OK, message = "با موفقیت بروز رسانی شد" }));

                    case UpdateUserResult.UserNotFound:
                        _logger.Error("National Code Not Found ");
                        return NotFound();

                    case UpdateUserResult.Failure:
                        _logger.Error("Unkown Error");
                        return Ok(new JsonResult(new { result = HttpStatusCode.InternalServerError, message = "مشکلی پیش امد" }));

                    default:
                        return Ok(new JsonResult(new { result = HttpStatusCode.OK, message = "با موفقیت ساخته شد" }));
                }
            }

            return Ok(new JsonResult(new { result = HttpStatusCode.BadRequest, message = "مشکلی پیش امد" }));
        }



        #endregion

        #region Delete

        [HttpDelete]
        [Route("DeleteUser/{id?}")]
        public async Task<IActionResult> DeleteUser(string id)
        {

            if (ModelState.IsValid)
            {
                _logger.Info("Ip Address :" + Request.HttpContext.Connection.RemoteIpAddress);
                var result = await _userService.DeleteUser(id);
                _logger.Info("Proccessing User Deletion");
                switch (result)
                {
                    case UpdateUserResult.Success:
                        _logger.Info("User Has Been Deleted");
                        return Ok(new JsonResult(new { result = HttpStatusCode.OK, message = "با موفقیت انجام شد" }));

                    case UpdateUserResult.UserNotFound:
                        _logger.Error("National Code Not Found ");
                        return NotFound();

                    case UpdateUserResult.Failure:
                        _logger.Error("Unkown Error");
                        return Ok(new JsonResult(new { result = HttpStatusCode.InternalServerError, message = "مشکلی پیش امد" }));

                    default:
                        break;
                }
            }

            return Ok(new JsonResult(new { result = HttpStatusCode.BadRequest, message = "مشکلی پیش امد" }));
        }

    }

    #endregion
}

