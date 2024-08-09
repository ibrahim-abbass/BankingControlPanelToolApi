using BCPT.ABSTACTION;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BCPT.BAL
{
    public class ClientService : IClientService
    {
        private ApplicationDbContext _context;
        #region Public
        public ClientService(ApplicationDbContext context)
        {
            this._context = context;
        }
        public async Task<Response> AddClient(InsertClientRequest insertClient)
        {
            var isUserExist = _context.Clients
                .Any(dbc => dbc.Email.ToUpper() == insertClient.Email.ToUpper());

            if (isUserExist)
                return new Response()
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = Status.Error,
                    Message = string.Format(ErrorMessage.UserExist, "Email")
                };

            var client = ModelMapper.MapInsertClient(insertClient);

            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return new Response()
            {
                Code = HttpStatusCode.Created,
                Message = string.Format(SuccessMessage.UserCreated, "Client"),
                Status = Status.Success
            };
        }
        public async Task<Response<ICollection<Client>>> GetClients()
        {
            var clients = await _context.Clients
                .Include(address => address.Address)
                .Include(account => account.Accounts)
                .ToListAsync();

            return new Response<ICollection<Client>>()
            {
                Code = HttpStatusCode.OK,
                Message = clients.Count() == 0 ? SuccessMessage.NoClientFound : SuccessMessage.RetreiveClients,
                Status = Status.Success,
                Data = clients.Count() == 0 ? null : clients
            };
        }
        public async Task<Response> DeleteClient(Guid id)
        {
            var client = _context.Clients
               .FirstOrDefault(dbc => dbc.Id == id);

            if (client == null)
                return new Response()
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = Status.Error,
                    Message = ErrorMessage.UserNotFound
                };

            _context.Clients.Remove(client);

            await _context.SaveChangesAsync();

            return new Response()
            {
                Code = HttpStatusCode.OK,
                Status = Status.Success,
                Message = SuccessMessage.Deleted
            };
        }
        public async Task<Response<ICollection<Client>>> FilterClients(FilterRequest filter)
        {
            if (!string.IsNullOrEmpty(filter.FilterBy) && !filter.FilterBy.IsValidProperty())
                return new Response<ICollection<Client>>()
                {
                    Data = null,
                    Code = HttpStatusCode.BadRequest,
                    Message = ErrorMessage.ValidProperty,
                    Status = Status.Error
                };

            var clients = await GetPaginatedClients(filter);

            return new Response<ICollection<Client>>()
            {
                Code = HttpStatusCode.OK,
                Message = clients.Count() == 0 ? SuccessMessage.NoClientFound : SuccessMessage.RetreiveClients,
                Status = Status.Success,
                Data = clients.Count() == 0 ? null : clients
            };
        }
        public async Task<Response<ICollection<Client>>> SortClients(string sortBy, bool isAsc = true)
        {
            if (!sortBy.IsValidProperty())
                return new Response<ICollection<Client>>()
                {
                    Code = HttpStatusCode.BadRequest,
                    Message = ErrorMessage.ValidProperty,
                    Status = Status.Error,
                    Data = null
                };

            var clients = isAsc ? await _context.Clients
                .Include(address => address.Address)
                .Include(account => account.Accounts)
                .OrderBy(sortBy.GetProperty()).ToListAsync() :
               await _context.Clients
                .Include(address => address.Address)
                .Include(account => account.Accounts)
                .OrderByDescending(sortBy.GetProperty()).ToListAsync();

            return new Response<ICollection<Client>>()
            {
                Code = HttpStatusCode.OK,
                Message = clients.Count() == 0 ? SuccessMessage.NoClientFound : SuccessMessage.RetreiveClients,
                Status = Status.Success,
                Data = clients.Count() == 0 ? null : clients
            };
        }
        public async Task<Response> UpdateClient(Guid id, UpdateClientRequest updateClient)
        {
            var client = _context.Clients
                .Include(address => address.Address)
                .Include(account => account.Accounts)
                .FirstOrDefault(dbc => dbc.Id == id);
            if (client == null)
                return new Response()
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = Status.Error,
                    Message = ErrorMessage.UserNotFound
                };

            if (updateClient.Accounts != null && updateClient.Accounts.Count() > 0)
            {
                foreach (var account in updateClient.Accounts)
                {
                    if (account.Id == Guid.Empty)
                        return new Response()
                        {
                            Code = HttpStatusCode.BadRequest,
                            Status = Status.Error,
                            Message = ErrorMessage.CheckAccountId
                        };
                }
            }


            var checkEmailExsit = _context.Clients
                .FirstOrDefault(dbc => dbc.Email == updateClient.Email &&
                dbc.Id != id);

            if (checkEmailExsit != null)
                return new Response()
                {
                    Code = HttpStatusCode.BadRequest,
                    Status = Status.Error,
                    Message = ErrorMessage.EmailExsit
                };

            client = ModelMapper.MapUpdateClient(updateClient, client);

            await _context.SaveChangesAsync();

            return new Response()
            {
                Code = HttpStatusCode.OK,
                Status = Status.Success,
                Message = SuccessMessage.Updated
            };

        }
        #endregion
        #region Private
        private async Task<List<Client>> GetPaginatedClients(FilterRequest filter)
        {
            var query = _context.Clients.AsQueryable();

            if (string.IsNullOrEmpty(filter.FilterBy)
                || string.IsNullOrWhiteSpace(filter.FilterBy))
            {
                query = query
                    .Where(dbc => dbc.FirstName.Contains(filter.FilterValue) ||
                    dbc.LastName.Contains(filter.FilterValue) ||
                    dbc.Email.Contains(filter.FilterValue) ||
                    dbc.Sex.Contains(filter.FilterValue) ||
                    dbc.MobileNumber.Contains(filter.FilterValue) ||
                    dbc.Id.ToString().Contains(filter.FilterValue) ||
                    dbc.PersonalId.Contains(filter.FilterValue))
                    .Include(address => address.Address)
                    .Include(account => account.Accounts);
            }
            else
            {
                query = query
                   .WhereContains(filter.FilterBy, filter.FilterValue)
                   .Include(address => address.Address)
                   .Include(account => account.Accounts);
            }

            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize);

            if (filter.PageNumber < 1)
            {
                filter.PageNumber = 1;
            }
            else if (filter.PageNumber > totalPages)
            {

                filter.PageNumber = totalPages == 0 ? 1 : totalPages;
            }

            var clients = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return clients;
        }
        #endregion

    }
}