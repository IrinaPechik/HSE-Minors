
using SQLite;
using System;

namespace MinorsApplication.Models
{
    [Table("students")]
    public record StudentInfo
    {
        public StudentInfo() { }
        public StudentInfo(string email, string password, string fullName, string faculty,
            string educationalProgram, int courseNumber, int percentile)
        {
            Email = email;
            Password = password;
            FullName = fullName;
            Faculty = faculty;
            EducationalProgram = educationalProgram;
            CourseNumber = courseNumber;
            Percentile = percentile;
            CanSelectMinor = true;
            TeachearIdApproved = -1;
            MotivationLetter = string.Empty;
            StatusOfApplication = Status.NotExist;
            MinorName = string.Empty;
            CanTeacherSelect = true;
        }
        [PrimaryKey, AutoIncrement]
        public int StudentId { get; set; }
        [MaxLength(250), Unique]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Faculty { get; set; }
        public string EducationalProgram { get; set; }
        public int CourseNumber { get; set; }
        public int Percentile { get; set; }
        public bool CanSelectMinor { get; set; }
        public bool CanTeacherSelect { get; set; }
        public int TeachearIdApproved { get; set; }
        public string MotivationLetter { get; set; }
        public string MinorName { get; set; }
        public Status StatusOfApplication { get; set; }
        public enum Status
        {
            NotExist = -1,
            Отправлена,
            Одобрена,
            Отклонена
        }
    }
}
