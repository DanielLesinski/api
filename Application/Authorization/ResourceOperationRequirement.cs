using Microsoft.AspNetCore.Authorization;

namespace Application.Authorization
{
    public enum ResorceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }

    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResorceOperation resorceOperation)
        {
            ResorceOperation = resorceOperation;
        }

        public ResorceOperation ResorceOperation { get; }
    }
}
