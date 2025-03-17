using Microsoft.AspNetCore.Mvc;
using learn_k8s_apiserver_net.Models;
using AutoMapper;
using learn_k8s_apiserver_net.BusinessLogic;

namespace learn_k8s_apiserver_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly ILogger<VoteController> _logger;
        private readonly IPollingApiLogic _logic;
        private readonly IMapper _mapper;

        public VoteController(ILogger<VoteController> logger, IPollingApiLogic logic, IMapper mapper)
        {
            _logger = logger;
            _logic = logic;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Vote>> CreateVote([FromBody]CreateVoteRequestModel voteInput)
        {
            var res = await _logic.CreateVote(voteInput);
            return res;
        }
    }
}
