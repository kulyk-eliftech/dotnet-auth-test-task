namespace Entities.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException()
        : base("User doesn't exist.")
    {
    }

    public UserNotFoundException(Guid id)
        : base($"User with id: {id} doesn't exist.")
    {
    }
}