using Bookify.Entities;
using System.Linq.Expressions;
using System.Reflection;

namespace Bookify.Extensions
{
    //GET / api / accommodations ? filters = field1 :operator:value1; field2:operator:value2
    //GET / api / accommodations ? filters = Name : LIKE:beach
    //GET / api / accommodations ? filters = City : IN:[New York, Paris, London]
    //GET / api / accommodations ? filters = Price :>:100
    //GET / api / accommodations ? filters = Name : LIKE:beach; Price:>:100; Rating:>=:4
    //GET / api / accommodations ? filters = Address.City :=:Boston
    public static class FilterExtensions
    {
        public static Expression<Func<T, bool>> BuildFilterExpression<T>(this FilterQuery filterQuery)
        {
            // If no filters, return a predicate that always returns true
            if (filterQuery == null || !filterQuery.Filters.Any())
                return entity => true;

            var parameter = Expression.Parameter(typeof(T), "entity");
            Expression combinedExpression = null;

            foreach (var filter in filterQuery.Filters)
            {
                var propertyPath = filter.Field.Split('.');
                Expression propertyAccess = parameter;

                // Navigate through nested properties if any (e.g., "Address.City")
                foreach (var prop in propertyPath)
                {
                    propertyAccess = Expression.Property(propertyAccess, prop);
                }

                var filterExpression = BuildComparisonExpression(propertyAccess, filter);

                // Combine expressions with AND
                combinedExpression = combinedExpression == null
                    ? filterExpression
                    : Expression.AndAlso(combinedExpression, filterExpression);
            }

            return Expression.Lambda<Func<T, bool>>(combinedExpression ?? Expression.Constant(true), parameter);
        }

        private static Expression BuildComparisonExpression(Expression propertyAccess, FilterItem filter)
        {
            var propertyType = ((MemberExpression)propertyAccess).Type;
            Expression valueExpression;

            switch (filter.Operator)
            {
                case FilterOperator.Equals:
                    valueExpression = Expression.Constant(ConvertValue(filter.Value, propertyType));
                    return Expression.Equal(propertyAccess, valueExpression);

                case FilterOperator.NotEqual:
                    valueExpression = Expression.Constant(ConvertValue(filter.Value, propertyType));
                    return Expression.NotEqual(propertyAccess, valueExpression);

                case FilterOperator.GreaterThan:
                    valueExpression = Expression.Constant(ConvertValue(filter.Value, propertyType));
                    return Expression.GreaterThan(propertyAccess, valueExpression);

                case FilterOperator.GreaterThanOrEqual:
                    valueExpression = Expression.Constant(ConvertValue(filter.Value, propertyType));
                    return Expression.GreaterThanOrEqual(propertyAccess, valueExpression);

                case FilterOperator.LessThan:
                    valueExpression = Expression.Constant(ConvertValue(filter.Value, propertyType));
                    return Expression.LessThan(propertyAccess, valueExpression);

                case FilterOperator.LessThanOrEqual:
                    valueExpression = Expression.Constant(ConvertValue(filter.Value, propertyType));
                    return Expression.LessThanOrEqual(propertyAccess, valueExpression);

                case FilterOperator.Like:
                    // For string properties, we use Contains method
                    if (propertyType == typeof(string))
                    {
                        var valueToFind = filter.Value.ToString();

                        // Method 1: Using ToLower() for case insensitivity
                        // This works across different database providers
                        var toLowerMethod = typeof(string).GetMethod("ToLower", Type.EmptyTypes);
                        var lowerProperty = Expression.Call(propertyAccess, toLowerMethod);

                        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                        var lowerValue = valueToFind.ToLower();
                        valueExpression = Expression.Constant(lowerValue);

                        return Expression.Call(lowerProperty, containsMethod, valueExpression);
                    }
                    throw new NotSupportedException("LIKE operator is only supported for string properties");

                case FilterOperator.In:
                    // For IN operator, we need to check if the property is in a list of values
                    if (filter.Value is string[] values)
                    {
                        var convertedValues = values.Select(v => ConvertValue(v, propertyType));
                        var listType = typeof(List<>).MakeGenericType(propertyType);
                        var list = Activator.CreateInstance(listType);
                        var addMethod = listType.GetMethod("Add");

                        foreach (var val in convertedValues)
                        {
                            addMethod.Invoke(list, new[] { val });
                        }

                        var containsMethod = listType.GetMethod("Contains", new[] { propertyType });
                        return Expression.Call(Expression.Constant(list), containsMethod, propertyAccess);
                    }
                    throw new NotSupportedException("IN operator requires an array of values");

                default:
                    throw new NotSupportedException($"Operator {filter.Operator} is not supported");
            }
        }

        private static object ConvertValue(object value, Type targetType)
        {
            if (value == null)
                return null;

            if (targetType.IsEnum && value is string stringValue)
                return Enum.Parse(targetType, stringValue);

            if (targetType == typeof(Guid) && value is string guidString)
                return Guid.Parse(guidString);

            try
            {
                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                return value;
            }
        }
    }
}
