using RoomSense.Api.Lib.Services;

namespace RoomSense.Api.Lib.Tests;

public class TimeStampGeneratorTest
{
    private readonly ITimeStampGenerator _timeStampGenerator;

    public TimeStampGeneratorTest()
    {
        _timeStampGenerator = new TimeStampGenerator();
    }

    [Fact]
    public void GenerateTimeStamp_ShoulReturnADateTime()
    {
        var lesserTime = DateTime.Now;
        var generated = _timeStampGenerator.GenerateTimeStamp();
        
        Assert.True(lesserTime < generated);
    }
}