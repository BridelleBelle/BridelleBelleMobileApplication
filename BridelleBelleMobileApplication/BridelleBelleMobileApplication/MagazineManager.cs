using System;
using System.Collections.Generic;
using System.Text;
using BridelleBelleMobileApplication.Database;
using System.Threading.Tasks;
using BridelleBelleMobileApplication.Models;

namespace BridelleBelleMobileApplication
{
    public class MagazineManager
    {
	    private DocumnetDBClient docDb;

	    public MagazineManager()
	    {
		    docDb = new DocumnetDBClient();
	    }

	    public Task<Magazine> Get()
	    {
		    return docDb.Get("4");
	    }
    }
}
