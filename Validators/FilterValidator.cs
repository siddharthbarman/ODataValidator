using Microsoft.AspNet.OData.Query;
using Microsoft.OData;
using Microsoft.OData.UriParser;

namespace ODataSample.Validators {
    public class FilterValidator : Microsoft.AspNet.OData.Query.Validators.FilterQueryValidator {
        public FilterValidator(DefaultQuerySettingsEx defaultQuerySettings) : base(defaultQuerySettings) {
            querySettings = defaultQuerySettings;
        }
        
        public override void Validate(FilterQueryOption filterQueryOption, ODataValidationSettings settings) {
            if (filterQueryOption.FilterClause.Expression.Kind == Microsoft.OData.UriParser.QueryNodeKind.BinaryOperator) {
                Microsoft.OData.UriParser.BinaryOperatorNode expr = filterQueryOption.FilterClause.Expression as Microsoft.OData.UriParser.BinaryOperatorNode;
                SingleValuePropertyAccessNode left = expr.Left as SingleValuePropertyAccessNode;
                if (left != null) {
                    string propertyName = left.Property.Name;
                    if (!ValidateFilterProperties(propertyName)) {
                        throw new ODataException(string.Format("{0} cannot be used for filtering", propertyName));
                    }
                }
            }            
        }

        private bool ValidateFilterProperties(string filterProperty) {
            bool result = true;
            if (querySettings != null && !string.IsNullOrEmpty(querySettings.DisallowedFilterProperties)) {
                string[] properties = querySettings.DisallowedFilterProperties.Split(",");
                foreach(string property in properties) {
                    if (string.Equals(property.Trim(), filterProperty, System.StringComparison.CurrentCultureIgnoreCase)) {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private DefaultQuerySettingsEx querySettings = null;
    }
}
