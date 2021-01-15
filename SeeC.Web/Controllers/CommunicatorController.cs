using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SeeC.Web.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SeeC.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunicatorController : Controller
    {
        private IHubContext<ChatHub> _hubContext;

        public CommunicatorController(IHubContext<ChatHub> hubcontext)
        {
            _hubContext = hubcontext;
        }

        // POST api/<CommunicatorController>
        [HttpPost("room/{roomId}")]
        public async Task<string> SendMachineInformationToRoom([FromRoute] string roomId, [FromBody] MachineInformation deviceInformation)
        {
            try
            {
                await _hubContext.Clients.Group(roomId).SendAsync("OnReceiveMachineInformation", deviceInformation.Information);
                return "Success";
            }
            catch (Exception ex)
            {
                return "There is an error in the communicator" + ex.Message;
            }
        }

        [HttpGet("room/{roomId}/{deviceInformation}")]
        public async Task<string> SendMachineInformationToRoom([FromRoute] string roomId, [FromRoute] string deviceInformation)
        {
            try
            {
                await _hubContext.Clients.Group(roomId).SendAsync("OnReceiveMachineInformation", deviceInformation);
                return "Success";
            }
            catch (Exception ex)
            {
                return "There is an error in the communicator" + ex.Message;
            }
        }
    }

    public class MachineInformation
    {
        public string Information { get; set; }
    }
}
