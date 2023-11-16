using System.Globalization;
using CsvHelper;
using CsvHelper.TypeConversion;

namespace ETSample
{
    public class Programmer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public string Country { get; set; }
    }

    public class LINQExpert
    {
        public Programmer Programmer;
        
        public LINQExpert(Programmer programmer)
        {
            Programmer = programmer;
        }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public static class Filler
    {
        public static IEnumerable<Programmer> FillInProgrammers()
        {
            using var reader = new StreamReader(@"testdata.csv");
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            
            var options = new TypeConverterOptions { Formats = new[] { "dd/MM/yyyy" } };
            csv.Context.TypeConverterOptionsCache.AddOptions<DateTime>(options);
            
            var records = csv.GetRecords<Programmer>();

            return records.ToList();
        }
    }
}