using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace webManagerCMS.Data.Models.PageContent
{
    public enum PageContentPluginType
    {
		DOC_H1TEXT = -3,			//DONE
        PAGE_CORE = 1,				//DONE
        TREE_CORE = 2,				//DONE
		GALLERY1 = 3,				//DONE
		//NOTE1 = 4,					//NOT IMPLEMENTING!!!
		PICHEADER = 5,				//DONE
		LINKFOOTER = 6,
		TXTHEADER = 7,
		TREEDISPLAYDEFINED1 = 31,	//DONE
		DOC_HTML = 40				//DONE
	}
}
