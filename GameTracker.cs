using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace CGI
{
    public class GameTracker
    {
        private static bool[] gameCompleted;
        private static string[] clues;
        public GameTracker(int numberOfGames){
            gameCompleted = new bool[numberOfGames];
            clues = new string[numberOfGames];
        }
        public static bool GetGameCompleted(int index){
            if(index <0 || index >= gameCompleted.Length){
                System.Console.WriteLine("Invalid game index");
                return false;
            }
            return gameCompleted[index];
        }
        public static string GetClues(int index){
            if(index <0 || index >= clues.Length){
                System.Console.WriteLine("Invalid game index");
                return null;
            }
            return clues[index];
        }
        public static void SetGameResults(int index, bool completed, string clue1, string clue2){
            if(index < 0 || index >= gameCompleted.Length){
                System.Console.WriteLine("Invalid game index");
                return;
            }
            clues[index] = $"Clue 1: {clue1}, Clue 2: {clue2}";;
            gameCompleted[index] = completed;
        }
    }
}