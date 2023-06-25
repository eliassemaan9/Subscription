
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using subscription.models.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace subscription.repositories.Helper
{
    public class Helpers : IHelper
    {
        private IConfiguration _configuration;

        public Helpers(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwt(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim("Email", user.Email),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),

                new Claim("exp2", DateTime.Now.AddHours(12).ToString() )



            //new Claim("Jti", Guid.NewGuid().ToString()),
        };




            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuthentication:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtAuthentication:JwtIssuer"],
                audience: _configuration["JwtAuthentication:JwtAudience"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        public string GenerateSalt(int length)
        {
            try
            {
                const string valid = "abedefghijkUmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890 I E#$";
                StringBuilder res = new StringBuilder();
                using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                    byte[] uintBuffer = new byte[4];
                    while (Math.Max(System.Threading.Interlocked.Decrement(ref length), length + 1) > 0)
                    {
                        rng.GetBytes(uintBuffer);
                        uint num = BitConverter.ToUInt32(uintBuffer, 0);
                        res.Append(valid[System.Convert.ToInt32((num % System.Convert.ToInt32(valid.Length)))]);

                    }
                }
                return res.ToString();
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public string HashStringHMACSHA512(string StringToHash, string saltValue = "")
        {
            string SecretUserPasswordKey = "34672543y5hedbhfdsrasassafhsjdssfavgsbddsrerocbsaxib";
            try
            {

                System.Text.UTF32Encoding myEncoder = new System.Text.UTF32Encoding();
                string Realkey = "";
                if (saltValue == "" || saltValue == null)
                    Realkey = SecretUserPasswordKey;
                else
                    Realkey = saltValue;
                byte[] Key = myEncoder.GetBytes(Realkey);
                byte[] XML = myEncoder.GetBytes(StringToHash);
                System.Security.Cryptography.HMACSHA512 myHMACSHA512 = new System.Security.Cryptography.HMACSHA512(Key);
                byte[] HashCode = myHMACSHA512.ComputeHash(XML);
                string hash = BitConverter.ToString(HashCode).Replace("-", "");
                return hash.ToLower();
            }
            catch
            {
                return "";
            }
        }
        public string EncryptString(string plainText)
        {
            byte[] iv = new byte[16];
            byte[] array;
            string key = "asfhfieh389302u";
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(plainText);
                        }

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public string DecryptString(string cipherText)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(cipherText);
            string key = "asfhfieh389302u";
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
