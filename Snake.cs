//Snake- The game snake designed to play in console. Once the game is complete the file plays "Never Gonna Give You Up" by Rick Astley
//Adam Davis
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace SnakeGame
{
    class Program
    {
        static void Main(string[] args)
        {
           
            bool play = true;
            int highScore = 0;

            //Continues until player wants to quit the game
            while (true)
            {
                //Sets up the console size and the first position of the snake
                Console.WindowHeight = 30;
                Console.WindowWidth = 61;
                int width = Console.WindowWidth;
                int height = Console.WindowHeight;
                square snake = new square();
                snake.xpos = 4;
                snake.ypos = 6;
                snake.color = ConsoleColor.DarkGreen;
                int movement = 0;
                List<int> pastx = new List<int>();
                List<int> pasty = new List<int>();
                pastx.Add(12);
                pastx.Add(8);
                pastx.Add(6);
                pasty.Add(5);
                pasty.Add(5);
                pasty.Add(5);
                pastx.Add(8);
                pasty.Add(8);

                //Places the food piece in a random poisiton in the console
                Random RanPos = new Random();
                int foodx = RanPos.Next(2, width - 2);
                if (foodx % 2 == 1)
                {
                    foodx++;
                }
                int foody = RanPos.Next(2, height - 2);
                int score = 0;
                int point = 10;
                int length = 3;
                Boolean GameOver = false;



                //Colors in the left and right side of the console
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                for (int n = 0; n < height; n++)
                {
                    Console.SetCursorPosition(0, n);
                    Console.Write("█");
                    Console.SetCursorPosition(1, n);
                    Console.Write("█");
                    Console.SetCursorPosition(width - 1, n);
                    Console.Write("█");
                    Console.SetCursorPosition(width - 2, n);
                    Console.Write("█");
                }
                //Colors in the top and bottom of the console
                for (int n = 0; n < width; n++)
                {
                    Console.SetCursorPosition(n, 0);
                    Console.Write("█");
                    Console.SetCursorPosition(n, 1);
                    Console.Write("█");
                    Console.SetCursorPosition(n, height - 1);
                    Console.Write("█");
                    Console.SetCursorPosition(n, height - 2);
                    Console.Write("█");
                }
                //Colors in the food in console
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition(foodx, foody);
                Console.Write("■");

                //Writes in the score and Highscore in console
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(2, height - 1);
                Console.Write("Score:");
                Console.SetCursorPosition(9, height - 1);
                Console.Write(score);
                Console.SetCursorPosition(2, 0);
                Console.Write("High Score:");
                Console.SetCursorPosition(14, 0);
                Console.Write(highScore);

                //Continues until player runs into wall or itself
                while (true)
                {
                    //Draws the head of the snake to be darkgreen
                    Console.SetCursorPosition(pastx[length], pasty[length]);
                    Console.Write(" ");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(pastx[0], pasty[0]);
                    Console.Write("■");

                    //Draws the rest of the snake the color green
                    for (int n = 1; n < length; n++)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(pastx[n], pasty[n]);
                        Console.Write("■");
                    }

                    //Moves the cursor so it is less visable
                    Console.SetCursorPosition(width - 1, height - 1);

                    //Test if it hit a wall
                    if (pastx[0] == 0 || pastx[0] == width - 1 || pasty[0] == 1 || pasty[0] == height - 2)
                    {
                        GameOver = true;
                    }
                    //Test if it hits itself
                    for (int n = 1; n < length; n++)
                    {
                        if (pastx[0] == pastx[n] && pasty[0] == pasty[n])
                        {
                            GameOver = true;
                        }
                    }
                    //If gameover stop game
                    if (GameOver)
                    {
                        break;
                    }

                    //Set to read the disired derection of the snake
                    int before = movement;
                    for (int x = 0; x < 10000; x++)
                    {
                        if (Console.KeyAvailable)
                        {
                            ConsoleKeyInfo Pressed = Console.ReadKey(true);
                            if (Pressed.Key.Equals(ConsoleKey.RightArrow) && before != 2)
                            {
                                movement = 0;
                            }
                            if (Pressed.Key.Equals(ConsoleKey.UpArrow) && before != 3)
                            {
                                movement = 1;
                            }
                            if (Pressed.Key.Equals(ConsoleKey.LeftArrow) && before != 0)
                            {
                                movement = 2;
                            }
                            if (Pressed.Key.Equals(ConsoleKey.DownArrow) && before != 1)
                            {
                                movement = 3;
                            }

                        }
                    }
                    //What ever direction is decided the new position is recorded
                    if (movement == 0)
                    {
                        pastx.Insert(0, pastx[0] + 2);
                        pasty.Insert(0, pasty[0]);
                    }
                    if (movement == 1)
                    {
                        pastx.Insert(0, pastx[0]);
                        pasty.Insert(0, pasty[0] - 1);
                    }
                    if (movement == 2)
                    {
                        pastx.Insert(0, pastx[0] - 2);
                        pasty.Insert(0, pasty[0]);
                    }
                    if (movement == 3)
                    {
                        pastx.Insert(0, pastx[0]);
                        pasty.Insert(0, pasty[0] + 1);
                    }
                    //Checks if the new position is where a piece of food is. If so add 1 to the length of the snake
                    if (pastx[0] == foodx && pasty[0] == foody)
                    {
                        length++;
                        Boolean match = true;
                        //Sets and draws the new position of the food piece
                        while (match)
                        {
                            foodx = RanPos.Next(2, width - 2);
                            if (foodx % 2 == 1)
                            {
                                foodx++;
                            }

                            foody = RanPos.Next(2, height - 2);
                            int x = 0;
                            for (int n = 0; n < length; n++)
                            {
                                if (foodx == pastx[n] && foody == pasty[n])
                                {
                                    x++;
                                    break;
                                }
                            }
                            if (x == 0)
                            {
                                match = false;
                            }
                        }
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(foodx, foody);

                        Console.Write("■");
                        //adds 10 to the score and draws the new score total
                        score += point;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(8, height - 1);
                        Console.Write(score);
                    }

                }
                //If it is the first death it set the console screen to say "You got the wiggly" and play Rick Astley
                if (play)
                {
                    System.Diagnostics.Process.Start(".\\RickRoll.exe");
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(5, 5);
                    Console.Write(" __   __                     ____           _    ");
                    Console.SetCursorPosition(5, 6);
                    Console.Write(" \\ \\ / /   ___    _   _     / ___|   ___   | |_  ");
                    Console.SetCursorPosition(5, 7);
                    Console.Write("  \\ V /   / _ \\  | | | |   | |  _   / _ \\  | __|");
                    Console.SetCursorPosition(5, 8);
                    Console.Write("   | |   | (_) | | |_| |   | |_| | | (_) | | |_   ");
                    Console.SetCursorPosition(5, 9);
                    Console.Write("   |_|    \\___/   \\__,_|    \\____|  \\___/   \\__|     ");
                    Console.SetCursorPosition(18, 10);
                    Console.Write(" _____   _   _   _____ ");
                    Console.SetCursorPosition(18, 11);
                    Console.Write("|_   _| | | | | | ____|");
                    Console.SetCursorPosition(18, 12);
                    Console.Write("  | |   | |_| | |  _|  ");
                    Console.SetCursorPosition(18, 13);
                    Console.Write("  | |   |  _  | | |___");
                    Console.SetCursorPosition(18, 14);
                    Console.Write("  |_|   |_| |_| |_____|  ");
                    Console.SetCursorPosition(6, 15);
                    Console.Write(" __        __  _                   _         ");
                    Console.SetCursorPosition(6, 16);
                    Console.Write(" \\ \\      / / (_)   __ _    __ _  | |  _   _ ");
                    Console.SetCursorPosition(6, 17);
                    Console.Write("  \\ \\ /\\ / /  | |  / _` |  / _` | | | | | | |");
                    Console.SetCursorPosition(6, 18);
                    Console.Write("   \\ V  V /   | | | (_| | | (_| | | | | |_| |");
                    Console.SetCursorPosition(6, 19);
                    Console.Write("    \\_/\\_/    |_|  \\__, |  \\__, | |_|  \\__, |");
                    Console.SetCursorPosition(6, 20);
                    Console.Write("                   |___/   |___/       |___/ ");

                    play = false;

                }
                //Playes game over symbol and prompts the person to play again or quit
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(22, 23);
                Console.Write("GAME OVER");
                Console.SetCursorPosition(22, 24);
                Console.Write("Play Again: Y");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(22, 25);
                Console.Write("Quit: N");




                // __   __                     ____           _       _____   _   _   _____    __        __  _                   _         
                // \ \ / /   ___    _   _     / ___|   ___   | |_    |_   _| | | | | | ____|   \ \      / / (_)   __ _    __ _  | |  _   _ 
                //  \ V /   / _ \  | | | |   | |  _   / _ \  | __|     | |   | |_| | |  _|      \ \ /\ / /  | |  / _` |  / _` | | | | | | |
                //   | |   | (_) | | |_| |   | |_| | | (_) | | |_      | |   |  _  | | |___      \ V  V /   | | | (_| | | (_| | | | | |_| |
                //   |_|    \___/   \__,_|    \____|  \___/   \__|     |_|   |_| |_| |_____|      \_/\_/    |_|  \__, |  \__, | |_|  \__, |
                //                                                                                                |___/   |___/       |___/ 


                Boolean end = false;
                //Reads in if the user types y or n
                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo Pressed = Console.ReadKey(true);
                        if (Pressed.Key.Equals(ConsoleKey.Y))
                        {
                            break;
                        }
                        if (Pressed.Key.Equals(ConsoleKey.N))
                        {
                            end = true;
                            break;
                        }

                    }
                }
                if (end)
                {
                    break;
                }
                //Updates highscore
                if (score > highScore)
                {
                    highScore = score;
                }
                //Clears console
                Console.Clear();


            }

        }
        class square
        {
            public int xpos { get; set; }
            public int ypos { get; set; }
            public ConsoleColor color { get; set; }
        }
    }
}