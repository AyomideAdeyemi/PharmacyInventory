namespace ContactBook_Domain.Dtos.Requests
{
    public class UserRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
       // public DateOnly Birthday { get; set; }
        public string PhoneNumber { get; set; }
        //public string Role { get; set; }
        //public ICollecrtion<string> Role { get; set; }

    }
}
