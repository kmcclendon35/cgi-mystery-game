using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Colorful;
using Figlet = Colorful.Figlet;

namespace CGI
{
    public class Utility
    {
        public static void ShowTitle(){
            System.Console.Clear();
            FigletFont font = FigletFont.Load("standard.flf");//puts the title in a font and i made it red
            Figlet figlet = new Figlet(font);
            Colorful.Console.WriteLine(figlet.ToAscii("Trophy Mystery Game"), Color.Crimson);
        }
        //adds colors based on difficulty of game
        public static void WriteMenu(){
            System.Console.Write("Choose from the following:\n1. See suspect list\n2. Talk to ");
            Colorful.Console.Write("Jeff Lucas", Color.Yellow);
            System.Console.Write(" at Hewson Hall\n3. Talk to ");
            Colorful.Console.Write("President Bell", Color.Green);
            System.Console.Write(" at the President's House\n4. Talk to ");
            Colorful.Console.Write("Coach Deboer", Color.Red);
            System.Console.Write(" at Bryant-Denny Stadium\n5. Talk to the ");
            Colorful.Console.Write("quad squirrel", Color.Red);
            System.Console.Write(" on the quad\n6. Talk to ");
            Colorful.Console.Write("Big Al", Color.Green);
            System.Console.Write(" at Coleman Coliseum\n7. Submit your guess\n8. Exit\n");
        }
        public static void SetUp(){
            Pause();
            LetterByLetter("It was a dark and stormy night...", 100);
            // Lightning();
            Thread.Sleep(500);
            LetterByLetter("You hear a loud KNOCK KNOCK on your door...", 100);
            //Lightning();
            Thread.Sleep(500);
            LetterByLetter("You open your door and find Nick Saban on the other side. Coach Saban informs you that the 2020 National Championship trophy has been stolen and only YOU can  help find it.", 60);
            Thread.Sleep(500);
            LetterByLetter("The rules are simple, you will talk to various people around campus and if you can complete their challenge, they will give you a clue on who stole the trophy and where it is hidden. You only get one guess so make it count and bring back the trophy. (P.S. If you want to view a clue for a completed game just pick the game again)", 60);
            Pause();
        }
        public static void LetterByLetter(string text, int delay = 100){
            for(int i = 0; i < text.Length; i++){
                System.Console.Write(text[i]);
                Thread.Sleep(delay);
            }
            System.Console.WriteLine();
        }
        public static void Pause(){
            System.Console.WriteLine("Press any key to continue...");
            System.Console.ReadKey();
            System.Console.Clear();
        }  
        public static bool WantToPlay(){
            System.Console.WriteLine("Would you like to play?(yes/no)");
            while(true){
                    string userChoice = System.Console.ReadLine().ToLower();
                    if(userChoice == "yes"){
                        return true;
                    }else if(userChoice == "no"){
                        System.Console.WriteLine("Leaving the game...");
                        return false;
                    }
                    System.Console.WriteLine("Invalid input, please enter yes or no.");
                    
            }
        }
        public static void PlayGame(int index, bool gameResult){
        if(gameResult)
        {
            string clue1 = Suspects.ReturnNotThief(); // First clue
            string clue2 = Suspects.ReturnNotTrophySpot(); // Second clue
            GameTracker.SetGameResults(index, true, clue1, clue2);
            Colorful.Console.WriteLine($"The thief is not {clue1} and the trophy is not hidden in {clue2}", Color.Crimson);
        }
        else{
            System.Console.WriteLine("You lost this game. No clues will be given.");
            GameTracker.SetGameResults(index, false, null, null); // Mark game as completed but no clues
        }
        }
    }
}