using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace CGI
{
    public class Suspects
    {
        public static string thief;
        public static string trophySpot;
        private static int spotIndex;
        private static int thiefIndex;
        private static string[] notSpot;
        private static string[] notThieves;
        private static string[] hidingSpots = { "Lakeside Dining", "Gorgas Library", "The Student Center", "Hewson Hall", "Denny Chimes", "Lloyd Hall" };
        private static string[] suspectList = {"Aubie", "Smokey", "Mike", "Uga", "Bevo", "Tony"};
        public Suspects()
        {
        }
        public static string GetThief(){
            return thief;
        }
        public static string GetTrophySpot(){
            return trophySpot;
        }
        public static string[] GetSuspectList(){
            return suspectList;
        }
        public static string[] GetHidingSpots(){
            return hidingSpots;
        }
        public static void AssignThief(){
            Random rando = new Random();
            thief = suspectList[rando.Next(suspectList.Length)];//chooses my thief each game
            MakeNotThief();
        }
        public static void MakeNotThief(){
            int thiefCount = 0;
            notThieves = new string[suspectList.Length -1]; //new array - the thief
            for(int i = 0; i < suspectList.Length; i++){
                if(suspectList[i] != thief){
                    notThieves[thiefCount] = suspectList[i];
                    thiefCount++;
                }
            }
            Random shuffle = new Random(); //this will give replayability since the order won't always be the same
            for(int i = 0; i < notThieves.Length; i++){
                int randomIndex = shuffle.Next(notThieves.Length); //this will return a spot in the array between 0 and the length
                string temp = notThieves[i];//using the swap method here 
                notThieves[i] = notThieves[randomIndex];
                notThieves[randomIndex] = temp;
            }
            thiefIndex = 0;
        }
        public static string ReturnNotThief(){
            if(thiefIndex >= notThieves.Length){
                System.Console.WriteLine("You have earned all possible clues"); //error check
            }
            string nextClue = notThieves[thiefIndex];
            thiefIndex++; //make sure index moves up
            return nextClue;
        }
        //all this is identical as for thief bc we want the same thing to occur
        public static void AssignTrophySpot(){
            Random hiding = new Random();
            trophySpot = hidingSpots[hiding.Next(hidingSpots.Length)];
            MakeNotTrophySpot();
        }
        public static void MakeNotTrophySpot(){
            int spotCount = 0;
            notSpot = new string[hidingSpots.Length -1];
            for(int i = 0; i < hidingSpots.Length; i++){
                if(hidingSpots[i] != trophySpot){
                    notSpot[spotCount] = hidingSpots[i];
                    spotCount++;
                }
            }
            Random shuffle = new Random(); 
            for(int i = 0; i < notSpot.Length; i++){
                int randomIndex = shuffle.Next(notSpot.Length); 
                string temp = notSpot[i];
                notSpot[i] = notSpot[randomIndex];
                notSpot[randomIndex] = temp;
            }
            spotIndex = 0;
        }
        public static string ReturnNotTrophySpot(){
            string nextClue = notSpot[spotIndex];
            spotIndex++;
            return nextClue;
        }
        public static void ResetGame(){ //this avoids any issues with initialization
            spotIndex = 0;
            thiefIndex = 0;
        }
    }
}