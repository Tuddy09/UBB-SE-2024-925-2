using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGames.Controller
{
    public class GameOfLifeController
    {
        public List<int> GenerateRandomIndices(int numberOfIndices, int numberOfTiles)
        {
            if (numberOfIndices > numberOfTiles)
            {
                throw new ArgumentOutOfRangeException("The number of randomly generated indices cannot be larger than the total number of tiles.");
            }
            if (numberOfIndices < 0)
            {
                throw new ArgumentOutOfRangeException("The number of randomly generated indices cannot be smaller than 0");
            }
            var lowerBound = 1;
            var upperBound = numberOfTiles + 1;
            List<int> randomNumbersList = new List<int>();
            Random random = new Random();

            while (numberOfIndices > 0)
            {
                var randomNumber = random.Next(lowerBound, upperBound);

                if (!randomNumbersList.Contains(randomNumber))
                {
                    randomNumbersList.Add(randomNumber);
                    numberOfIndices--;
                }
            }
            return randomNumbersList;
        }
    }
}
