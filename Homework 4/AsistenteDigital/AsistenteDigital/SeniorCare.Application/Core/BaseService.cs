using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeniorCare.Application.Core
{
    public class BaseService
    {
        public bool IsValidString(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        public bool IsValidDate(DateTime date)
        {
            return date > DateTime.MinValue && date < DateTime.MaxValue;
        }

        public bool IsPositiveNumber(int number)
        {
            return number > 0;
        }
    }
}
