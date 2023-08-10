using System.Collections.Generic;

namespace CustomerDemo.Services;

public interface ICsvDataService
{
    public IEnumerable<EntityType> ReadCsv<EntityType>(string filePath);

    public void ConvertToCsv<T>(IEnumerable<T> data, string filePath);
}