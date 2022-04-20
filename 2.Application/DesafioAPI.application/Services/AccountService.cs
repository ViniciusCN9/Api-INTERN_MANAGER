using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioAPI.application.DataTransferObjects;
using DesafioAPI.application.Interfaces;
using DesafioAPI.application.Mappers;
using DesafioAPI.domain.Entities;
using DesafioAPI.domain.Repositories;

namespace DesafioAPI.application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<Account> GetAccounts()
        {
            var accounts = _accountRepository.GetAccounts();
            if (accounts is null)
                throw new ArgumentException("Nenhum conta encontrada");

            return accounts;
        }

        public Account GetByIdAccount(int id)
        {
            var account = _accountRepository.GetByIdAccount(id);
            if (account is null)
                throw new ArgumentException("Conta não encontrada");

            return account;
        }

        public Account PostLogin(string username, string password)
        {
            var account = _accountRepository.PostLogin(username, password);
            if (account is null)
                throw new ArgumentException("Conta não encontrada");

            return account;
        }

        public void PostRegister(AccountRegisterDto accountRegisterDto)
        {
            _accountRepository.PostRegister(accountRegisterDto.ToDomain());
        }

        public void PatchByIdAccount(AccountDto accountDto, int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");
                
            var account = _accountRepository.GetByIdAccount(id);
            if (account is null)
                throw new ArgumentException("Conta não encontrada");

            account.Username = accountDto.Username ?? account.Username;
            account.Password = accountDto.Password ?? account.Password;
            account.IsActive = accountDto.IsActive ?? account.IsActive;

            _accountRepository.UpdateAccount(account);
        }

        public void PutByIdAccount(AccountDto accountDto, int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");

            var account = _accountRepository.GetByIdAccount(id);
            if (account is null)
                throw new ArgumentException("Conta não encontrada");

            account.Username = accountDto.Username;
            account.Password = accountDto.Password;
            account.IsActive = (bool)accountDto.IsActive;

            _accountRepository.UpdateAccount(account);
        }

        public void DeleteByIdAccount(int id)
        {
            if (id < 0)
                throw new ArgumentException("Id inválido");
                
            var account = _accountRepository.GetByIdAccount(id);
            if (account is null)
                throw new ArgumentException("Conta não encontrada");

            _accountRepository.DeleteAccount(account);
        }
    }
}