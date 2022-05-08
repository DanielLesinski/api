namespace Application.Dto.Account
{
    public class ListUserDto
    {
        public int Count { get; set; }
        public IList<GetUserDto> Users { get; set; }
    }
}
