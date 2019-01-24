using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DomainHunter.BLL
{
    public class DefaultRandomNumberGenerator : IRandomNumberGenerator
    {
        public int GenerateRandomNumber(int maxNumber)
        {
            byte[] randomBytes = new byte[1];
            var rngProvider = new RNGCryptoServiceProvider();
            rngProvider.GetBytes(randomBytes);
            return randomBytes[0] % maxNumber;
        }
    }
}
