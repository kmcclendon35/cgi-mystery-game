using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CGI
{
    public class Menu
    {
        private Utility utility;
        private GameTracker tracker;    
        private CharacterGames characterGames;
        public Menu(){
            utility = new Utility();
            tracker = new GameTracker(5); // Adjust the constructor as needed
            characterGames = new CharacterGames(); 
        }
        public void ShowMenu(){
            GameIntro();
            Suspects.AssignThief();
            Suspects.AssignTrophySpot();
            string userChoice = "";
            while(userChoice != "8" && userChoice != "7"){
                Utility.WriteMenu();
                userChoice = Console.ReadLine();
                Console.Clear();
                switch(userChoice){
                    case "1":
                        GuessOptions();
                        break;
                    case "2":
                        if(!GameTracker.GetGameCompleted(0)){
                            bool gameResult = Jeff();
                            Utility.PlayGame(0, gameResult);
                            Utility.Pause();
                        } else{
                            System.Console.WriteLine($"You already played this game. Your clues were {GameTracker.GetClues(0)}");
                        }
                        break;
                    case "3":
                        if(!GameTracker.GetGameCompleted(1)){
                            bool gameResult = Bell();
                            Utility.PlayGame(1, gameResult);
                            Utility.Pause();
                        } else{
                            System.Console.WriteLine($"You already played this game. Your clues were {GameTracker.GetClues(1)}");
                        }
                        break;
                    case "4":
                        if(!GameTracker.GetGameCompleted(2)){
                            bool gameResult = Deboer();
                            Utility.PlayGame(2, gameResult);
                            Utility.Pause();
                        } else{
                            System.Console.WriteLine($"You already played this game. Your clue was: {GameTracker.GetClues(2)}");
                        }
                        break;
                    case "5":
                        if(!GameTracker.GetGameCompleted(3)){
                            bool gameResult = Squirrel();
                            Utility.PlayGame(3, gameResult);
                            Utility.Pause();
                        } else{
                            System.Console.WriteLine($"You already played this game. Your clue was: {GameTracker.GetClues(3)}");
                        }
                        break;
                    case "6":
                        if(!GameTracker.GetGameCompleted(4)){
                            bool gameResult = BigAl();
                            Utility.PlayGame(4, gameResult);
                            Utility.Pause();
                        } else{
                            System.Console.WriteLine($"You already played this game. Your clue was: {GameTracker.GetClues(4)}");
                        }
                        break;
                    case "7":
                        MakeGuess(userChoice);
                        break;
                    case "8":
                        ProgramExit();
                        break;
                    default:
                        System.Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        public void GameIntro(){ 
            Utility.ShowTitle();
            Utility.SetUp();
        }
        public void GuessOptions(){
            System.Console.WriteLine("To win the game you must guess the correct suspect and where they hid it. The following people are your suspects:\n1. Aubie\n2. Smokey\n3. Mike\n4. Uga\n5. Bevo\n6. Tony");
            System.Console.WriteLine("The thief hid the trophy in one of the following locations:\n1. Lakeside Dining\n2. Gorgas Library\n3. The Student Center\n4. The 3rd floor of Hewson\n5. Denny Chimes\n6. Lloyd Hall");
            Utility.Pause();
        }
        public bool Jeff(){
            System.Console.WriteLine("You visit Jeff Lucas in his office on the third floor of Hewson and sit down. He tells you he would love to help you catch the thief, but you have to prove that you can think and speak. To do that, he says he will test you with his signature game.");
            while(Utility.WantToPlay()){
                bool won = CharacterGames.RunNumbersGame();
                if(won){
                    return true;
                }else{
                    System.Console.WriteLine("You ran out of tries! Jeff is extremely disappointed in you and now your loved ones are in danger...");
                    return false;
                }
            }
            System.Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        public bool Bell(){
            System.Console.WriteLine("With the sounds of Denny Chimes tolling behind you, you approach the president's mansion. Dr. Bell invites you inside and you explain the situation. He is happy to help you, but only if you can prove you are a true Alabama fan.");
            while(Utility.WantToPlay()){
                bool result = characterGames.YeaAlabama();
                if(result){
                    System.Console.WriteLine("You know our fight song by heart! You have earned a clue!");
                    return true;
                    
                }
                else{
                    System.Console.WriteLine("Not only did you fail the true test of a loyal Alabama fan, but you also don't get a clue.");
                    return false;
                }
            }
            System.Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        public bool Deboer(){
            System.Console.WriteLine("You walk onto the field of Bryant-Denny Stadium and approach Coach Deboer to ask for a clue. He tells you that you don't become the football coach of the greatest football school in the country by just getting hand-outs. If you want his help, you are going to have to prove your worth and make a field goal. The  rules are simple, you have to get your accuracy and power right to make it. You can try as many times as it takes to make it");
            while(Utility.WantToPlay()){
                bool result = characterGames.FieldGoal();
                if(result){
                    System.Console.WriteLine("Deboer is impressed and says he will give you a clue.");
                    return true;
                }
                else{
                    System.Console.WriteLine("You couldn't make a field goal (Pat McAfee would be disappointed). No clue for you.");
                    return false;
                }
            }
            System.Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        public bool Squirrel(){
            System.Console.WriteLine("As you walk through the quad, you see a group of squirrels huddled together. The biggest of the squirrels turns to you, picks up a paper, and then brings it toyou. The paper tells you that the squirrels will give you a clue if you help them collect acorns for the winter. Seems a little weird, but you will do anythingfor some more clues.");
            while(Utility.WantToPlay()){
                bool won = characterGames.AcornGame();
                if(won){
                    return true;
                }
                else{
                    System.Console.WriteLine("You missed too many acorns. No clue for you.");
                    return false;
                }
            }
            System.Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        
        public bool BigAl(){
            System.Console.WriteLine("You find Big Al at Coleman Coliseum running around in circles waving his arms around. You calm him down and ask him what's wrong. He says he is running late for the football game and he can't find his way to the stadium because of all the gameday road blockers! If you can help him find his way to the stadium he will give you a clue to solve your mystery.");
            while(Utility.WantToPlay()){
                bool result = characterGames.ElephantMarch();
                if(result){
                    System.Console.WriteLine("You helped Big Al out so he gives you a clue.");
                    return true;
                }
                else{
                    System.Console.WriteLine("Big Al doesn't make it to the game and the team loses without their mascot by their side. You didn't earn a clue");
                    return false;
                }
            }
            System.Console.WriteLine("\nPress any key to return to the menu.");
            Console.ReadKey();
            Console.Clear();
            return false;
        }
        public void MakeGuess(string userChoice){
            string[] hidingSpots = Suspects.GetHidingSpots();
            string[] suspectList = Suspects.GetSuspectList();
            System.Console.WriteLine("Are you ready to make your guess? Rememeber you only get one attempt or the game ends.");
            System.Console.WriteLine("Who do you think stole the trophy? (Enter the number)");
            for(int i = 0; i < suspectList.Length; i++){
                System.Console.WriteLine($"{i + 1}. {suspectList[i]}");
            }
            int thiefGuess = int.Parse(Console.ReadLine())-1;
            Console.Clear();
            System.Console.WriteLine("Where do you think they hid it? (Enter the number)");
            for(int i = 0; i < hidingSpots.Length; i++){
                System.Console.WriteLine($"{i + 1}. {hidingSpots[i]}");
            }
            int hidingGuess = int.Parse(Console.ReadLine())-1;
            Console.Clear();
            string correctThief = Suspects.GetThief();
            string correctSpot = Suspects.GetTrophySpot();
            if(suspectList[thiefGuess] == correctThief && hidingSpots[hidingGuess] == correctSpot){
                System.Console.WriteLine("Congrats you have caught the thief and found the trophy!");
                return;
            }
            else if(suspectList[thiefGuess] == correctThief && hidingSpots[hidingGuess] != correctSpot){
                System.Console.WriteLine("You caught the thief, but never found the trophy. The University of Alabama will forever miss their prized national championship trophy (good thing we still    have 17 others))");
                System.Console.WriteLine($"The correct spot was {correctSpot}");
                return;
            }
            else if(suspectList[thiefGuess] != correctThief && hidingSpots[hidingGuess] == correctSpot){
                System.Console.WriteLine("You found our beloved trophy, but the thief is still at large and could strike again at any time");
                System.Console.WriteLine($"The correct thief was {correctThief}");
                return;
                
            }
            else{
                System.Console.WriteLine("You didn't find the trophy OR the thief. You've disappointed everyone at the University and this will forever be a dark time in the school's history.");
                System.Console.WriteLine($"The correct answer was thief: {correctThief} and spot: {correctSpot}");
                return;
            }
        }
        public void ProgramExit(){
            System.Console.WriteLine("Goodbye!");
        }
    }   
    
}