using BCPT.ABSTACTION;

namespace BCPT.BAL
{
    public interface IHistoryService
    {
        Task<Response> AddHistory(AddHistoryRequest addHistory);
        Task<Response<List<string>>> GetTopNHistory(int top ,string? propertyName = null);
    }
}
