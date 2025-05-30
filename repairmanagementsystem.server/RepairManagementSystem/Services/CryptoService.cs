using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

public class CryptoService : ICryptoService
{
    public (string Hash, string Salt) HashPassword(string password)
    {
        const int saltSize = 16;
        const int keySize = 32;
        const int iterations = 100_000;

        using (var rng = RandomNumberGenerator.Create())
        {
            var salt = new byte[saltSize];
            rng.GetBytes(salt);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256))
            {
                var key = pbkdf2.GetBytes(keySize);
                var hashBytes = new byte[keySize + sizeof(int)];
                Buffer.BlockCopy(key, 0, hashBytes, 0, keySize);
                Buffer.BlockCopy(BitConverter.GetBytes(iterations), 0, hashBytes, keySize, sizeof(int));
                return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(salt));
            }
        }
    }

    public bool VerifyPassword(string password, string hashedPassword, string salt)
    {
        var keySize = 32;
        var hashBytes = Convert.FromBase64String(hashedPassword);
        if (hashBytes.Length != keySize + 4)
        {
            throw new InvalidOperationException("Invalid hash format.");
        }

        var key = new byte[keySize];
        Buffer.BlockCopy(hashBytes, 0, key, 0, key.Length);
        var iterations = BitConverter.ToInt32(hashBytes, key.Length);
        var saltBytes = Convert.FromBase64String(salt);

        using (var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, iterations, HashAlgorithmName.SHA256))
        {
            var computedKey = pbkdf2.GetBytes(key.Length);
            return CryptographicOperations.FixedTimeEquals(computedKey, key);
        }
    }
}
