using AutoMapper;
using BCPT.ABSTACTION;
using System;

namespace BCPT.ABSTACTION
{
    public class ModelMapper
    {
        public static Client MapInsertClient(AddClientRequest clientRequest)
        {
            var config = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<AddClientRequest, Client>();
                cfg.CreateMap<AccountDto, Account>();
                cfg.CreateMap<AddressDto, Address>();
            });

            var Mapper = config.CreateMapper();
            return Mapper.Map<Client>(clientRequest);
        }
        public static Client MapUpdateClient(UpdateClientRequest clientRequest, Client clientDto)
        {
            var config = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<UpdateClientRequest, Client>()
                .ForMember(dest => dest.Accounts, opt => opt.MapFrom((src, dest) =>
                {
                    var newAccounts = new List<Account>();

                    if (src.Accounts != null && src.Accounts.Count() > 0)
                    {
                        foreach (var updatedAccount in src.Accounts)
                        {
                            var existingAccount = dest.Accounts.FirstOrDefault(account => account.Id == updatedAccount.Id);
                            if (existingAccount != null)
                            {
                                if (updatedAccount.AccountNumber != null)
                                    existingAccount.AccountNumber = updatedAccount.AccountNumber;
                                if (updatedAccount.Balance != 0)
                                    existingAccount.Balance = updatedAccount.Balance;
                            }
                        }
                    }

                    foreach (var account in dest.Accounts)
                    {
                        newAccounts.Add(account);
                    }

                    return newAccounts;
                }))
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null ||
                    (srcMember is string && string.IsNullOrWhiteSpace((string)srcMember)))
                        return false;

                    if (srcMember is System.Collections.ICollection collection && collection.Count == 0)
                        return false;

                    return true;
                }));

                cfg.CreateMap<AccountDto, Account>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null || (srcMember is string && string.IsNullOrWhiteSpace((string)srcMember)))
                        return false;

                    return true;
                }));

                cfg.CreateMap<AddressDto, Address>()
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                {
                    if (srcMember == null || (srcMember is string && string.IsNullOrWhiteSpace((string)srcMember)))
                        return false;

                    return true;
                }));
            });

            var Mapper = config.CreateMapper();
            return Mapper.Map(clientRequest, clientDto);
        }
        public static History MapAddHistory(AddHistoryRequest historyRequest)
        {
            var config = new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<AddHistoryRequest, History>()
                .ForMember(dest => dest.SearchDate, opt => opt.MapFrom(src => DateTime.Now));
            });

            var Mapper = config.CreateMapper();
            return Mapper.Map<History>(historyRequest);
        }
    }
}