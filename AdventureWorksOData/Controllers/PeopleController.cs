using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdventureWorksOData.Database;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNet.OData.Query;

namespace AdventureWorksOData.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PeopleController : ODataController
    {
        private readonly AdventureWorksContext _context;

        public PeopleController(AdventureWorksContext context)
        {
            _context = context;
        }

        [EnableQuery()]
        [ODataRoute]
        public IActionResult Get()
        {
            return Ok(_context.Person.AsQueryable());
        }

        [ODataRoute("({key})")]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        public IActionResult Get([FromODataUri]int key)
        {
            return Ok(_context.Person.Find(key));
        }   
    }
}