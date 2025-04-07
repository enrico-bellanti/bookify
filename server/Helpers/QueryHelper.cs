using Bookify.Entities;
using System.Globalization;

namespace Bookify.Helpers
{
    public class QueryHelper
    {
        /// <summary>
        /// Parses a comma-separated string of include paths into a string array
        /// </summary>
        /// <param name="includes">A comma-separated string of entity properties to include</param>
        /// <returns>An array of include paths or an empty array if none provided</returns>
        public static string[] ParseIncludes(string includes)
        {
            return string.IsNullOrEmpty(includes)
                ? Array.Empty<string>()
                : includes.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }

        public static FilterQuery ParseFilters(string filters)
        {
            var result = new FilterQuery();

            if (string.IsNullOrWhiteSpace(filters))
                return result;

            try
            {
                // Expected format: field1:op1:value1;field2:op2:value2
                var filterItems = filters.Split(';', StringSplitOptions.RemoveEmptyEntries);

                foreach (var item in filterItems)
                {
                    var parts = item.Split(':', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 3)
                        continue;

                    var field = parts[0].Trim();
                    var op = ParseOperator(parts[1].Trim());
                    var value = ParseValue(parts[2].Trim(), op);

                    result.Filters.Add(new FilterItem
                    {
                        Field = field,
                        Operator = op,
                        Value = value
                    });
                }
            }
            catch (Exception)
            {
                // Log error if needed
                // Return empty filter set on parsing error
            }

            return result;
        }

        private static FilterOperator ParseOperator(string op)
        {
            return op.ToUpper() switch
            {
                "=" => FilterOperator.Equals,
                ">" => FilterOperator.GreaterThan,
                ">=" => FilterOperator.GreaterThanOrEqual,
                "IN" => FilterOperator.In,
                "<" => FilterOperator.LessThan,
                "<=" => FilterOperator.LessThanOrEqual,
                "!=" => FilterOperator.NotEqual,
                "LIKE" => FilterOperator.Like,
                _ => FilterOperator.Equals // Default to equals if not recognized
            };
        }

        private static object ParseValue(string value, FilterOperator op)
        {
            if (op == FilterOperator.In)
            {
                // For IN operator, we expect a comma-separated list
                // Remove the brackets if present [value1,value2]
                value = value.Trim('[', ']');
                return value.Split(',').Select(v => v.Trim()).ToArray();
            }

            // Try to parse as different types
            if (int.TryParse(value, out int intValue))
                return intValue;

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal decimalValue))
                return decimalValue;

            if (DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateValue))
                return dateValue;

            if (bool.TryParse(value, out bool boolValue))
                return boolValue;

            // Default to string if we can't parse as anything else
            return value;
        }
    }
}
