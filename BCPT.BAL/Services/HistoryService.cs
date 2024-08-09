using BCPT.ABSTACTION;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BCPT.BAL
{
    public class HistoryService : IHistoryService
    {
        private ApplicationDbContext _context;
        public HistoryService(ApplicationDbContext context)
        {
            this._context = context;
        }

        #region Public
        public async Task<Response> AddHistory(AddHistoryRequest addHistory)
        {
            var history = ModelMapper.MapAddHistory(addHistory);

            _context.Histories.Add(history);

            await _context.SaveChangesAsync();

            return new Response()
            {
                Code = System.Net.HttpStatusCode.Created,
                Message = SuccessMessage.HistoryCreated,
                Status = Status.Success,
            };
        }

        public async Task<Response<List<string>>> GetTopNHistory(int top, string? propertyName = null)
        {
            if (!string.IsNullOrEmpty(propertyName) && !propertyName.IsValidProperty())
                return new Response<List<string>>()
                {
                    Data = null,
                    Code = HttpStatusCode.BadRequest,
                    Message = ErrorMessage.ValidProperty,
                    Status = Status.Error
                };

            var suggestions = string.IsNullOrEmpty(propertyName) ?
                await _context.Histories
                .OrderByDescending(s => s.SearchDate)
                .Select(s => s.SearchParameter)
                .Distinct()
                .Take(3)
                .ToListAsync() :
                await _context.Histories
                .OrderByDescending(s => s.SearchDate)
                .Where(c => c.SearchProperty == propertyName.GetProperty())
                .Select(s => s.SearchParameter)
                .Distinct()
                .Take(3)
                .ToListAsync();

            return new Response<List<string>>()
            {
                Data = suggestions.Count() == 0 ? null : suggestions,
                Code = HttpStatusCode.OK,
                Message = suggestions.Count() == 0 ? SuccessMessage.NoSuggestionYet : string.Format(SuccessMessage.TopNSuggestion, suggestions.Count()),
                Status = Status.Success
            };
        }
        #endregion
    }
}
