using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Core.Services.ComponentService;

namespace Components
{
	public class DynamicComponents : IDynamicComponents
    {
		public IDictionary<string, Type> Components => new Dictionary<string, Type>
		{
			{ "Component1", typeof(Component1) }
		};

		public string test() {
			return "xxx";
		}
    }
}
