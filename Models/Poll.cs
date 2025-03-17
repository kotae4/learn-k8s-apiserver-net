using System.ComponentModel.DataAnnotations;

namespace learn_k8s_apiserver_net.Models
{
    public class Poll
    {
        public long PollId { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int VoteCount { get; set; }

        public ICollection<Choice> Choices { get; set; } = new List<Choice>();
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
