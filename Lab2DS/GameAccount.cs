using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    public class GameAccount
    {
        //Ім'я
        protected string userName;
        //Рейтинг. Стандартно -- нульовий
        protected int currentRating = 0;
        //Кількість зіграних ігр. Стандартно -- нуль
        protected int gamesCount = 0;
        //Список історії зіграних ігр
        protected List<GameRecord> history = new List<GameRecord>();
        //Тип аккаунту
        protected string accountType = "Standart";

        //Геттери/Сеттери
        public string getUserName() { return userName; }
        public int getCurrentRating() { return currentRating; }
        public int getGamesCount() { return gamesCount; }
        public List<GameRecord> getHistory() { return history; }
        public string getAccountType() { return accountType; }

        //Рейтинг не може бути менше 0
        //Якщо отримане значення менше 0, то встановлюємо 0
        public void setCurrentRating(int value)
        {
            if (value < 0)
                currentRating = 0;
            else
                currentRating = value;
        }

        //Конструктор користувача
        public GameAccount(string userName)
        {
            this.userName = userName;
        }

        //Виграш
        public virtual void winGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.getGameType() == "ALL-IN-GAME")
            {
                //Зміна рейтингу і кількості ігор
                setCurrentRating(currentRating + opponent.currentRating);
                gamesCount++;

                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), opponent.currentRating, true));
                return;
            }

            //В інших випадках логіка однакова
            //Зміна рейтингу і кількості ігор
            setCurrentRating(currentRating + game.getRating());
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), game.getRating(), true));
        }

        //Програш
        public virtual void loseGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.getGameType() == "ALL-IN-GAME")
            {
                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), -currentRating, false));

                //Зміна рейтингу і кількості ігор
                setCurrentRating(0);
                gamesCount++;

                return;
            }

            
            //Якщо гра стандартна чи тренувальна, то логіка на оба варіанти одна
            //Зміна рейтингу і кількості ігор
            setCurrentRating(currentRating - game.getRating());
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), -game.getRating(), false));
        }

        //Виведення інфо про ігри
        public void getStats()
        {
            //Якщо не грав -- виводимо відповідне повідомлення
            if (history == null)
            {
                Console.WriteLine("Player " + userName + " not played yet!");
                return;
            }

            //Отримуємо дані щодо ігр
            Console.WriteLine("Player: " + userName + ".\nRating: " + currentRating + ".\n" +
                              "Account type: " + accountType + "\nPlayed " + gamesCount + " games:");

            Console.WriteLine("\tID   \tOpponent   \tGame type   \tR. change   \tResult");
            foreach (GameRecord game in history)
            {
                //Виводимо тип гри, з ким, який рейтинг гравець отримав/втратив (рейтинг не стає нижче 0)
                Console.Write("\t" + game.getID() + "\t" + game.getSecondPlayer().userName + "\t\t" + game.getGameType() + "\t" + game.getRating());
                if (game.isFirstPlayerWin())
                    Console.WriteLine("\t\tWin");
                else
                    Console.WriteLine("\t\tLose");
            }
            Console.WriteLine();
        }
    }
    
    //Преміум акаунт. На 50% більша нагорода за перемогу, на 30% менші втрати при програші
    public class PremiumAccount : GameAccount
    {
        //Початковий рейтинг -- одразу 130. Люблю 13
        public PremiumAccount(string userName) : base(userName)
        {
            currentRating = 130;
            accountType = "Premium";
        }

        //Виграш
        public override void winGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.getGameType() == "ALL-IN-GAME")
            {
                //Зміна рейтингу і кількості ігор з коефіцієнтом преміуму
                setCurrentRating(currentRating + (int)(opponent.getCurrentRating() * 1.5));
                gamesCount++;

                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), (int)(opponent.getCurrentRating() * 1.5), true));
                return;
            }

            //В інших випадках логіка однакова
            //Зміна рейтингу і кількості ігор з врахуванням преміуму
            setCurrentRating(currentRating + (int)(game.getRating() * 1.5));
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), (int)(game.getRating() * 1.5), true));
        }

        //Програш
        public override void loseGame(int gameID, GameAccount opponent, Game game)
        {
            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            //Преміумні гравці також втрачають весь рейтинг при грі на все. Бо я так хочу
            if (game.getGameType() == "ALL-IN-GAME")
            {
                //Запис в історію
                history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), -currentRating, false));

                //Зміна рейтингу і кількості ігор
                setCurrentRating(0);
                gamesCount++;

                return;
            }

            //Якщо гра стандартна чи тренувальна, то логіка на оба варіанти одна
            //Зміна рейтингу і кількості ігор з урахування преміуму
            setCurrentRating(currentRating - (int)(game.getRating() * 0.7));
            gamesCount++;

            //Запис в історію
            history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), -(int)(game.getRating() * 0.7), false));
        }
    }

    //Акаунт любителя вінстріків. За кожну перемогу підряд нагорода збільшується на 10%
    public class WinStreakerAccount : GameAccount
    {
        //Початковий рейтинг -- стандартний, 0
        public WinStreakerAccount(string userName) : base(userName)
        {
            accountType = "WinStreaker";
        }

        //Виграш
        public override void winGame(int gameID, GameAccount opponent, Game game)
        {
            //Рахуємо вінстрік (кількість перемог підряд з кінця)
            int winStreak = 0;
            for (int i = history.Count - 1; i >= 0; i--)
            {
                if (history[i].getFirstPlayer().getUserName() == userName && history[i].isFirstPlayerWin() ||
                    history[i].getSecondPlayer().getUserName() == userName && !history[i].isFirstPlayerWin()) 
                {
                    winStreak++;
                }
                else
                {
                    break;
                }
            }

            //Якщо гра на весь рейтинг, то буде своя логіка для нарахування рейтингу
            if (game.getGameType() == "ALL-IN-GAME")
            {
                //Зміна рейтингу і кількості ігор
                setCurrentRating(currentRating + (int)(opponent.getCurrentRating() * (1 + 0.1 * winStreak)));
                gamesCount++;

                //Запис в історію + обрахунок рейтингу в залежності від вінстріку
                history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), (int)(opponent.getCurrentRating() * (1 + 0.1 * winStreak)), true));
                return;
            }

            //Для інших видів ігр
            //Зміна рейтингу і кількості ігор
            setCurrentRating(currentRating + (int)(game.getRating() * (1 + 0.1 * winStreak)));
            gamesCount++;

            //Запис в історію + обрахунок рейтингу в залежності від вінстірку
            history.Add(new GameRecord(gameID, this, opponent, game.getGameType(), (int)(game.getRating() * (1 + 0.1 * winStreak)), true));
        }

        //Програш. Тут вінстрік ніяк не впливає, тому не перевантажую метод
        //
    }
}