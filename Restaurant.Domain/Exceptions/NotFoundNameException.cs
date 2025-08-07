namespace Restaurant.Domain.Exceptions;

public class NotFoundNameException(string resourceType, string name)
: Exception($"{resourceType} with name: {name} doesn't exist")
{
}
