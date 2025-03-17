namespace learn_k8s_apiserver_net.Models
{
    public class CreateVoteRequestModel
    {
        public long PollId { get; set; }
        public long ChoiceId { get; set; }
        public string User { get; set; }
    }
}
