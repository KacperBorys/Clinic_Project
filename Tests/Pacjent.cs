using Clinic_Project;


namespace mstesty_final
{
    [TestClass]
    public class TestPacjent
    {
        [TestMethod]
        public void TestPesel()
        {
            string pes = "77777";
            Pacjent pacjent = new Pacjent();
            Assert.ThrowsException<ArgumentException>(() => pacjent.Pesel = pes);
        }
        [TestMethod]
        public void TestKonstruktora()
        {
            Pacjent pacjent = new Pacjent();
            Assert.IsNotNull(pacjent.HistoriaWizyt);
        }
        [TestMethod]
        public void TestKonstruktora1()
        {
            string oczekiwana = "00099988877";
            Pacjent pacjent = new Pacjent("Jan", "Kowalski", "01.01.2000", "00099988877", EnumPlec.M);
            Assert.AreEqual(oczekiwana, pacjent.Pesel);
        }
        [TestMethod]
        public void TestDodajDiagnozeRecepta()
        {
            Wizyta w1 = new Wizyta();
            Pacjent pacjent = new Pacjent();
            Diagnoza diagnoza = new Diagnoza(w1, "Zapalenie pluc", "Amoksycylina");
            pacjent.DodajDiagnoze(diagnoza);
            Assert.IsTrue(pacjent.HistoriaWizyt.Contains(diagnoza));
        }
    }
}