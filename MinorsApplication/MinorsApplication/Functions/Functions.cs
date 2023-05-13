using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System;
using System.Text;
namespace MinorsApplication.Functions
{
    public static class Functions
    {
        public static string CheckStatus(string email)
        {
            // Паттерн для поиска адресов электронной почты с доменом @edu.hse.ru
            string pattern1 = @"([a-zA-Z0-9._%+-]+)@edu\.hse\.ru";
            // Паттерн для поиска адресов электронной почты с доменом @hse.ru
            string pattern2 = @"([a-zA-Z0-9._%+-]+)@hse\.ru";

            // Создаем объект Regex для каждого шаблона
            Regex regex1 = new Regex(pattern1);
            Regex regex2 = new Regex(pattern2);

            // Проверяем соответствие адреса электронной почты шаблонам
            bool isMatch1 = regex1.IsMatch(email);
            bool isMatch2 = regex2.IsMatch(email);

            if (isMatch1)
                return "Студент";
            else if (isMatch2)
                return "Преподаватель";
            else
                return String.Empty;
        }

        // Шифрование пароля.
        public static string hashPassword(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (var a in hash)
                sb.Append(a.ToString("X2"));
            return Convert.ToString(sb);

        }
    }
}
