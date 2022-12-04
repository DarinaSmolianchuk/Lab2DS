using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2DS
{
    class Program
    {
        static void Main(string[] args)
        {
            GameAccount player1 = new GameAccount("FIRST");
            PremiumAccount player2 = new PremiumAccount("SECOND");
            WinStreakerAccount player3 = new WinStreakerAccount("THIRD");
            GameSelector selector = new GameSelector();

            //Спроба грати на все у гравців, де у одного з них рейтинг == 0
            Game.play(player1, player2, selector.getAllInGame());

            //Тестування гри преміумного гравця
            Console.WriteLine("\t\t\t  Premium test: player2");
            Game.play(player1, player2, selector.getStandartGame());
            Game.play(player2, player1, selector.getStandartGame());
            Game.play(player2, player1, selector.getStandartGame());
            Game.play(player2, player1, selector.getStandartGame());
            player1.getStats();
            player2.getStats();

            //Тестування гри любителя вінстріків
            Console.WriteLine("\t\t\t  Winstreak test: player3");
            Game.play(player3, player1, selector.getStandartGame());
            Game.play(player3, player1, selector.getStandartGame());
            Game.play(player3, player1, selector.getStandartGame());
            Game.play(player3, player1, selector.getStandartGame());
            Game.play(player3, player1, selector.getStandartGame());
            player3.getStats();
            player1.getStats();

            //Тренувальна гра
            Console.WriteLine("\t\t\t  Training test");
            Game.play(player3, player2, selector.getTrainingGame());
            Game.play(player3, player2, selector.getTrainingGame());
            player3.getStats();
            player2.getStats();

            //Гра на все
            Console.WriteLine("\t\t\t  All-in test");
            Game.play(player2, player3, selector.getAllInGame());
            player3.getStats();
            player2.getStats();

        }
    }
}