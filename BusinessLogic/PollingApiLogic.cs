using learn_k8s_apiserver_net.Database;
using learn_k8s_apiserver_net.Models;

namespace learn_k8s_apiserver_net.BusinessLogic
{
    public class PollingApiLogic : IPollingApiLogic
    {
        private readonly IPollingDbLogic _pollingDb;
        public PollingApiLogic(IPollingDbLogic pollingDb) { _pollingDb = pollingDb; }

        public async Task<Poll> CreatePoll(CreatePollRequestModel createPollParams)
        {
            var newPoll = await _pollingDb.CreatePoll(createPollParams.Title, createPollParams.User, createPollParams.Expiration, createPollParams.ShortDescription, createPollParams.LongDescription);
            newPoll = await _pollingDb.RegisterChoices(newPoll.PollId, createPollParams.Choices);
            return newPoll;
        }

        public async Task<List<Poll>> ListPolls(int offset = 0, int limit = 50)
        {
            var polls = await _pollingDb.ListPolls(offset, limit);
            return polls;
        }

        public async Task<Poll> GetPollById(long pollId)
        {
            var poll = await _pollingDb.GetPollById(pollId);
            return poll;
        }

        public async Task<Vote> CreateVote(CreateVoteRequestModel createVoteParams)
        {
            var newVote = await _pollingDb.CreateVote(createVoteParams.PollId, createVoteParams.ChoiceId, createVoteParams.User);
            return newVote;
        }
    }
}
