using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestModule02
{
    [TestClass]
    public class UnitTest_TinhTienDien
    {
        private MethodLibrary.MethodLibrary o = new MethodLibrary.MethodLibrary();

        [DataTestMethod]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV",
                    "|DataDirectory|\\ElectricityBillTests.csv",
                    "ElectricityBillTests#csv",
                    DataAccessMethod.Sequential)]
        public void TestTinhTienDien_FromCSV()
        {
            // Arrange
            int stt = Convert.ToInt32(TestContext.DataRow["STT"]);
            int chiSoCu = Convert.ToInt32(TestContext.DataRow["ChiSoCu"]);
            int chiSoMoi = Convert.ToInt32(TestContext.DataRow["ChiSoMoi"]);
            double expected = Convert.ToDouble(TestContext.DataRow["Expected"]);

            // Act
            double actual = o.TinhTienDien(chiSoCu, chiSoMoi);

            // Assert
            Assert.AreEqual(expected, Math.Round(actual, 0),
                $"Sai ở test case STT={stt} (ChiSoCu={chiSoCu}, ChiSoMoi={chiSoMoi})");
        }

        public TestContext TestContext { get; set; }
    }
}
