namespace RoomSense.Api.Lib.Services;

public class TimeStampGenerator : ITimeStampGenerator
{
    public DateTime GenerateTimeStamp()
    {
        return DateTime.Now;
    }
}