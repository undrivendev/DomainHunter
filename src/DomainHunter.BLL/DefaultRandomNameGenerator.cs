using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL
{
    public class DefaultRandomNameGenerator : IRandomNameGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public DefaultRandomNameGenerator(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string GenerateName(int length)
        {
            var sb = new StringBuilder(GetRandomConsonant());
            for (int i = 0; i < length; i++)
            {
                if (i % 2 == 0)
                {
                    sb.Append(GetRandomVowel());
                }
                else
                {
                    sb.Append(GetRandomConsonant());
                }
            }
            return sb.ToString();
        }

        private char GetRandomVowel()
        {
            string letterList = "aeiou";
            return GetRandomLetter(letterList);
        }

        private char GetRandomConsonant()
        {
            string letterList = "bcdfghjklmnpqrstvwxyz";
            return GetRandomLetter(letterList);
        }

        private char GetRandomLetter(string letterList)
            => letterList[_randomNumberGenerator.GenerateRandomNumber(letterList.Length - 1)];
        

       
    }
}
