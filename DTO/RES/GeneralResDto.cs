namespace AutoBiographyAPI.DTO.RES
{
    public class GeneralResDto
    {
        public bool IsSuccess {  get; set; }
	    public bool HasException { get; set; }
	    public ErrorResDto? ErrorResDto { get; set; } 
	    public string? Message {  get; set; }
    }
}
