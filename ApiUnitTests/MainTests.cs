using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Services;

namespace ApiUnitTests
{
    public class MainTests
    {
        [Fact]
        public void TestMethod()
        {

            var connectionString = "Server=localhost;Database=Szkola;User Id=sa;Password=Batw1ngs-Adm1n1!;TrustServerCertificate=True";
            
            var databaseService = new DatabaseService(connectionString);
            databaseService.ClearTables();

            var kursRozkladService = new KursRozkladService(connectionString);
            var result1 = kursRozkladService.GetKursIdAndRozkladOcen(1);
            var res1 = new Dictionary<string, string>();
            res1.Add("1", "2.0: 6.66666666666667% | 3.0: 20% | 3.5: 26.6666666666667% | 4.0: 20% | 4.5: 13.3333333333333% | 5.0: 0%");
            Assert.Equal(result1, res1);

            var kursService = new KursService(connectionString);
            var result2 = kursService.GetCzestoscPiatkiForKursId(1);
            var res2 = new Dictionary<string, string>();
            res2.Add("Matematyka zaawansowana", "Ilość piątek: 2, Część piątek z wszystkich ocen: 13.33%");
            Assert.Equal(result2, res2);
            var result3 = kursService.GetCzestoscPiatkiForKursId(2);
            var res3 = new Dictionary<string, string>();
            res3.Add("Jezyk Polski podstawowy", "Ilość piątek: 4, Część piątek z wszystkich ocen: 26.67%");
            Assert.Equal(result3, res3);

            var liczbaOcenService = new LiczbaOcenService(connectionString);
            var result4 = liczbaOcenService.GetLiczbaOcen(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-12-12"), 1);
            var result5 = liczbaOcenService.GetLiczbaOcen(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-12-12"), 2);
            var result6 = liczbaOcenService.GetLiczbaOcen(DateTime.Parse("2023-01-01"), DateTime.Parse("2023-12-12"), 3);
            var res4 = "4";
            var res5 = "5";
            var res6 = "4";
            Assert.Equal(result4, res4);
            Assert.Equal(result5, res5);
            Assert.Equal(result6, res6);

            var nauczycielService = new NauczycielService(connectionString);
            var result7 = nauczycielService.GetNauczycielIdAndSredniaOcen(1);
            var result8 = nauczycielService.GetNauczycielIdAndSredniaOcen(2);
            var res7 = new Dictionary<string, string>();
            var res8 = new Dictionary<string, string>();
            res7.Add("1", "3,40");
            res8.Add("2", "3,40");
            Assert.Equal(result7, res7);
            Assert.Equal(result8, res8);

            var liczbaOcenPozNegService = new LiczbaOcenPozNegService(connectionString);
            var result9 = liczbaOcenPozNegService.GetLiczbaOcenPozNeg(1);
            var result10 = liczbaOcenPozNegService.GetLiczbaOcenPozNeg(2);
            var res9 = "Oceny < 3.0: 3 | Oceny >= 3.0: 12";
            var res10 = "Oceny < 3.0: 4 | Oceny >= 3.0: 11";
            Assert.Equal(result9, res9);
            Assert.Equal(result10, res10);
        }
    }
}
