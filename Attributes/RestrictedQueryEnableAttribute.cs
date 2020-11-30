using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using ODataSample.Validators;

namespace ODataSample.Attributes {
	public class RestrictedQueryEnableAttribute : EnableQueryAttribute {
        public string DisallowedFilterProperties { 
            get; 
            set; 
        }

        public override void ValidateQuery(HttpRequest request, ODataQueryOptions queryOptions) {
            if (!string.IsNullOrEmpty(DisallowedFilterProperties) && queryOptions.Filter != null) {
                queryOptions.Filter.Validator = new FilterValidator(
                    new DefaultQuerySettingsEx {
                        DisallowedFilterProperties = DisallowedFilterProperties
                    });
            }            
            base.ValidateQuery(request, queryOptions);
        }        
    }
}
