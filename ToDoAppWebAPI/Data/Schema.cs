namespace ToDoAppWebAPI.Data
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            Query = serviceProvider.GetRequiredService<Query>();
            Mutation = serviceProvider.GetRequiredService<Mutation>();
        }
    }
}
