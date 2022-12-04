using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    public class GameRecord
    {
        //Змінні гри
        public static int gamesCount = 0;
        private int ID;
        private GameAccount firstPlayer;
        private GameAccount secondPlayer;
        private string gameType;
        private int rating;
        bool isFirstWin;

        //Конструктор
        public GameRecord(int ID, GameAccount firstPlayer, GameAccount secondPlayer, string gameType, int rating, bool isFirstWin)
        {
            this.ID = ID;
            this.firstPlayer = firstPlayer;
            this.secondPlayer = secondPlayer;
            this.gameType = gameType;
            this.rating = rating;
            this.isFirstWin = isFirstWin;
        }

        //Геттери
        public int getID() { return ID; }
        public GameAccount getFirstPlayer() { return firstPlayer; }
        public GameAccount getSecondPlayer() { return secondPlayer; }
        public string getGameType() { return gameType; }
        public int getRating() { return rating; }
        public bool isFirstPlayerWin() { return isFirstWin; }
    }
}
