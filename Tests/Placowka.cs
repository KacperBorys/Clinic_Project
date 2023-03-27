using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic_Project;

namespace mstesty_final
{
    [TestClass]
    public class TestPlacowka
    {
        [TestMethod]
        public void TestKonstruktora()
        {
            Placowka placówka = new();
            Assert.IsNotNull(placówka.Konta);
        }

        [TestMethod]
        public void TestDodajPacjenta()
        {
            Placowka placówka = new();
            Pacjent pacjent = new Pacjent("Jan", "Kowalski", "01.01.2000", "00099988877", EnumPlec.M);
            placówka.DodajPacjenta(pacjent);
            Assert.IsTrue(placówka.Pacjenci.Contains(pacjent));
        }
        [TestMethod]
        public void TestUsunPacjenta()
        {
            Placowka placówka = new();
            Pacjent pacjent = new();
            pacjent.Pesel = "11111222233";
            Pacjent pacjent1 = new();
            placówka.DodajPacjenta(pacjent);
            placówka.DodajPacjenta(pacjent1);
            placówka.UsuńPacjenta("11111222233");
            Assert.IsFalse(placówka.Pacjenci.Contains(pacjent));
        }
        [TestMethod]
        public void TestDodajLekarza()
        {
            Lekarz lekarz = new();
            Placowka placówka = new();
            lekarz.Pesel = "99999888877";
            placówka.DodajLekarza(lekarz);
            Assert.IsTrue(placówka.Lekarze.Contains(lekarz));
        }

        [TestMethod]
        public void TestUsunLekarza1()
        {
            Lekarz lekarz = new();
            Lekarz lekarz1 = new();
            lekarz.Pesel = "22222222222";
            Placowka placówka = new();
            placówka.DodajLekarza(lekarz);
            placówka.DodajLekarza(lekarz1);
            placówka.UsunLekarza("22222222222");
            Assert.IsFalse(placówka.Lekarze.Contains(lekarz));
        }

        [TestMethod]
        public void TestDodajWizyte()
        {
            Pacjent pacjent = new();
            pacjent.Pesel = "55555555555";
            Lekarz lekarz = new();
            lekarz.GodzinyPracy =
            new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>
            {
                { DayOfWeek.Tuesday, new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0)) }
            };
            Wizyta wizyta = new("31.01.2023", lekarz, pacjent, new TimeSpan(13, 0, 0));
            Placowka placówka = new();
            placówka.DodajWizyte(wizyta);
            Assert.IsTrue(placówka.WizytyPacjenta("55555555555").Contains(wizyta));
        }

        [TestMethod]
        public void TestLekarzWDanymDniu()
        {
            Pacjent pacjent = new();
            Lekarz lekarz = new();
            lekarz.Pesel = "77777777777";
            lekarz.GodzinyPracy =
            new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>
            {
                { DayOfWeek.Tuesday, new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0)) }
            };
            Wizyta wizyta = new("31.01.2023", lekarz, pacjent, new TimeSpan(13, 0, 0));
            Placowka placówka = new();
            placówka.DodajWizyte(wizyta);
            Assert.IsTrue(placówka.LekarzWDanymDniu("77777777777", new DateTime(2023, 01, 31)).Contains(wizyta));
        }
        [TestMethod]
        public void TestWyszukajSpecjalizacje()
        {
            Lekarz lekarz = new();
            lekarz.Specjalizacja = "kardiolog";
            Lekarz lekarz1 = new();
            lekarz1.Specjalizacja = "chirurg";
            Placowka placówka = new();
            placówka.DodajLekarza(lekarz);
            placówka.DodajLekarza(lekarz1);
            Assert.IsTrue(placówka.WyszukajSpecjalizacja("kardiolog").Contains(lekarz));
        }
        [TestMethod]
        public void TestWyszukajSpecjalizacje1()
        {
            Lekarz lekarz = new();
            lekarz.Specjalizacja = "kardiolog";
            Lekarz lekarz1 = new();
            lekarz1.Specjalizacja = "chirurg";
            Placowka placówka = new();
            placówka.DodajLekarza(lekarz);
            placówka.DodajLekarza(lekarz1);
            Assert.IsFalse(placówka.WyszukajSpecjalizacja("chirurg").Contains(lekarz));
        }
    }
}