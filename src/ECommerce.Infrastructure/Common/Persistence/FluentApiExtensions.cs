using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerce.Infrastructure.Common.Persistence;

public static class FluentApiExtensions
{
    public static PropertyBuilder<T> HasListOfIdsConverter<T>(this PropertyBuilder<T> builder)
    {
        return builder.HasConversion(
            new ListOfIdsConverter());
    }
}