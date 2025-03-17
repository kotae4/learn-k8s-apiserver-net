namespace learn_k8s_apiserver_net.BusinessLogic
{
    public static class BusinessLogicServiceExtensions
    {
        public static IServiceCollection RegisterBusinessLogic(this IServiceCollection services)
        {
            services.AddTransient<IPollingApiLogic, PollingApiLogic>();

            return services;
        }
    }
}
