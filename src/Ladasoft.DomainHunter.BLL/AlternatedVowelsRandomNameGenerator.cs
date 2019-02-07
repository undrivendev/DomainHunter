using System;
using System.Collections.Generic;
using System.Text;

namespace DomainHunter.BLL
{
    public class AlternatedVowelsRandomNameGenerator : IRandomNameGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public AlternatedVowelsRandomNameGenerator(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string GenerateName(int length)
        {
            return $"{GetRandomStarting()}{GetRandomVowel()}{GetRandomVersatile()}{GetRandomConsonant()}{GetRandomVowel()}";
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

        private char GetRandomVersatile()
        {
            string letterList = "lrn";
            return GetRandomLetter(letterList);
        }
        
        private char GetRandomStarting()
        {
            string letterList = "dpmnvbc";
            return GetRandomLetter(letterList);
        }


        private char GetRandomLetter(string letterList)
            => letterList[_randomNumberGenerator.GenerateRandomNumber(letterList.Length - 1)];

    }
}
