namespace BoardGames.Model.GameOfLife
{
    public class Spinner
    {
        public static readonly IDictionary<int, int> ResultProbabilitiesAsPercentages = new Dictionary<int, int>()
        {
            [1] = 19,
            [2] = 17,
            [3] = 14,
            [4] = 11,
            [5] = 9,
            [6] = 8,
            [7] = 7,
            [8] = 6,
            [9] = 5,
            [10] = 4
        };
        private Random randomizer;

        public Spinner()
        {
            randomizer = new Random();
        }

        public int RollSpinner()
        {
            int randomPercentage = randomizer.Next(1, 100);

            int valueToBeReturned = 1, probabilitySumUntilValue = ResultProbabilitiesAsPercentages[1];
            while (probabilitySumUntilValue < randomPercentage)
            {
                valueToBeReturned++;
                probabilitySumUntilValue += ResultProbabilitiesAsPercentages[valueToBeReturned];
            }
            return valueToBeReturned;
        }

        public Random GetRandomizer()
        {
            return randomizer;
        }
    }
}
