
using learn_k8s_apiserver_net.Models;

namespace learn_k8s_apiserver_net.BusinessLogic
{
    public interface IPollingApiLogic
    {
        public Task<Poll> CreatePoll(CreatePollRequestModel createPollParams);
        public Task<List<Poll>> ListPolls(int offset = 0, int limit = 50);
        public Task<Poll> GetPollById(long pollId);

        public Task<Vote> CreateVote(CreateVoteRequestModel createVoteParams);
    }
}
