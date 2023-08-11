using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;
using CustomerDemo.Models;

namespace CustomerDemo.Services;

public class CsvDataService
{
    public IEnumerable<TEntityType> ReadCsv<TEntityType>(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("File not found", filePath);
        }
        if (Path.GetExtension(filePath) != ".csv")
        {
            throw new FileLoadException("File is not a CSV file", filePath);
        }
        
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        using (var reader = new StreamReader(FormatFilePath(filePath)))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Context.RegisterClassMap<FooMap>();
            var records = csv.GetRecords<TEntityType>();
            return records.ToList();
        }
    }
    
    public void ConvertToCsv<T>(IEnumerable<T> data, string filePath)
    {
        
    }

    private string FormatFilePath(string originalFilePath)
    {
        var escapedFilePath = originalFilePath.Replace(@"\", @"\\");
        return escapedFilePath;
    }
}

public class FooMap : ClassMap<Client>
{
    public FooMap()
    {
        AutoMap(CultureInfo.InvariantCulture);
        Map(m => m.Id).Ignore();
        Map(m => m.SourceId).Ignore();
        Map(m => m.FirstName).Index(0);
        Map(m => m.LastName).Index(1);
        Map(m => m.Address).Index(2);
        Map(m => m.Email).Index(3);
        Map(m => m.Phone).Index(4);
    }
}