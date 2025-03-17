namespace learn_k8s_apiserver_net.Database
{
    public static class DbLogicServiceExtensions
    {
        public static IServiceCollection AddDbLogic(this IServiceCollection services)
        {
            services.AddTransient<IPollingDbLogic, PollingDbLogic>();

            return services;
        }
    }
}
