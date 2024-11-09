using AutoBiographyAPI.Data;
using AutoBiographyAPI.DTO.REQ;
using AutoBiographyAPI.DTO.RES;
using AutoBiographyAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoBiographyAPI.Service
{
    public class UserSavedPicsService
    {
        private readonly ApplicationDbContext _db;

        public UserSavedPicsService(ApplicationDbContext db)
        {
            _db = db;
        }
        public UserSavedPicsListResDto GetUserSavedPics(
            string username,
            int page,
            int pageSize
        )
        {
            return
                new UserSavedPicsListResDto
                {
                    totalPages = (int)Math.Ceiling(_db.UserSavedPics
                                        .Where(x => x.Username == username)
                                        .Count() / (double)pageSize),
                    page = page,
                    perPage = pageSize,
                    UserSavedPicsList = [.. _db.UserSavedPics
                                        .Where(x => x.Username == username)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)]
                };
        }

        public UserSavedPicsListResDto? GetSingleUserSavedPics(
            string username,
            int id,
            int page,
            int pageSize
            )
        {
            if (_db.UserSavedPics.FirstOrDefault(x => x.Id == id) == null)
            {
                return null;
            }
            else
            {
                return
                    new UserSavedPicsListResDto
                    {
                        totalPages = (int)Math.Ceiling(_db.UserSavedPics
                                    .Where(x => x.Username == username && x.Id == id)
                                    .Count() / (double)pageSize),
                        page = page,
                        perPage = pageSize,
                        UserSavedPicsList = [.. _db.UserSavedPics
                                        .Where(x => x.Username == username && x.Id == id)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)]
                    };
            }
        }

        public GeneralResDto CreateUserSavedPics(UserSavedPicsReqDto userSavedPicsReqDto)
        {
            if(_db.Users.FirstOrDefault(x => x.Username == userSavedPicsReqDto.Username) == null)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new ErrorResDto
                    {
                        Timestamp = new DateTime(),
                        Code = "username_not_found",
                        Message = "Username not found",
                        Details = "Can not find username: " + userSavedPicsReqDto.Username
                    },
                    Message = "Can not find username: " + userSavedPicsReqDto.Username
                };
            }

            if(_db.UserSavedPics.FirstOrDefault(x => x.Username == userSavedPicsReqDto.Username && x.ThumbUri == userSavedPicsReqDto.ThumbUri && x.FullUri == userSavedPicsReqDto.FullUri) != null)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = false,
                    Message = "Image already saved."
                };
            }

            try
            {
                UserSavedPics userSavedPics = new UserSavedPics
                {
                    Username = userSavedPicsReqDto.Username,
                    ThumbUri = userSavedPicsReqDto.ThumbUri,
                    FullUri = userSavedPicsReqDto.FullUri,
                    Slug = userSavedPicsReqDto.Slug
                };
                _db.UserSavedPics.Add(userSavedPics);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = true,
                    ErrorResDto = new ErrorResDto
                    {
                        Timestamp = new DateTime(),
                        Code = "internal_server_error",
                        Message = "Internal Server Error Occured while saving image.",
                        Details = ex.Message
                    },
                    Message = ex.Message
                };
            }
            return new GeneralResDto
            {
                IsSuccess = true,
                HasException = false,
                Message = "Successfully saved the image."
            };
        }

        public GeneralResDto UpdateUserSavedPics(int id, UserSavedPicsReqDto userSavedPicsReqDto)
        {
            if (_db.Users.FirstOrDefault(x => x.Username == userSavedPicsReqDto.Username) == null)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = false,
                    ErrorResDto = new ErrorResDto
                    {
                        Timestamp = new DateTime(),
                        Code = "username_not_found",
                        Message = "Username not found",
                        Details = "Can not find username: " + userSavedPicsReqDto.Username
                    },
                    Message = "Can not find username: " + userSavedPicsReqDto.Username
                };
            }

            if (_db.UserSavedPics.AsNoTracking().FirstOrDefault(x => x.Username == userSavedPicsReqDto.Username && x.Id == id) == null)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = true,
                    ErrorResDto = new ErrorResDto
                    {
                        Timestamp = new DateTime(),
                        Code = "record_not_found",
                        Message = "Record not found with given id and username.",
                        Details = "Record not found with given id and username."
                    },
                    Message = "Record not found with given id and username in the database."
                };
            }

            try
            {
                UserSavedPics userSavedPics = new UserSavedPics
                {
                    Id = id,
                    Username = userSavedPicsReqDto.Username,
                    ThumbUri = userSavedPicsReqDto.ThumbUri,
                    FullUri = userSavedPicsReqDto.FullUri,
                    Slug = userSavedPicsReqDto.Slug
                };
                _db.UserSavedPics.Update(userSavedPics);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = true,
                    ErrorResDto = new ErrorResDto
                    {
                        Timestamp = new DateTime(),
                        Code = "internal_server_error",
                        Message = "Internal Server Error Occured while updating image.",
                        Details = ex.Message
                    },
                    Message = ex.Message
                };
            }
            return new GeneralResDto
            {
                IsSuccess = true,
                HasException = false,
                Message = "Successfully updated the record."
            };
        }

        public GeneralResDto DeleteUserSavedPics(int id)
        {

            UserSavedPics? res = _db.UserSavedPics.Find(id);
            if (res == null)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = true,
                    ErrorResDto = new ErrorResDto
                    {
                        Timestamp = new DateTime(),
                        Code = "record_not_found",
                        Message = "Record not found with given id.",
                        Details = "Record not found with given id."
                    },
                    Message = "Record not found with given id in the database."
                };
            }

            try
            {
                _db.UserSavedPics.Remove(res);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return new GeneralResDto
                {
                    IsSuccess = false,
                    HasException = true,
                    ErrorResDto = new ErrorResDto
                    {
                        Timestamp = new DateTime(),
                        Code = "internal_server_error",
                        Message = "Internal Server Error Occured while deleting image.",
                        Details = ex.Message
                    },
                    Message = ex.Message
                };
            }
            return new GeneralResDto
            {
                IsSuccess = true,
                HasException = false,
                Message = "Successfully un-saved the image."
            };
        }
    }
}