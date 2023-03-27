using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic_Project;

namespace mstesty_final
{
    [TestClass]
    public class TestDiagnoza
    {
        [TestMethod]
        public void TestKonstruktora()
        {
            Diagnoza diagnoza = new Diagnoza();
            Assert.IsNotNull(diagnoza);
        }
        [TestMethod]
        public void TestKonstruktora1()
        {
            Diagnoza diagnoza = new Diagnoza();
            diagnoza.Recepta = "Paracetamol";
            diagnoza.Choroba = "Przeziębienie";
            Assert.AreEqual("Paracetamol", diagnoza.Recepta);
        }
    }
}