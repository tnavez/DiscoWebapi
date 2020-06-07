using Disco.Entities;
using Disco.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Disco.Validator
{
    public class Tools
    {
        public static bool IsValidNN(string niss)
        {
            Regex regnnn = new Regex(@"^[0-9]{3}[.]{0,1}[0-9]{2}[.]{0,1}[0-9]{2}[-]{0,1}[0-9]{3}[-]{0,1}[0-9]{2}$");
            if (regnnn.IsMatch(niss)) return true;
            else return false;
        }

        public static bool CheckMinAge(DateTime dateTime)
        {
            int age = new DateTime(DateTime.Now.Subtract(dateTime).Ticks).Year - 1;

            if (age >= 18)
            {
                return true;
            }
            else { return false; }
        }
    }
}
