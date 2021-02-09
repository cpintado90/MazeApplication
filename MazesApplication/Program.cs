using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace MazesApplication
{
    class Program
    {
        //Change the file directory to read in mazes
        static readonly string textFile = @"C:\Users\pintc\Downloads\maze1.txt";

        static void Main(string[] args)
        {
            Stopwatch timer = new Stopwatch();
            Console.WriteLine("Maze Application START: ");
            timer.Start();

            //Get the path of the maze from the file
            int[,] mazePath = readFile(textFile);

            string printResult = "";
            navigateMaze(mazePath, 0, 0, printResult);

            timer.Stop();
            Console.WriteLine(printResult);
            Console.WriteLine("Maze Application END Application took: " + timer.Elapsed.TotalSeconds + " seconds to complete.");
        }


        /*
         * This method reads in a file
         */
        public static int[,] readFile(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string fileInput = File.ReadAllText(fileName);
                    int lineCount = File.ReadAllLines(fileName).Count();
                    int i = 0, j = 0;

                    int rows = lineCount + 1;
                    int cols = lineCount + 1;
                    int[,] results = new int[rows, cols];

                    foreach (var row in fileInput.Split('\n'))
                    {
                        j = 0;
                        foreach (var col in row.Trim().Split(' '))
                        {
                            results[i, j] = int.Parse(col.Trim());
                            j++;
                        }
                        i++;
                    }

                    return results;
                }
            } catch(Exception e)
            {
                Console.WriteLine("An error has occurred: " + e.Message);
            }

            return null;
        }

        /*
         * This method is used to navigate the maze 
         */
        public static void navigateMaze(int[,] maze, int row, int column, string printPathResult)
        {
            int rowSize = maze.GetLength(0);
            //int columnSize = maze.GetLength(1);

            /*
             * If we reached the end of the maze, 
             * end the method
             */
            if (row == rowSize && maze[row, column] == 1)
            {
                return;
            }

            /*
             * If the position is equal to 2, then we are in the current position 
             * and need to continue searching the path
             */
            if (maze[row, column] == 2)
            {
                return;
            }

            //get the current position
            int position = maze[row, column];

            //Mark the current position
            maze[row, column] = 2;

            // Move to the Right 
            if (column + position < maze.GetLength(1))
            {
                printPathResult += printPath(row, column, printPathResult) + ",";
                position++;
                navigateMaze(maze, row, column + position, printPathResult);
            }

            // Move Down 
            if (row + position < maze.GetLength(0))
            {
                printPathResult += printPath(row, column, printPathResult) + ",";
                position++;
                navigateMaze(maze, row + position, column, printPathResult);
            }

            // Move to the Left 
            if (column - position > 0)
            {
                printPathResult += printPath(row, column, printPathResult) + ",";
                position--;
                navigateMaze(maze, row, column - position, printPathResult);
            }

            // Move Up
            if (row - position > 0)
            {
                printPathResult += printPath(row, column, printPathResult) + ",";
                position--;
                navigateMaze(maze, row - position, column, printPathResult);
            }

            //return the current position to the original value
            maze[row, column] = position;
        }

        public static string printPath(int row, int column, string result)
        {
            return result += result + "(" + row + "," + column + ")";
        }
    }
}
