using AutoBiographyAPI.Models;

namespace AutoBiographyAPI.DTO.RES
{
    public class UserSavedPicsListResDto
    {

        public int page {  get; set; }  

        public int totalPages { get; set; }

        public int perPage { get; set; }

        public required List<UserSavedPics> UserSavedPicsList { get; set; }
    }
}
