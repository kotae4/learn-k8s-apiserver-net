using Microsoft.EntityFrameworkCore;

namespace learn_k8s_apiserver_net.Models
{
    [PrimaryKey(nameof(PollId), nameof(User))]
    public class Vote
    {
        public long PollId { get; set; }
        public long ChoiceId { get; set; }
        public string User { get; set; }
        public DateTime VoteTime { get; set; }

        public Poll Poll { get; set; } = null!;
        public Choice Choice { get; set; } = null!;
    }
}
