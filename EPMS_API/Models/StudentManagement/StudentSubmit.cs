namespace EPMS_API.Models.StudentManagement
{
    public class StudentSubmit
    {
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string passwordRepeat { get; set; }
        public StudentSubmit(string name, string phoneNumber, string email, string username, string password, string passwordRepeat)
        {
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.username = username;
            this.password = password;
            this.passwordRepeat = passwordRepeat;
        }
    }
}
