using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECommerce.Infrastructure.Common.Persistence;

public class ListOfIdsConverter : ValueConverter<List<Guid> , string>
{
    public ListOfIdsConverter(ConverterMappingHints? mappingHints = null) 
        : base(
            c => string.Join(',' , c),
            c => c.Split(',' , StringSplitOptions.RemoveEmptyEntries)
                .Select(Guid.Parse).ToList(),
            mappingHints
            ) { }
}