using MinorsApplication.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinorsApplication.Functions
{
    public class StringNullOrEmptyToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string stringValue = value as string;

            // Проверяем, является ли значение пустым или null
            if (string.IsNullOrEmpty(stringValue))
            {
                // Возвращаем false
                return false;
            }

            // Возвращаем true
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Не требуется
            throw new NotImplementedException();
        }
    }

    public class EmptyCollectionToTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection collection && collection.Count == 0)
            {
                return "Поданных заявок нет";
            }

            return "Студенты, выбравшие ваш майнор";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ListNullOrEmptyToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            List<StudentInfo> listValue = value as List<StudentInfo>;

            // Проверяем, является ли значение пустым или null
            if (listValue == null || listValue.Count == 0)
            {
                // Возвращаем false
                return false;
            }

            // Возвращаем true
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Не требуется
            throw new NotImplementedException();
        }
    }
}
