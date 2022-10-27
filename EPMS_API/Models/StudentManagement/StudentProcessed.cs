namespace EPMS_API.Models.StudentManagement
{
    public class StudentProcessed
    {
        public string id { get; set; }
        public string name { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public StudentProcessed(string id, string name, string phoneNumber, string email)
        {
            this.id = id;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.email = email;
        }
    }
}
