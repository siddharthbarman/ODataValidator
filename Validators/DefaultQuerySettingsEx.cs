using Microsoft.AspNet.OData.Query;
using System;

namespace ODataSample.Validators {
	public class DefaultQuerySettingsEx : DefaultQuerySettings {
		public String DisallowedFilterProperties { get; set; }
	}
}
