using learn_k8s_apiserver_net.Models;

namespace learn_k8s_apiserver_net.Database
{
    public interface IPollingDbLogic
    {
        public Task<Poll> CreatePoll(string title, string author, DateTime expiration, string shortDesc, string longDesc);
        public Task<Poll> RegisterChoices(long pollId, List<string> choices);
        public Task<List<Poll>> ListPolls(int offset = 0, int limit = 50);
        public Task<Poll> GetPollById(long id);

        public Task<Vote> CreateVote(long pollId, long choiceId, string user);
    }
}
