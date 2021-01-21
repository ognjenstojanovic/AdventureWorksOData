using AdventureWorksOData.Database;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureWorksOData.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PersonController : ControllerBase
    {
        private readonly AdventureWorks2019Context _context;

        public PersonController(AdventureWorks2019Context context)
        {
            _context = context;
        }

        [EnableQuery()]
        [ODataRoute]
        public IActionResult Get()
        {
            return Ok(_context.People.Take(100).AsQueryable());
        }

        [ODataRoute("({key})")]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(_context.People.Find(key));
        }
    }
}
