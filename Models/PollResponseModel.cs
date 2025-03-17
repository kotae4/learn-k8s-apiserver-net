namespace learn_k8s_apiserver_net.Models
{
    public class PollResponseModel
    {
        public long PollId { get; set; }
        public string Title { get; set; }
        public string User { get; set; }
        public DateTime Expiration { get; set; }
        public string ShortDescription { get; set; }
        public int VoteCount { get; set; }
    }
}
