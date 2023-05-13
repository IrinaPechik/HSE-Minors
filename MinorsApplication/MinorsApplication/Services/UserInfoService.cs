using MinorsApplication.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace MinorsApplication.Services
{
    public class UserInfoService
    {
        string _dbPath;
        public string StatusMessage { get; set; }
        private SQLiteAsyncConnection conn;
        private List<StudentInfo> allStudents;

        private async Task InitStudentTable()
        {
            if (conn != null)
                return;
            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<StudentInfo>();
        }

        public UserInfoService(string dbPath)
        {
            _dbPath = dbPath;
        }

        public async Task AddNewStudent(StudentInfo student)
        {
            try
            {
                int result;
                await InitStudentTable();
                result = await conn.InsertAsync(student);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", student.FullName, ex.Message);
            }

        }

        public async Task<List<StudentInfo>> GetAllStudents()
        {
            try
            {
                await InitStudentTable();
                allStudents = await conn.Table<StudentInfo>().ToListAsync();
                return allStudents;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<StudentInfo>();
        }

        public async Task<List<StudentInfo>> GetStudentsByTeacherId(int teacherId)
        {
            try
            {
                await GetAllStudents();
                var studentsForTeacher = allStudents.Where(x => x.TeachearIdApproved == teacherId && x.CanTeacherSelect == true).ToList();
                return studentsForTeacher;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<StudentInfo>();
        }

        public async Task<StudentInfo> GetStudentByEmail(string email)
        {
            try
            {
                await GetAllStudents();
                if (allStudents == null || allStudents.Count == 0)
                {
                    return null;
                }
                var student = allStudents.Where
                    (x =>
                    x.Email.Trim() == email.Trim())
                    .First();
                return student;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return null;
        }

        private List<TeacherInfo> allTeachers;

        private async Task InitTeachers()
        {
            if (conn != null)
                return;
            conn = new SQLiteAsyncConnection(_dbPath);
            await conn.CreateTableAsync<TeacherInfo>();
        }

        public async Task AddNewTeacher(TeacherInfo teacher)
        {
            try
            {
                int result;
                await InitTeachers();
                result = await conn.InsertAsync(teacher);
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to add {0}. Error: {1}", teacher.FullName, ex.Message);
            }

        }

        public async Task<List<TeacherInfo>> GetAllTeachers()
        {
            try
            {
                await InitTeachers();
                allTeachers = await conn.Table<TeacherInfo>().ToListAsync();
                return allTeachers;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return new List<TeacherInfo>();
        }

        public async Task<TeacherInfo> GetTeacherByEmail(string email)
        {
            try
            {
                await GetAllTeachers();
                if (allTeachers == null || allTeachers.Count == 0)
                {
                    return null;
                }
                var teacher = allTeachers.Where
                    (x =>
                    x.Email.Trim() == email.Trim())
                    .First();
                return teacher;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return null;
        }

        public async Task<TeacherInfo> GetTeacherById(int id)
        {
            try
            {
                await GetAllTeachers();
                if (allTeachers == null || allTeachers.Count == 0)
                {
                    return null;
                }
                var teacher = allTeachers.Where
                    (x =>
                    x.TeacherId == id)
                    .First();
                return teacher;
            }
            catch (Exception ex)
            {
                StatusMessage = string.Format("Failed to retrieve data. {0}", ex.Message);
            }
            return null;
        }

        public async Task<StudentInfo> AddApplication(string email, int teacherId, string motivationLetter)
        {
            var student = await GetStudentByEmail(email);
            student.TeachearIdApproved = teacherId;
            student.StatusOfApplication = 0;
            student.CanSelectMinor = false;
            student.MotivationLetter = motivationLetter;
            var teacher = await GetTeacherById(teacherId);
            student.MinorName = teacher.Minor;
            await conn.RunInTransactionAsync(conn =>
            {
                conn.Update(student);
            });
            return student;
        }

        public async Task<StudentInfo> RemoveApplication(string email)
        {
            var student = await GetStudentByEmail(email);
            student.CanSelectMinor = true;
            student.TeachearIdApproved = -1;
            student.StatusOfApplication = StudentInfo.Status.NotExist;
            student.MotivationLetter = string.Empty;
            student.MinorName = string.Empty;
            student.CanTeacherSelect = true;
            await conn.RunInTransactionAsync(conn =>
            {
                conn.Update(student);
            });
            return student;
        }
        public async Task ApproveApplications(List<StudentInfo> approvedStudents, TeacherInfo currentTeacher)
        {
            // Одобряем заявку.
            foreach (StudentInfo approvedStudent in approvedStudents)
            {
                var student = await GetStudentByEmail(approvedStudent.Email);
                student.StatusOfApplication = StudentInfo.Status.Одобрена;
                student.CanSelectMinor = false;
                // Говорим, что теперь студент числится у текущего преподавателя.
                student.TeachearIdApproved = currentTeacher.TeacherId;
                student.CanTeacherSelect = false;
                await conn.RunInTransactionAsync(conn =>
                {
                    conn.Update(student);
                });
            }
            // Обновляем количество вакантных мест.
            currentTeacher.NumberOfVacantSeats -= approvedStudents.Count;
            if (currentTeacher.NumberOfVacantSeats < 0)
            {
                currentTeacher.NumberOfVacantSeats = 0;
            }
            await conn.RunInTransactionAsync(conn =>
            {
                conn.Update(currentTeacher);
            });
        }

        public async Task RejectApplications(List<StudentInfo> rejectedStudents, TeacherInfo currentTeacher)
        {
            // Отклоняем заявку.
            foreach (StudentInfo rejectedStudent in rejectedStudents)
            {
                var student = await GetStudentByEmail(rejectedStudent.Email);
                student.StatusOfApplication = StudentInfo.Status.Отклонена;
                // Открываем возможность снова записаться на майнор.
                student.CanSelectMinor = true;
                student.CanTeacherSelect = true;
                student.TeachearIdApproved = -1;
                await conn.RunInTransactionAsync(conn =>
                {
                    conn.Update(student);
                });
            }
        }
    }
}
