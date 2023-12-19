namespace StaffRecords.Domain.Extensions
{
    public static class PropertyExtensions
    {
        public static bool IsSupportedType(this Type propertyType)
        {
            return propertyType.IsPrimitive ||
                   propertyType == typeof(string) ||
                   propertyType == typeof(Guid) ||
                   propertyType == typeof(DateTime) ||
                   propertyType == typeof(int?) ||
                   propertyType == typeof(double?) ||
                   propertyType == typeof(decimal) ||
                   propertyType == typeof(float?) ||
                   propertyType == typeof(long?) ||
                   propertyType == typeof(short?) ||
                   propertyType == typeof(byte?) ||
                   propertyType == typeof(Guid?) ||
                   propertyType == typeof(DateTime?);
        }
    }

}
