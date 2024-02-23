using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Core.Services.ComponentService;

namespace Components
{
	internal class DynamicComponents : IDynamicComponents
	{
		public IDictionary<string, Type> Components => new Dictionary<string, Type>
		{
			{ "Component1", typeof(Component1) }
		};
	}
}
