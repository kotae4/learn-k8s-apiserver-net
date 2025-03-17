using learn_k8s_apiserver_net.Models;
using Microsoft.EntityFrameworkCore;

namespace learn_k8s_apiserver_net.Database
{
    public class PollingApiDbContext : DbContext
    {
        public PollingApiDbContext(DbContextOptions<PollingApiDbContext> options) : base(options)
        {

        }

        public DbSet<Poll> Polls { get; set; }
    }
}
