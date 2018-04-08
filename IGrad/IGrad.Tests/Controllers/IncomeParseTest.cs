using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGrad.Tests.Controllers
{
    [TestClass]
    public class IncomeParseTest
    {
        [TestMethod]
        public void TestIncomeRegEx()
        {
            string income = "$0 to $100";
            int value1 = -1;
            int value2 = -1;

            Match match = Regex.Match(income, @"[0-9]+", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                value1 = Convert.ToInt32(match.ToString());
                income = income.Remove(match.Index, match.Length);
                Match subMatch = Regex.Match(income, @"[0-9]+", RegexOptions.IgnoreCase);
                if(subMatch.Success)
                {
                    value2 = Convert.ToInt32(subMatch.ToString());
                }
            }

            Assert.IsTrue(value1 == 0);
            Assert.IsTrue(value2 == 100);
        }

        [TestMethod]
        public void TestIncomeRegExLarger()
        {
            string income = "$2500 to $19750";
            int value1 = -1;
            int value2 = -1;

            Match match = Regex.Match(income, @"[0-9]+", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                value1 = Convert.ToInt32(match.ToString());
                income = income.Remove(match.Index, match.Length);
                Match subMatch = Regex.Match(income, @"[0-9]+", RegexOptions.IgnoreCase);
                if (subMatch.Success)
                {
                    value2 = Convert.ToInt32(subMatch.ToString());
                }
            }

            Assert.IsTrue(value1 == 2500);
            Assert.IsTrue(value2 == 19750);
        }
    }
}
