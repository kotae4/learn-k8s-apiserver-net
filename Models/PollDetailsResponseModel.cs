namespace learn_k8s_apiserver_net.Models
{
    public class PollDetailsResponseModel
    {
        public long PollId { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public DateTime Expiration { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public int VoteCount { get; set; }
        public ICollection<Choice> Choices { get; set; } = new List<Choice>();
        public IDictionary<string, int> VoteSummary { get; set; } = new Dictionary<string, int>();
    }
}
