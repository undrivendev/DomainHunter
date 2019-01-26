using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL
{
    public class RandomNameGenerator : IRandomNameGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public RandomNameGenerator(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string GenerateName(int length)
        {
            var sb = new StringBuilder(GetRandomConsonant());
            for (int i = 0; i <= length; i++)
            {
                var charTypeSelector = _randomNumberGenerator.GenerateRandomNumber(1);
                if (charTypeSelector == 0)
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
