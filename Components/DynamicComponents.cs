﻿using System;
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
			{ "PAGE_CORE_0_1", typeof(PAGE_CORE_0_1) },
			{ "DOC_HTML_5_0", typeof(DOC_HTML_5_0) },
			{ "TREEDISPLAYDEFINED1_0_0", typeof(TREEDISPLAYDEFINED1_0_0) },
			{ "DOC_H1TEXT_0_0", typeof(DOC_H1TEXT_0_0) },
			{ "GALLERY1_0_0", typeof(GALLERY1_0_0) },
			{ "GALLERY1_0_1", typeof(GALLERY1_0_1) }
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
