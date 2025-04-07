using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bookify.Entities
{
    public enum FilterOperator
    {
        Equals,
        GreaterThan,
        GreaterThanOrEqual,
        In,
        LessThan,
        LessThanOrEqual,
        NotEqual,
        Like
    }

    public class FilterItem
    {
        public string Field { get; set; }
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }
    }

    public class FilterQuery
    {
        public List<FilterItem> Filters { get; set; } = new List<FilterItem>();
    }
}