using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dotnet_api_first.models;
using Microsoft.EntityFrameworkCore;
namespace dotnet_api_first.Data
{
    public class AuthRepo : IAuthRepo

    {
        private readonly DataContex _context;
        private readonly IMapper _mapper;

        public AuthRepo(DataContex context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public Task<ServiceRespinse<int>> Login(string userName, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceRespinse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceRespinse<int>();
            var Newuser = _mapper.Map<User>(user);
            try
            {
                createPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.password = passwordHash;
                user.passwordSalt = passwordSalt;

                await _context.users.AddAsync(user);
                await _context.SaveChangesAsync();
                serviceResponse.Data = user.id;
                return serviceResponse;
            }
            catch (System.Exception)
            {
                throw new Exception("Registration Error");
            }
        }

        public Task<bool> UserExists(string userName)
        {
            throw new NotImplementedException();
        }
        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }
    }

}