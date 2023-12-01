using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pi.Interfaces.Repositories.Admin;
using Pi.Interfaces.Services.Admin;
using Pi.Models.Entities.PI;
using Pi.Models.RequestModels.Admin;
using Pi.Models.RequestModels.Users;
using Pi.Models.ResponseModels.Users;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Services.Admin
{
    public class AdminServices : IAdminServices
    {
        private readonly IAdminRepositories _adminRepositories;
        private readonly IConfiguration _configuration;
        public AdminServices(IAdminRepositories adminRepositories, IConfiguration configuration)
        {
            _adminRepositories = adminRepositories;
            _configuration = configuration;
        }
        public async Task<string> GenToken()
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, _configuration.GetSection("PiSecurities")["AppKey"]),
                new Claim(ClaimTypes.Email, "sample@gmail.com"),
                new Claim(ClaimTypes.GivenName, "sampleGivenName"),
                new Claim(ClaimTypes.Expiration, DateTime.Now.AddMinutes(30).ToString()),


            };
            // Add more claims as needed

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt")["Key"])); // Use the same secret key as in authentication configuration
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("Jwt")["Issuer"],
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Token expiration time
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<bool> RegisterUser(RegisterUserRequest req)
        {
            req.Password = EncryptPasswd(req.Password);
            var result = await _adminRepositories.RegisterUser(req);
            return result;
        }
        public async Task<string> ValidateLogin(LoginRequest req)
        {
            var result = await _adminRepositories.GetCipherPasswd(req);
            result = DecryptPasswd(result);
            var a = req.Password == result;
            return (req.Password == result ? await GenToken() : string.Empty);
        }
        private string EncryptPasswd(string passwd)
        {
            return Encrypt(passwd, _configuration.GetSection("Authorize")["Key"], _configuration.GetSection("Authorize")["Iv"]);
        }
        private string DecryptPasswd(string encryptedPasswd)
        {
            return Decrypt(encryptedPasswd, _configuration.GetSection("Authorize")["Key"], _configuration.GetSection("Authorize")["Iv"]);
        }
        public static string Encrypt(string plainText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256; // Use a 256-bit key (32 bytes).
                aesAlg.BlockSize = 128;
                aesAlg.Mode = CipherMode.CFB;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Ensure the key is the correct length (32 bytes) by padding or truncating it.
                byte[] keyBytes = Encoding.UTF8.GetBytes(key); // 256 bits key size
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

                byte[] originalKeyBytes = Encoding.UTF8.GetBytes(key);
                Array.Copy(originalKeyBytes, keyBytes, Math.Min(originalKeyBytes.Length, keyBytes.Length));

                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string key, string iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256; // Use a 256-bit key (32 bytes).
                aesAlg.BlockSize = 128;
                aesAlg.Mode = CipherMode.CFB;
                aesAlg.Padding = PaddingMode.PKCS7;

                // Ensure the key is the correct length (32 bytes) by padding or truncating it.
                byte[] keyBytes = Encoding.UTF8.GetBytes(key); // 256 bits key size
                byte[] ivBytes = Encoding.UTF8.GetBytes(iv);

                byte[] originalKeyBytes = Encoding.UTF8.GetBytes(key);
                Array.Copy(originalKeyBytes, keyBytes, Math.Min(originalKeyBytes.Length, keyBytes.Length));

                aesAlg.Key = keyBytes;
                aesAlg.IV = ivBytes;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
