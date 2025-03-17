using AutoMapper;
using learn_k8s_apiserver_net.BusinessLogic;
using learn_k8s_apiserver_net.Models;
using Microsoft.AspNetCore.Mvc;

namespace learn_k8s_apiserver_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PollController : ControllerBase
    {
        private readonly ILogger<PollController> _logger;
        private readonly IPollingApiLogic _logic;
        private readonly IMapper _mapper;

        public PollController(ILogger<PollController> logger, IPollingApiLogic logic, IMapper mapper)
        {
            _logger = logger;
            _logic = logic;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<PollDetailsResponseModel>> CreatePoll([FromBody] CreatePollRequestModel pollInput)
        {
            var res = await _logic.CreatePoll(pollInput);
            return _mapper.Map<PollDetailsResponseModel>(res);
        }

        [HttpGet]
        public async Task<ActionResult<List<PollResponseModel>>> ListPolls([FromQuery] int offset = 0, [FromQuery] int limit = 50)
        {
            var res = await _logic.ListPolls(offset, limit);
            return _mapper.Map<List<PollResponseModel>>(res);
        }

        [HttpGet("{pollId:int}")]
        public async Task<ActionResult<PollDetailsResponseModel>> GetPoll([FromRoute] int pollId)
        {
            var res = await _logic.GetPollById(pollId);
            return _mapper.Map<PollDetailsResponseModel>(res);
        }
    }
}
