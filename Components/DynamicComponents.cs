using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webManagerCMS.Data.Tenants;

namespace Components
{
	public class DynamicComponents : IDynamicComponents
	{
		public IDictionary<string, Type> Components => new Dictionary<string, Type>
		{
			{ "TREE_CORE_0_0", typeof(PAGE_TREE_0_0) },
			{ "PAGE_CORE_0_0", typeof(PAGE_CORE_0_0) },
			{ "DOC_HTML_5_0", typeof(DOC_HTML_5_0) }
		};

		public string test() {
			return "xxx";
		}

        public string test2()
        {
            return "yyy";
        }
    }
}
