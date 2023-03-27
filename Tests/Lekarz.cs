using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic_Project;

namespace mstesty_final
{
    [TestClass]
    public class TestLekarz
    {
        [TestMethod]
        public void TestKonstruktora()
        {
            Lekarz lekarz = new Lekarz();

            Assert.IsNotNull(lekarz.GodzinyPracy);
        }
        [TestMethod]
        public void TestCzyMoznaUmowic_1()
        {
            Lekarz lekarz = new Lekarz();
            Assert.ThrowsException<ArgumentException>(() => lekarz.SprawdzCzyMoznaUmowic("30.30.2023", new TimeSpan(13, 0, 0)));
        }
        [TestMethod]
        public void TestCzyMoznaUmowic_2()
        {
            Lekarz lekarz = new();
            Assert.IsFalse(lekarz.SprawdzCzyMoznaUmowic("30.01.2023", new TimeSpan(13, 15, 0)));

        }
        [TestMethod]
        public void TestCzyMoznaUmowic_3()
        {
            Lekarz lekarz = new Lekarz();
            lekarz.GodzinyPracy =
            new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>
            {
                { DayOfWeek.Thursday, new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0)) }
            };

            Assert.IsFalse(lekarz.SprawdzCzyMoznaUmowic("03.02.2023", new TimeSpan(13, 0, 0)));
        }
        [TestMethod]
        public void TestCzyMoznaUmowic_4()
        {
            Lekarz lekarz = new Lekarz();
            lekarz.GodzinyPracy =
            new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>
            {
                { DayOfWeek.Thursday, new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0)) }
            };
            lekarz.Zaplanowane_Wizyty = new Dictionary<Tuple<DateTime, TimeSpan>, bool>
            {
                { new Tuple<DateTime, TimeSpan>(new DateTime(2023,2,2), new TimeSpan(15, 0, 0)), true }
            };
            Assert.IsTrue(lekarz.SprawdzCzyMoznaUmowic("02.02.2023", new TimeSpan(13, 0, 0)));
        }
        [TestMethod]
        public void TestCzyMoznaUmowic_5()
        {
            Lekarz lekarz = new Lekarz();
            lekarz.GodzinyPracy =
            new Dictionary<DayOfWeek, Tuple<TimeSpan, TimeSpan>>
            {
                { DayOfWeek.Thursday, new Tuple<TimeSpan, TimeSpan>(new TimeSpan(8, 0, 0), new TimeSpan(16, 0, 0)) }
            };
            lekarz.Zaplanowane_Wizyty = new Dictionary<Tuple<DateTime, TimeSpan>, bool>
            {
                { new Tuple<DateTime, TimeSpan>(new DateTime(2023,2,2), new TimeSpan(13, 0, 0)), true }
            };
            Assert.IsFalse(lekarz.SprawdzCzyMoznaUmowic("02.02.2023", new TimeSpan(13, 0, 0)));
        }

    }
}