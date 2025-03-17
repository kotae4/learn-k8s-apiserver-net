using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace learn_k8s_apiserver_net.Models
{
    [PrimaryKey(nameof(ChoiceId)), Index(nameof(PollId), nameof(Value), IsUnique = true)]
    public class Choice
    {
        public long ChoiceId { get; set; }
        public long PollId { get; set; }
        [Required]
        public string Value { get; set; }
    }
}
