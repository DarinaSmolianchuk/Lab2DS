using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    class GameSelector
    {
        public Game getStandartGame()
        {
            return new StandartGame();
        }
        public Game getTrainingGame()
        {
            return new TrainingGame();
        }
        public Game getAllInGame()
        {
            return new AllInGame();
        }
    }
}
