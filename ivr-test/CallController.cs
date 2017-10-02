using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ivr_test
{
    [RoutePrefix("call")]
    public class CallController : ApiController
    {
        private IHubContext _context;

        public CallController()
        {
            _context = GlobalHost.ConnectionManager.GetHubContext<IvrHub>();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult NewCall(CallDetails details)
        {
            Console.WriteLine($"New Call: {details}");
            var id = details.Advisor;
            if (string.IsNullOrWhiteSpace(id))
            {
                return BadRequest("Must include the advisor's Hub ID in the call details.");
            }

            _context.Clients.Group(id).onNewCall(details);
            return Ok();
        }
    }
}
