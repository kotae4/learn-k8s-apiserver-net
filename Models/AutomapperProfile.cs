using AutoMapper;

namespace learn_k8s_apiserver_net.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Poll, PollResponseModel>();
            CreateMap<Poll, PollDetailsResponseModel>()
                .ForMember(dest => dest.VoteSummary, opt => opt.MapFrom(MapPollVoteSummary));
        }

        IDictionary<string, int> MapPollVoteSummary(Poll input, PollDetailsResponseModel output)
        {
            foreach (Choice c in input.Choices)
            {
                output.VoteSummary.Add(c.Value, 0);
            }
            foreach (Vote v in input.Votes)
            {
                if (output.VoteSummary.ContainsKey(v.Choice.Value))
                    output.VoteSummary[v.Choice.Value] += 1;
                else
                    output.VoteSummary[v.Choice.Value] = 1;
            }
            return output.VoteSummary;
        }
    }
}
