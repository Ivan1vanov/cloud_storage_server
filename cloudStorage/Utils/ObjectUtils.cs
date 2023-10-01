using CloudStorage.Models;

namespace CloudStorage.Utils
{
    public static class ObjectUtils {

        public static CheckForNullPropertiesResult CheckForNullProperties<T>(T obj) 
        {
            var properteisWithNullValue = obj
            .GetType()
            .GetProperties()
            .Where((property) => property.GetValue(obj) == null)
            .Select((property) => property.Name)
            .ToList();

            if(properteisWithNullValue.Any())
            {
                return new CheckForNullPropertiesResult(){
                    HasNullProperties = true,
                    NullProperties = properteisWithNullValue,
                };
            }

            return new CheckForNullPropertiesResult(){
                    HasNullProperties = false,
                };
        }

    }
}