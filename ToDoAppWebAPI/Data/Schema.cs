namespace ToDoAppWebAPI.Data
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(Query query, Mutation mutation)
        {
            Query = query;
            Mutation = mutation;
        }
    }
}
