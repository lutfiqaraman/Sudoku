using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] board =
            {
                {0, 2, 0, 0, 0, 0, 3, 0, 0 },
                {0, 0, 4, 9, 0, 0, 0, 0, 0 },
                {5, 0, 0, 0, 0, 6, 0, 0, 7 },
                {0, 3, 0, 1, 0, 0, 0, 0, 0 },
                {0, 0, 9, 0, 0, 0, 0, 8, 0 },
                {6, 0, 0, 0, 0, 4, 0, 0, 5 },
                {0, 1, 0, 0, 0, 0, 9, 0, 0 },
                {0, 0, 0, 3, 0, 0, 0, 4, 0 },
                {0, 0, 7, 0, 0, 5, 0, 0, 2 }
            };

            Console.WriteLine("Sudoku - Initial Board");
            Console.WriteLine("0 value means Empty");
            Print(board);

            Console.WriteLine("");

            if (Suduku.Solve(board))
            {
                Console.WriteLine("Sudoku - Solved Board");
                Print(board);
            }
            else
            {
                Console.WriteLine("Puzzle cannot be solved");
            }

            Console.ReadLine();
        }

        public static void Print(int[,] board)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                    Console.Write(board[i, j] + "\t");

                Console.WriteLine();
            }
        }
    }


    public class Suduku
    {
        public static bool Solve(int[,] board)
        {
            int[,] status = new int[board.Length, board.Length];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    status[i, j] = board[i, j] > 0 ? 2 : 0;
                }
            }

            return Solve(board, status, 0, 0);
        }

        public static bool Solve(int[,] board, int[,] status, int x, int y)
        {
            if (x == 9)
            {
                int count = 0;

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        count += board[i, j] > 0 ? 1 : 0;
                    }
                }

                if (count == 81)
                    return true;
                else
                    return false;

            }

            if (status[x,y] >= 1)
            {
                int nextX = x;
                int nextY = y + 1;

                if (nextY == 9)
                {
                    nextX = x + 1;
                    nextY = 0;
                }

                return Solve(board, status, nextX, nextY);
            }
            else
            {
                Boolean[] used = new Boolean[9];

                for(int i = 0; i < 9; i++)
                {
                    if (status[x, i] >= 1)
                        used[board[x, i] - 1] = true;
                }

                for (int i = 0; i < 9; i++)
                {
                    if (status[i, y] >= 1)
                        used[board[i, y] - 1] = true;
                }

                for(int i = (x - (x%3)); i < (x - (x % 3) + 3); i++)
                    for (int j = (y - (y % 3)); j < (y - (y % 3) + 3); j++)
                    {
                        if (status[i, j] >= 1)
                            used[board[i, j] - 1] = true;
                    }

                for(int i = 0; i < used.Length; i++)
                {
                    if (!used[i])
                    {
                        status[x, y] = 1;
                        board[x, y]  = i + 1;

                        int nextX = x;
                        int nextY = y + 1;

                        if (nextY == 9)
                        {
                            nextX = x + 1;
                            nextY = 0;
                        }

                        if (Solve(board, status, nextX, nextY))
                            return true;

                        for(int m = 0; m < 9; m++)
                            for (int n = 0; n < 9; n++)
                            {
                                if ((m > x) || (m == x && n >= y))
                                {
                                    if(status[m,n] == 1)
                                    {
                                        status[m, n] = 0;
                                        board[m, n]  = 0;
                                    }
                                }
                            }
                    }
                }
            }

            return false;
        }
        
    }
}