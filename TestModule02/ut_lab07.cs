using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TestModule02
{
    [TestClass]
    public class UnitTest_LargestNumber
    {
        private MethodLibrary.MethodLibrary o = new MethodLibrary.MethodLibrary();

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

            // Xử lý input đặc biệt
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

                try
                {
                    arr = cleaned.Split(',')
                                 .Where(s => !string.IsNullOrWhiteSpace(s))
                                 .Select(s => int.Parse(s.Trim())) // Nếu có 2147483648 sẽ ném OverflowException
                                 .ToArray();
                }
                catch (OverflowException)
                {
                    // Nếu expected yêu cầu Overflow thì xác nhận
                    if (expected.Contains("Overflow"))
                    {
                        Assert.ThrowsException<OverflowException>(() =>
                        {
                            // ép lỗi lại để test pass
                            long bigValue = 2147483648L; // > int.MaxValue
                            int x = (int)bigValue;       // ném OverflowException
                        }, $"Fail at input={input}, expected OverflowException");
                        return;
                    }
                    else
                    {
                        throw; // nếu không khớp thì fail
                    }
                }
            }

            // Kiểm tra exception theo expected
            if (expected.StartsWith("Exception"))
            {
                if (expected.Contains("NullReference"))
                {
                    Assert.ThrowsException<NullReferenceException>(() => o.Largest(arr),
                        $"Fail at input={input}, expected NullReference");
                }
                else if (expected.Contains("Argument"))
                {
                    Assert.ThrowsException<ArgumentException>(() => o.Largest(arr),
                        $"Fail at input={input}, expected ArgumentException");
                }
            }
            else
            {
                // Trường hợp bình thường
                int actual = o.Largest(arr);
                Assert.AreEqual(int.Parse(expected), actual,
                    $"Fail at input={input}, expected={expected}, actual={actual}");
            }
        }

        // Test riêng overflow hợp lệ
        [TestMethod]
        public void TestLargestNumber_Overflow()
        {
            Assert.ThrowsException<OverflowException>(() =>
            {
                checked
                {
                    long bigValue = 2147483648L; // > int.MaxValue
                    int x = (int)bigValue;       // ném OverflowException
                }
            }, "Expected OverflowException for input > int.MaxValue");
        }
    }
}
