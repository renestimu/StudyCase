using CsvHelper.Configuration;
using StudyCase.Domain.Entities;
using System.Globalization;

namespace StudyCaseAPI
{
    public sealed class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Ignore();
            Map(m => m.Name);
            Map(m => m.Description);
            Map(m => m.Stock);
            Map(m => m.Price);
            Map(m => m.CreateTime).TypeConverterOption.Format("dd/MM/yyyy");
        }
    }
}
