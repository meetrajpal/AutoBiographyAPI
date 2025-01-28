using AutoBiographyAPI.DTO.RES;
using AutoBiographyAPI.DTO.REQ;
using AutoBiographyAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Cors;

namespace AutoBiographyAPI.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api/user-saved-pics")]
    //[Route("api/[controller]")]
    public class AutoBiographyAPIController : ControllerBase
    {
        private readonly UserSavedPicsService _userSavedPicsService;

        public AutoBiographyAPIController(UserSavedPicsService userSavedPicsService)
        {
            _userSavedPicsService = userSavedPicsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GeneralResDto))]
        //[ProducesResponseType(500, Type = typeof(IGeneralResDto))]
        public ActionResult<UserSavedPicsListResDto> GetUserSavedPics(
            [FromQuery(Name = "username"), Required] string username,
            [FromQuery(Name = "id")] int id,
            [FromQuery(Name = "page")] int page = 1,
            [FromQuery(Name = "pageSize")] int pageSize = 10
            )
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new
                    {
                        Timestamp = new DateTime(),
                        Code = "bad_request",
                        Message = "Please enter username",
                        Details = "Username can not be null"
                    },
                }
                );
            }

            if (id != 0)
            {
                var res = _userSavedPicsService.GetSingleUserSavedPics(username, id, page, pageSize);
                if (res == null)
                {
                    return NotFound(
                        new
                        {
                            IsSuccess = false,
                            HasException = false,
                            ErrorResDto = new
                            {
                                Timestamp = DateTime.Now,
                                Code = "record_not_found",
                                Message = "Record with given id and username not found",
                                Details = "Record with given id and username not found"
                            },
                        }
                    );
                }
                else
                {
                    return Ok(res);
                }
            }

            return Ok(_userSavedPicsService.GetUserSavedPics(username, page, pageSize));
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GeneralResDto))]
        public ActionResult<GeneralResDto> PostUserSavedPics([FromBody] UserSavedPicsReqDto userSavedPicsReqDto)
        {
            if (string.IsNullOrEmpty(userSavedPicsReqDto.Username) || string.IsNullOrWhiteSpace(userSavedPicsReqDto.Username))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new
                    {
                        Timestamp = new DateTime(),
                        Code = "bad_request",
                        Message = "Please enter username",
                        Details = "Username can not be null"
                    },
                }
                );
            }

            if (string.IsNullOrEmpty(userSavedPicsReqDto.ThumbUri) || string.IsNullOrWhiteSpace(userSavedPicsReqDto.ThumbUri))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new
                    {
                        Timestamp = new DateTime(),
                        Code = "bad_request",
                        Message = "Please enter thumb url",
                        Details = "Thumbnail URL of image can not be null"
                    },
                }
                );
            }

            if (string.IsNullOrEmpty(userSavedPicsReqDto.FullUri) || string.IsNullOrWhiteSpace(userSavedPicsReqDto.FullUri))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new
                    {
                        Timestamp = new DateTime(),
                        Code = "bad_request",
                        Message = "Please enter full url",
                        Details = "Full URL of image can not be null"
                    },
                }
                );
            }

            var res = _userSavedPicsService.CreateUserSavedPics(userSavedPicsReqDto);
            if (res.HasException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            else if (res.ErrorResDto?.Code == "username_not_found")
            {
                return NotFound(res);
            }

            return Ok(res);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GeneralResDto))]
        public ActionResult<GeneralResDto> PutUserSavedImage([FromQuery, Required] int id, [FromBody] UserSavedPicsReqDto userSavedPicsReqDto) {
            if (string.IsNullOrEmpty(userSavedPicsReqDto.Username) || string.IsNullOrWhiteSpace(userSavedPicsReqDto.Username))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new
                    {
                        Timestamp = new DateTime(),
                        Code = "bad_request",
                        Message = "Please enter username",
                        Details = "Username can not be null"
                    },
                }
                );
            }

            if (string.IsNullOrEmpty(userSavedPicsReqDto.ThumbUri) || string.IsNullOrWhiteSpace(userSavedPicsReqDto.ThumbUri))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new
                    {
                        Timestamp = new DateTime(),
                        Code = "bad_request",
                        Message = "Please enter thumb url",
                        Details = "Thumbnail URL of image can not be null"
                    },
                }
                );
            }

            if (string.IsNullOrEmpty(userSavedPicsReqDto.FullUri) || string.IsNullOrWhiteSpace(userSavedPicsReqDto.FullUri))
            {
                return BadRequest(new
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new
                    {
                        Timestamp = new DateTime(),
                        Code = "bad_request",
                        Message = "Please enter full url",
                        Details = "Full URL of image can not be null"
                    },
                }
                );
            }

            var res = _userSavedPicsService.UpdateUserSavedPics(id, userSavedPicsReqDto);
            if (res.HasException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            else if (res.ErrorResDto?.Code == "username_not_found")
            {
                return NotFound(res);
            }
            else if (res.ErrorResDto?.Code == "record_not_found")
            {
                return NotFound(res);
            }

            return Ok(res);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GeneralResDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GeneralResDto))]
        public ActionResult<GeneralResDto> DeleteUserSavedImage([FromQuery, Required] int id)
        {
            var res = _userSavedPicsService.DeleteUserSavedPics(id);
            if (res.HasException)
            {
                StatusCode(StatusCodes.Status500InternalServerError, res);
            }
            else if (res.ErrorResDto?.Code == "record_not_found")
            {
                return NotFound(res);
            }

            return Ok(res);
        }
    }
}
