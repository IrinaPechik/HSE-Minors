using SQLite;
using System;

namespace MinorsApplication.Models
{
    [Table("teachers")]
    public record TeacherInfo
    {
        public TeacherInfo() { }
        public TeacherInfo(string email, string password, string fullName, string minor,
            int numberOfSeats, int numberOfVacantSeats)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            Minor = minor;
            NumberOfSeats = numberOfSeats;
            NumberOfVacantSeats = numberOfVacantSeats;
        }
        [PrimaryKey, AutoIncrement]
        public int TeacherId { get; set; }
        [MaxLength(250), Unique]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Minor { get; set; }
        public int NumberOfSeats { get; set; }
        public int NumberOfVacantSeats { get; set; }
/*        [TextBlob("allApplications")]
        public List<StudentInfo> allApplications { get; set; }*/
        /*        public override string ToString()
                {
                    return $"{Minor}";
                }*/
    }
}
