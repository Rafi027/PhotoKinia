using System;
using System.Collections.Generic;
using System.Text;

namespace ImageSortingModule.Converters
{
    class MonthToString
    {
        private Dictionary<int, string> monthDictionary;

        public MonthToString()
        {
            monthDictionary = new Dictionary<int, string>();
            monthDictionary.Add(1, "Styczeń");
            monthDictionary.Add(2, "Luty");
            monthDictionary.Add(3, "Marzec");
            monthDictionary.Add(4, "Kwiecień");
            monthDictionary.Add(5, "Maj");
            monthDictionary.Add(6, "Czerwiec");
            monthDictionary.Add(7, "Lipiec");
            monthDictionary.Add(8, "Sierpień");
            monthDictionary.Add(9, "Wrzesień");
            monthDictionary.Add(10, "Październik");
            monthDictionary.Add(11, "Listopad");
            monthDictionary.Add(12, "Grudzień");
        }

        internal string Convert(int month)
        {
            if (!monthDictionary.ContainsKey(month))
                return null;
            return $"{month}. {monthDictionary[month]}";
        }
    }
}
