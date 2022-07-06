namespace Morrison_Gym.API.Models.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; } = true;
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public List<string>? ErrorMessages { get; set; }
    }
}
