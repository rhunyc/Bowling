using Bowling_Console_App.Game;
using System;

namespace Bowling_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            // Since we're only doing 1 round, a game is a round
            Round game = new Round();

            game.PlayRound(); //Start our game
        }
    }
}
