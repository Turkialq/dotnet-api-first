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
        public async Task<ServiceRespinse<int>> Login(string userName, string password)
        {
            var serviceResponse = new ServiceRespinse<int>();
            try
            {
                var user = await _context.users
                .FirstOrDefaultAsync(u => u.userName.ToLower().Equals(userName.ToLower()));

                if (user is null)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User does not exsits";
                }
                else if (!VerifiyPasswordHash(password, user.password, user.passwordSalt))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "Wrong password";

                }
                else
                {
                    serviceResponse.Data = user.id;
                }
                return serviceResponse;

            }
            catch (System.Exception)
            {

                throw new Exception("Error happened in Login");
            }
        }

        public async Task<ServiceRespinse<int>> Register(User user, string password)
        {
            var serviceResponse = new ServiceRespinse<int>();
            var Newuser = _mapper.Map<User>(user);
            try
            {
                if (await UserExists(user.userName))
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "User already exists";
                    return serviceResponse;

                }
                else
                {
                    createPasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                    user.password = passwordHash;
                    user.passwordSalt = passwordSalt;

                    await _context.users.AddAsync(user);
                    await _context.SaveChangesAsync();
                    serviceResponse.Data = user.id;
                    return serviceResponse;
                }
            }
            catch (System.Exception)
            {
                throw new Exception("Registration Error");
            }
        }

        public async Task<bool> UserExists(string userName)
        {
            try
            {
                var user = await _context.users.AnyAsync(u => u.userName.ToLower() == userName.ToLower());

                if (user)
                {
                    return true;
                }
                return false;

            }
            catch (System.Exception)
            {

                throw new Exception("something went wrong with user exists function");
            }
        }
        private void createPasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }

        }

        private bool VerifiyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computeHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computeHash.SequenceEqual(passwordHash);

            }
        }
    }

}