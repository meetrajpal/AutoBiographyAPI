namespace AutoBiographyAPI.DTO.REQ
{
    public class UserSavedPicsReqDto
    {
        public required string Username { get; set; }

        public required string ThumbUri { get; set; }

        public required string FullUri { get; set; }

        public string? Slug { get; set; }
    }
}
