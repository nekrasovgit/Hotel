﻿using Authorization.Dal;
using Authorization.UserRepository;
using Elskom.Generic.Libs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Authorization.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<string> Register(RegisterRequest model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email))
                    return null;
                var user = (await _userRepository.GetAllAsync(u => u.Email.ToUpper().Equals(model.Email.ToUpper()))).FirstOrDefault();
                if (user != null) return null;
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Password = Encrypt(model.Password),
                    Email = model.Email,
                    Roles = Roles.User,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth
                };

                await _userRepository.AddUserAsync(newUser);
                var token = GenerateJwtToken(newUser);
                return token;
            }
            catch
            {
                throw;
            }
        }
        public string RegisterAdmin(RegisterRequest model)
        {
            try
            {

                var newUser = new User()
                {
                    Id = Guid.NewGuid(),
                    Password = Encrypt(model.Password),
                    Email = model.Email,
                    Roles = Roles.Admin,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth
                };
                _userRepository.AddUserAsync(newUser);
                var token = GenerateJwtToken(newUser);
                return token;
            }
            catch
            {
                throw;
            }
        }
        public async Task<string> Authenticate(AuthenticateRequest model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.Email))
                    return null;
                var user = (await _userRepository.GetAllAsync(u => u.Email.ToUpper().Equals(model.Email.ToUpper()))).FirstOrDefault();
                if (user == null) return null;
                var verified = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                if (!verified) return null;
                var token = GenerateJwtToken(user);
                return token;
            }
            catch
            {
                throw;
            }

        }
        private string GenerateJwtToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, userInfo.FirstName),
            new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string Encrypt(string password)
        {

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            string hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return hash;
        }

        public IEnumerable<User> GetUsers()
        {
            var getUsers = _userRepository.GetAllUsers();
            return getUsers;
        }
    }
}