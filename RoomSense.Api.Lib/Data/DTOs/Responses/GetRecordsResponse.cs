namespace RoomSense.Api.Lib.Data.DTOs.Responses;

public class GetRecordsResponse
{
    public int Status { get; set; }
    public string Message { get; set; } = null!;
    public IEnumerable<GetAllRecords> Data { get; set; } = Enumerable.Empty<GetAllRecords>();
}