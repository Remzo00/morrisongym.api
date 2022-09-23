namespace Morrison_Gym.API.Dto
{
    public class ResponseDto
    {
        public object? Result { get; set; } = true;
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public List<string>? ErrorMessages { get; set; }
    }
}
