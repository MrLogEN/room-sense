using RoomSense.Api.Lib.Services;

namespace RoomSense.Api.Lib.Tests;

public class IdGeneratorTest
{
    private readonly IIdGenerator _idGenerator;

    public IdGeneratorTest()
    {
        _idGenerator = new IdGenerator();
    }

    [Fact]
    public void GenerateId_ShouldCreateAGuid()
    {
        //arrange
        //action
        var actual = _idGenerator.GenerateId();
        //assert
        Assert.IsType<Guid>(actual);
    }
    [Fact]
    public void GenerateId_MultipleCallsShouldReturnDifferentGuids()
    {
        //arrange
        //action
        var id1 = _idGenerator.GenerateId();
        var id2 = _idGenerator.GenerateId();

        //assert
        Assert.NotEqual(id1,id2);
    }
}