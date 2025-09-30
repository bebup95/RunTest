using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestModule02
{
    [TestClass]
    public class UnitTest_Max
    {
        private MethodLibrary.MethodLibrary o = new MethodLibrary.MethodLibrary();

        [DataTestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\MaxFunctionTests.csv",
                    "MaxFunctionTests#csv",
                    DataAccessMethod.Sequential)]
        public void TestMax_FromCSV()
        {
            // Arrange
            int stt = Convert.ToInt32(TestContext.DataRow["STT"]);
            int a = Convert.ToInt32(TestContext.DataRow["A"]);
            int b = Convert.ToInt32(TestContext.DataRow["B"]);
            int c = Convert.ToInt32(TestContext.DataRow["C"]);
            string expectedResult = TestContext.DataRow["Expected"].ToString();
            bool expectException = expectedResult.Equals("Exception", StringComparison.OrdinalIgnoreCase);

            // Act & Assert
            if (expectException)
            {
                Assert.ThrowsException<IndexOutOfRangeException>(() => o.Max(a, b, c),
                    $"Sai ở test case STT={stt} (A={a}, B={b}, C={c}): Ngoại lệ không được ném ra khi có giá trị không hợp lệ");
            }
            else
            {
                int expected = Convert.ToInt32(expectedResult);
                int actual = o.Max(a, b, c);
                Assert.AreEqual(expected, actual,
                    $"Sai ở test case STT={stt} (A={a}, B={b}, C={c}): Kết quả không đúng");
            }
        }

        public TestContext TestContext { get; set; }
    }
}
