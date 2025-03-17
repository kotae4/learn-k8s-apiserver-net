namespace learn_k8s_apiserver_net.Models
{
    public class CreatePollRequestModel
    {
        public string Title { get; set; }
        public string User { get; set; }
        public DateTime Expiration { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public List<string> Choices { get; set; }
    }
}
