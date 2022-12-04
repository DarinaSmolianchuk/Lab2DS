using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    public abstract class Game
    {
        protected string gameType;
        protected int rating;

        public abstract string getGameType();
        public int getRating() { return rating; }

        //Гра
        public static void play(GameAccount winner, GameAccount loser, Game game)
        {
            //Не можна грати в гру на все, якщо різниця в рейтигу більша ніж в 2 рази
            if (game.getGameType() == "ALL-IN-GAME")
            {
                try
                {
                    if (winner.getCurrentRating() == 0 || loser.getCurrentRating() == 0 ||
                        winner.getCurrentRating() / loser.getCurrentRating() > 2 ||
                        loser.getCurrentRating() / winner.getCurrentRating() > 2)
                        throw new Exception("Can't play All-in game!\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            //В будь-які інші ігри можна грати при будь-яких умовах
            int gameID = GameRecord.gamesCount++;
            //Запис в історію отримують обидва гравця
            winner.winGame(gameID, loser, game);
            loser.loseGame(gameID, winner, game);
        }
    }

    //Стандартна гра. Гра на 26 рейтингу
    public class StandartGame : Game
    {
        public StandartGame()
        {
            rating = 26;
        }
        public override string getGameType()
        {
            return "Standart";
        }
    }

    //Тренувальна гра. Без рейтингу
    public class TrainingGame : Game
    {
        public TrainingGame()
        {
            rating = 0;
        }
        public override string getGameType()
        {
            return "Training";
        }
    }

    //Гра на все. Рейтинг береться з поточних рейтингів гравців, значення -1 символічне
    public class AllInGame : Game
    {
        public AllInGame()
        {
            rating = -1;
        }
        public override string getGameType()
        {
            return "ALL-IN-GAME";
        }
    }
}
