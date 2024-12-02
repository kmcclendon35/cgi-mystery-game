using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Threading.Tasks;

namespace CGI
{
    public class CharacterGames
    {
        private static int guessCount;
        private const int defaultGameSpeed = 400;
        public CharacterGames(){
            guessCount = 0;
        } 
        public static bool RunNumbersGame(){
            System.Console.WriteLine("I'm thinking of a number between 1-100. You have 7 tries to guess what number I am thinking of and I will tell you if my number is higher or lower than your guess. If you get it right, I will give you a hint as to who stole the trophy. If you get it wrong all your loved ones will die. No pressure...");
            Random number = new Random();
            int randomNum = number.Next(1,101);
            while(guessCount < 7){
                int userGuess = 0;
                bool validInput = false;
                while(!validInput){
                    Console.WriteLine($"Attempt {guessCount + 1} of 7: Please guess a number between 1 and 100.");
                    try{
                        userGuess = int.Parse(Console.ReadLine());
                        if (userGuess < 1 || userGuess > 100){
                            Console.WriteLine("Your guess is out of range. Please guess a number between 1-100.");
                        }else{
                            validInput = true;
                        }
                    }catch(Exception e){
                    System.Console.WriteLine(e.Message);
                    }
                }
                if(userGuess == randomNum){
                    System.Console.WriteLine("Either you know how to do a binary search or you just got lucky...");
                    return true;
                }
                else if(userGuess > randomNum){
                    System.Console.WriteLine("Lower");
                }
                else{
                    System.Console.WriteLine("Higher");
                }
                guessCount++;   
            }
            return false;
        }
        public bool AcornGame(){
            // Game Configuration
            int screenWidth = 20;
            int screenHeight = 12;
            int basketPosition = screenWidth / 2; // Start basket in the middle
            int score = 0;
            int missedAcorns = 0;
            int maxMisses = 3;
            double timer = 20;
            int elapsedTime = 0;
            Random acorn = new Random();
            int[] acornPositions = new int[screenWidth]; // -1 means no acorn in that column
            for(int i = 0; i < acornPositions.Length; i++){
                acornPositions[i] = -1; // Initialize with no acorns
            }
            Console.CursorVisible = false; // Hides cursor
            Console.Clear();
            Console.WriteLine($"Catch the falling acorns with your basket (use Left/Right arrow keys).\nYou can miss up to {maxMisses} acorns. Good luck!");
            Utility.Pause(); // Let the user read the instructions
            // Game Loop
            while(GameRunning(missedAcorns, maxMisses, timer) == true){
                AcornSettings(ref acornPositions, basketPosition, screenHeight, screenWidth, ref missedAcorns, acorn, ref score);
                //Displays the actual game
                Console.Clear();
                DrawAcorns(acornPositions, screenHeight);
                DrawBasket(basketPosition, screenHeight);
                Console.SetCursorPosition(0, screenHeight);
                Console.WriteLine($"Score: {score}\tMissed: {missedAcorns}/{maxMisses}\tTime: {timer}");
                // Move the basket based on user input
                MoveBasket(ref basketPosition, screenWidth);
                // Slow down the game loop
                timer = Timer(timer, ref elapsedTime);
                Thread.Sleep(defaultGameSpeed);
            }
            Console.Clear();
            if(missedAcorns >= maxMisses){
                return false;
            }
            else{
                Console.WriteLine("Time's up! You survived the Acorn Game!");
                Console.WriteLine($"Final Score: {score}");
                Utility.Pause();
                return true;
            }
        }
        private bool GameRunning(int missedAcorns, int maxMisses, double timer){
            if(missedAcorns >= maxMisses || timer <= 0){
                System.Console.WriteLine("You missed too many acorns, game over!");
                return false;
            }
            else if(timer <= 0){
                System.Console.WriteLine("Time's up! You survived the Acorn Game!");
                return false;
            }
            return true;
        }
        private double Timer(double timer, ref int elapsedTime){
            elapsedTime += defaultGameSpeed;
            if(elapsedTime >= 1000){ //thread.sleep runs in milliseconds which means 1000 is 1 second. I did this because if you have the timer based on the thread.sleep, it won't show accurate seconds 
                timer--; //now our timer will be accurate even if the game speed gets changed at some point (also don't have to do the math of how much to increment based on the game speed)
                elapsedTime -= 1000; // Reset the accumulated time
            }
            return timer;
        }
        private void AcornSettings(ref int[] acornPositions, int basketPosition, int screenHeight, int screenWidth, ref int missedAcorns, Random acorn, ref int score){
            //this makes it where the acorns cant appear more than 6 columns away from where the basket is
            int acornAppearRange = 6;
            int leftSide = basketPosition - acornAppearRange;
            int rightSide = basketPosition + acornAppearRange;
            //error check so we cant go past the screen width
            if(leftSide < 0){
                leftSide = 0;
            }
            if(rightSide >= screenWidth){
                rightSide = screenWidth -1;
            }
            // Spawn new acorns randomly
            if(acorn.Next(10) < 2){// 20% chance to drop an acorn each frame
                int spawnColumn = acorn.Next(leftSide, rightSide +1); //the next acorn must appear within these boundaries
                //this is so that only one column will have an acorn at a time
                if(acornPositions[spawnColumn] == -1){ // Only spawn if no acorn already present
                    acornPositions[spawnColumn] = 0; // Spawns at the top row (row 0)
                }
            }
            //Make acorns fall
            for(int i = 0; i < acornPositions.Length; i++){
                if(acornPositions[i] != -1){
                    acornPositions[i]++;
                    if(acornPositions[i] == screenHeight -1){
                        //the basket is 5 long so this makes sure it covers the whole basket
                        if(i >= basketPosition && i < basketPosition + 5){
                            score ++;
                        }
                        else{
                            missedAcorns ++;
                        }
                        acornPositions[i] = -1;
                    }
                }
            }
        }
        private void MoveBasket(ref int basketPosition, int screenWidth){
            if(Console.KeyAvailable){
                    ConsoleKey pressedKey = Console.ReadKey(true).Key;
                    if(pressedKey == ConsoleKey.LeftArrow && basketPosition > 0){
                        basketPosition -= 2; //moving by 2 so you can get across the screen in time
                    }
                    else if(pressedKey == ConsoleKey.RightArrow && basketPosition < screenWidth -5){
                        basketPosition += 2;
                    }
                }
        }
        private void DrawAcorns(int[] acornPositions, int screenHeight){
            for (int i = 0; i < acornPositions.Length; i++){
                if(acornPositions[i] != -1){ // Acorn exists
                    Console.SetCursorPosition(i, acornPositions[i]);
                    Console.Write("🌰");
                }
            }
        }
        private void DrawBasket(int basketPosition, int screenHeight)
        {
            for(int i = 0; i < 5; i++){ // Basket width
                Console.SetCursorPosition(basketPosition + i, screenHeight - 1);
                Console.Write("=");
            }
        }

        public bool YeaAlabama(){
            System.Console.WriteLine("As president of The University of Alabama, my job is to make sure that everyone on this campus know the lyrics to our fight song, Yea Alabama. I will fill in parts of the song for you, but you have to do the rest. If you miss anything, you will have to start all the way from the beginning.");
            //using parallel strings to make it simpler
            string[] questions = {"Here we go:\n Yea Alabama, ___ 'em Tide!", "Every Bama man's behind you\nHit your ____.", "Go teach the _______ __ _____,", "Send the ______ _____ to a watery grave.", "And if a man starts to weaken,\nthat's a _____!",
                "For Bama's pluck and grit have\nWrit her name in ____ ____.", "Fight on, fight on, fight on men!\nRemember the ____ ____, we'll win then.", "So roll on to victory,\nHit your stride,\nYou're Dixie's _______ _____,", "Crimson tide, ____ tide, ____ tide! **JUST ENTER THE WORD ONCE**"};
            string[] answers = {"drown", "stride", "bulldogs to behave", "yellow jackets", "shame", "crimson flame", "rose bowl", "football pride", "roll"};
            while(true){
                for(int i = 0; i < questions.Length; i++){
                    System.Console.WriteLine(questions[i]);
                    string userInput = Console.ReadLine().ToLower();
                    if(userInput != answers[i]){
                        System.Console.WriteLine("It appears you don't know the fight song....");
                        return false;
                    } 
                    if(i == questions.Length -1){
                        Console.Clear();
                        System.Console.WriteLine("Amazing job! You are a true Alabama fan!");
                        return true;
                    }
                }
            }
        }
        public bool FieldGoal(){
            int power = PowerMeter();
            int accuracy = AccuracyMeter();
            bool isPowerValid = power >= 75 && power <= 95;
            bool isAccuracyValid = accuracy >= 45 && accuracy <= 55;
            if(isAccuracyValid == true && isPowerValid == true){
                System.Console.WriteLine("You made the field goal!");
                return true;
            }
            else if(isPowerValid == false){
                System.Console.WriteLine("Your power was off");
            }
            else if(isAccuracyValid== false){
                System.Console.WriteLine("Your accuracy was off");
            }
            else if(isAccuracyValid == false && isPowerValid == false){
                System.Console.WriteLine("Both power and accuracy were off");
            }  
            return false;
        }
        private int PowerMeter(){
            int power = 0;
            bool increasing = true;
            bool isRunning = true;
            System.Console.WriteLine("Press the space bar to get your power. Your power should be between 75 and 95");
            Utility.Pause();
            while(isRunning){//moves the numbers up and down
                if(increasing){
                    power += 5;
                }else{
                    power -= 5;
                }
                if(power >= 100){
                    increasing = false;
                }
                else if(power <= 0){
                    increasing = true;
                }
                Console.Clear();
                System.Console.WriteLine($"Power: {power}");
                if(Console.KeyAvailable){
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if(key == ConsoleKey.Spacebar){
                        System.Console.WriteLine($"Power is locked in as {power}");
                        return power;
                    }
                }
                Thread.Sleep(95);
            }
            return power;
        }
        private int AccuracyMeter(){
            int accuracy = 0;
            bool movingRight = true;
            bool isRunning = true;
            System.Console.WriteLine("Press the space bar to get your accuracy. Your accuracy should be between 45 and 55");
            Utility.Pause();
            while(isRunning){
                if(movingRight){
                    accuracy += 5;
                }else{
                    accuracy -= 5;
                }
                if(accuracy >= 100){
                    movingRight = false;
                }
                else if(accuracy <= 0){
                    movingRight = true;
                }
                Console.Clear();
                System.Console.WriteLine($"Accuracy: {accuracy}");
                if(Console.KeyAvailable){
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if(key == ConsoleKey.Spacebar){
                        System.Console.WriteLine($"Accuracy is locked in as {accuracy}");
                        return accuracy;
                    }
                }
                Thread.Sleep(105);//this one is slower bc the window is smaller
            }
            return accuracy;
        }
        public bool ElephantMarch(){
            int bigAlRow = 0;
            int bigAlCol = 0;
            string[,] maze = ReadMazeFile();//this [,] lets me have my x & y coord AND allows for the emojis
            Maze(maze, bigAlCol, bigAlRow);
            MoveAl(ref bigAlRow, ref bigAlCol, maze);//need ref to show big al moving
            Console.Clear();
            if(maze[bigAlRow, bigAlCol] == "🏟️"){//now i just have to comma separate rows and columns bc of [,]
                System.Console.WriteLine("Congrats you got out of the maze!");
                return true;
            }
            else{
                System.Console.WriteLine("You lost :()");
                return false;
            }
        }
        public static string[,] ReadMazeFile(){
            string[,] maze = new string[10,10];
            StreamReader inFile = new StreamReader("maze.txt"); //open
            for(int i = 0; i< 10; i++){
                string line = inFile.ReadLine();
                string[] row = line.Split(' '); // Split emojis by spaces
                for(int j = 0; j < 10; j++){
                        maze[i, j] = row[j];
                }
            }
            inFile.Close(); //close
            return maze;
        }
        public static void Maze(string[,] maze, int bigAlCol, int bigAlRow){
            Console.Clear();
            //this will allow big al to show up at the coordinate postion he is and everywhere else to have their symbol
            for(int i = 0; i < 10; i++){
                for(int j = 0; j < 10; j++){
                    if(i == bigAlRow && j == bigAlCol){
                        System.Console.Write("🐘"); //big al :)
                    }
                    else{
                        System.Console.Write(maze[i,j]);
                    }
                }
                System.Console.WriteLine();
            }
        }
        public static bool MoveAl(ref int bigAlRow, ref int bigAlCol, string[,] maze){
            while(maze[bigAlRow, bigAlCol] != "🏟️"){
                int newRow = bigAlRow;
                int newCol = bigAlCol;
                Maze(maze, bigAlCol, bigAlRow);
                System.Console.WriteLine("Press ESC to leave the game");
                ConsoleKey key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.RightArrow && bigAlCol < 9){
                    newCol ++;
                }
                else if(key == ConsoleKey.LeftArrow && bigAlCol>0){
                    newCol --;
                }
                else if(key == ConsoleKey.UpArrow && bigAlRow > 0){
                    newRow --;
                }else if(key == ConsoleKey.DownArrow && bigAlRow < 9){
                    newRow ++;
                }else if(key == ConsoleKey.Escape){
                    return false;
                }
                if(maze[newRow, newCol] != "🚧"){ //this way you cant move onto an obstacle
                    bigAlRow = newRow;
                    bigAlCol = newCol;
                }
            } 
            return maze[bigAlRow,bigAlCol] == "🏟️";
        }
    
    }
}