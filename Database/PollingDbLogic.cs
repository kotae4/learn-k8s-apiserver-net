using AutoMapper;
using learn_k8s_apiserver_net.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_k8s_apiserver_net.Database
{
    public class PollingDbLogic : IPollingDbLogic
    {
        private readonly PollingApiDbContext _context;

        public PollingDbLogic(PollingApiDbContext context) { _context = context; }

        public async Task<Poll> CreatePoll(string title, string author, DateTime expiration, string shortDesc, string longDesc)
        {
            var newPoll = new Poll() { Title = title, User=author, Expiration = expiration, ShortDescription=shortDesc, LongDescription=longDesc };
            _context.Polls.Add(newPoll);
            await _context.SaveChangesAsync();
            return newPoll;
        }

        public async Task<Poll> RegisterChoices(long pollId, List<string> choices)
        {
            var poll = await GetPollById(pollId);
            foreach (string choice in choices)
            {
                var newChoice = new Choice() { PollId = pollId, Value = choice };
                poll.Choices.Add(newChoice);
            }
            await _context.SaveChangesAsync();
            return poll;
        }

        public async Task<List<Poll>> ListPolls(int offset = 0, int limit = 50)
        {
            var polls = await _context.Polls
                .OrderBy(row => row.PollId)
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
            return polls;
        }

        public async Task<Poll> GetPollById(long id)
        {
            var poll = await _context.Polls.Include(poll => poll.Choices).Include(poll=>poll.Votes).SingleAsync(row => row.PollId == id);
            return poll;
        }

        public async Task<Vote> CreateVote(long pollId, long choiceId, string user)
        {
            var poll = await GetPollById(pollId);
            var newVote = new Vote() { PollId = pollId, ChoiceId = choiceId, User = user };
            poll.Votes.Add(newVote);
            await _context.SaveChangesAsync();
            return newVote;
        }
    }
}
