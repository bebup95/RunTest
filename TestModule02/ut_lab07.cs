using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TestModule02
{
    [TestClass]
    public class UnitTest_LargestNumber
    {
        private LargestFinder finder = new LargestFinder();

        public TestContext TestContext { get; set; }

        [DataTestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ut_lab07.csv",
                    "ut_lab07#csv",
                    DataAccessMethod.Sequential)]
        public void TestLargestNumber_FromCSV()
        {
            string input = TestContext.DataRow["input"].ToString().Trim();
            string expected = TestContext.DataRow["expected"].ToString().Trim();

            int[] arr = null;

            if (input.Equals("null", StringComparison.OrdinalIgnoreCase))
            {
                arr = null;
            }
            else if (input.Equals("[]"))
            {
                arr = new int[] { };
            }
            else
            {
                string cleaned = input.Replace("[", "").Replace("]", "").Trim();
                arr = cleaned.Split(',')
                             .Where(s => !string.IsNullOrWhiteSpace(s))
                             .Select(s => int.Parse(s.Trim()))
                             .ToArray();
            }

            if (expected.StartsWith("Exception"))
            {
                if (expected.Contains("Argument"))
                {
                    Assert.ThrowsException<ArgumentException>(() => finder.Largest(arr),
                        $"Sai ở input={input}, expected ArgumentException");
                }
                else if (expected.Contains("NullReference"))
                {
                    Assert.ThrowsException<NullReferenceException>(() => finder.Largest(arr),
                        $"Sai ở input={input}, expected NullReferenceException");
                }
            }
            else
            {
                int actual = finder.Largest(arr);
                int expectedValue = int.Parse(expected);
                Assert.AreEqual(expectedValue, actual,
                    $"Sai ở input={input}, expected={expected}, actual={actual}");
            }
        }

        [TestMethod]
        public void TestLargestNumber_Overflow()
        {
            Assert.ThrowsException<OverflowException>(() =>
            {
                checked
                {
                    long bigValue = 2147483648L; // > int.MaxValue
                    int x = (int)bigValue;       // sẽ ném OverflowException
                }
            }, "Expected OverflowException cho input > int.MaxValue");
        }
    }
}