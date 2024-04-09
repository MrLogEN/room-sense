namespace RoomSense.Api.Lib.Services;

public class IdGenerator : IIdGenerator
{
    public Guid GenerateId()
    {
        return Guid.NewGuid();
    }
}