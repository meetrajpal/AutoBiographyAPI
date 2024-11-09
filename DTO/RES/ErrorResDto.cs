namespace AutoBiographyAPI.DTO.RES
{
    public class ErrorResDto
    {
        public DateTime Timestamp {  get; set; }
	    public string Code { get; set; }
	    public string Message {  get; set; }
	    public string Details {  get; set; }
    }
}
