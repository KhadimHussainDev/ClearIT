using System;
using System.IO;
using System.Threading;
using EZInput;

namespace ClearIT
{
    internal class Program
    {

        static int bossX = 18;
        static int bossY = 20;
        static int enemyX = 1;
        static int enemyY = 19;
        static char[,] gameBoard = new char[20, 45];
        static int score = 0;
        static int level = 1;
        static int[] highScore = new int[100];
        static int scoreCount = 0;
        static bool loading = true;
        static bool over = false;
        static int count = 0;
        static int count1 = 0;
        static int count2 = -1;
        static int lives = 3;
        static int x;
        static int y;
        static char ch;
        static int enemyMove = 13;
        static int enemyLife = 10;
        static void Main()
        {
            lives = 3;
            enemyLife = 10;
            if (loading)
            {
                loadScores();
                loading = false;
            }

            playGame();
        }
        static void showLevel()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 45; j++)
                {
                    if (gameBoard[i, j] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(gameBoard[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(gameBoard[i, j]);
                    }
                }
                Console.Write("\n");
            }
        }
        static void loadLevel()
        {
            // string path = "E:\\2nd Semester\\Projects In C#\\ClearIT\\Data Files\\Level.txt";
            string path = "Level.txt";
            StreamReader sr = new StreamReader(path);
            string line;
            for (int i = 0; i < 20; i++)
            {
                line = sr.ReadLine();
                for (int j = 0; j < 44; j++)
                {
                    gameBoard[i, j] = line[j];
                }
            }
            sr.Close();
        }
        static void movebulletup()
        {
            for (int i = 0; i < 20; i++)
            {

                for (int j = 0; j < 45; j++)
                {
                    if (gameBoard[i, j] == '^')
                    {
                        gameBoard[i, j] = ' ';
                        Console.SetCursorPosition(j, i);
                        Console.Write(" ");
                        if (gameBoard[i - 1, j] == ' ')
                        {
                            gameBoard[i - 1, j] = '^';
                            Console.SetCursorPosition(j, i - 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("^");
                            Console.ResetColor();
                        }
                        if (gameBoard[i - 1, j] == '*')
                        {
                            gameBoard[i - 1, j] = ' ';
                            Console.SetCursorPosition(j, i - 1);
                            Console.Write(" ");
                            calculateScore();
                        }
                        if (gameBoard[i - 1, j] == '%')
                        {
                            enemyLife--;
                            calculateScore();
                        }
                        if (gameBoard[i - 1, j] == '@')
                        {
                            if (gameBoard[i - 1, j] == '@')
                            {
                                randomBomb();
                            }
                            gameBoard[i - 1, j] = ' ';
                            Console.SetCursorPosition(j, i - 1);
                            Console.Write(" ");
                            calculateScore();
                        }
                    }
                }
            }
        }
        static void moveBulletDown()
        {
            for (int i = 19; i > 0; i--)
            {

                for (int j = 44; j > 0; j--)
                {
                    if (gameBoard[i, j] == '*')
                    {
                        gameBoard[i, j] = ' ';
                        Console.SetCursorPosition(j, i);
                        Console.Write(" ");
                        if (gameBoard[i + 1, j] == ' ')
                        {

                            gameBoard[i + 1, j] = '*';
                            Console.SetCursorPosition(j, i + 1);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("*");
                            Console.ResetColor();
                        }
                        if (gameBoard[i + 1, j] == 'W')
                        {
                            gameOver();
                        }
                    }
                }
            }
        }
        static void bullet()
        {
            gameBoard[bossX - 1, bossY] = '^';
            Console.SetCursorPosition(bossY, bossX - 1);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("^");
            Console.ResetColor();
        }
        static void moveRight()
        {
            if (gameBoard[bossX, bossY + 1] == ' ' || gameBoard[bossX, bossY + 1] == '*')
            {

                gameBoard[bossX, bossY] = ' ';
                Console.SetCursorPosition(bossY, bossX);
                Console.Write(" ");
                bossY++;
                gameBoard[bossX, bossY] = 'W';
                Console.SetCursorPosition(bossY, bossX);
                Console.Write("W");
            }
        }
        static void moveLeft()
        {
            if (gameBoard[bossX, bossY - 1] == ' ' || gameBoard[bossX, bossY - 1] == '*')
            {
                gameBoard[bossX, bossY] = ' ';
                Console.SetCursorPosition(bossY, bossX);
                Console.Write(" ");
                bossY--;
                gameBoard[bossX, bossY] = 'W';
                Console.SetCursorPosition(bossY, bossX);
                Console.Write("W");
            }
        }

        static string menu()
        {
            header();

            Console.Write("\n");
            Console.Write("OPTIONS:");
            Console.Write("\n");
            Console.Write("1.Play Game from Start.");
            Console.Write("\n");
            Console.Write("2.Play Specific Level.");
            Console.Write("\n");
            Console.Write("3.View High Scores.");
            Console.Write("\n");
            Console.Write("4.Exit.");
            Console.Write("\n");
            Console.Write("Choose Any option ...");
            string option;
            //  option = Console.ReadKey().KeyChar;
              option = Console.ReadLine();

            return option;
        }
        static void playGame()
        {
            while (true)
            {

                Console.Clear();
                string option = menu();
                if (option == "1")
                {
                    playLevel();
                }
                else if (option == "2")
                {
                    chooseLevel();
                }
                else if (option == "3")
                {
                    printHighScores();
                }
                else if (option == "4")
                {
                    storeScores();
                    Console.Clear();
                    header();
                    Console.Write("Tnanks For Playing....");
                    Console.Write("\n");


                    Environment.Exit(0);
                }
                else
                {
                    Console.Write("Invalid Option.\nTry Again.\n");
                    playGame();
                }
                Console.Write("Press Any Key To Continue...");
                Console.ReadKey();
            }
        }
        static void playLevel()
        {
            Console.Clear();
            loadLevel();
            if (level == 1)
            {
                for (int i = 1; i < 5; i++)
                {
                    printLine(i);
                }
            }
            else if (level == 2)
            {
                for (int i = 1; i < 7; i++)
                {
                    printLine(i);
                }
            }
            else if (level == 3)
            {
                for (int i = 1; i < 7; i++)
                {
                    printLine(i);
                }
            }
            else if (level == 4)
            {
            }

            showLevel();
            Console.SetCursorPosition(bossY, bossX);
            Console.Write("W");
            //..............................................While
            // Loop.................................
            while (true)
            {
                Thread.Sleep(50);
                movebulletup();
                if (level == 2 || level == 3)
                {
                    moveLineDown();
                    if (level == 3)
                    {
                        fallbomb();
                    }
                }
                if (level == 4)
                {
                    printEnemy();
                    moveEnemy();
                    if (count1 == 0)
                    {
                        creatEnemy();
                        moveBulletDown();
                        count1 += 2;
                    }
                    count1--;
                }
                printScore();
                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    moveRight();
                }
                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {

                    moveLeft();
                }
                if (Keyboard.IsKeyPressed(Key.Space))
                {
                    bullet();
                }
                if (Keyboard.IsKeyPressed(Key.Escape))
                {
                    addHighScore(score);
                    level = 1;
                    score = 0;
                    Main();
                }
            }
        }
        static void chooseLevel()
        {
            Console.Write("\nChoose Level (1-4) : ");
            char op;
            op = Console.ReadKey().KeyChar;
            if (op == '1')
            {
                level = 1;
                playLevel();
            }
            else if (op == '2')
            {
                level = 2;
                playLevel();
            }
            else if (op == '3')
            {
                level = 3;
                playLevel();
            }
            else if (op == '4')
            {
                level = 4;
                playLevel();
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("\nInvalid Level.");
                Console.Write("\n");
                Console.Write("Try Again.");
                Console.Write("\n");
                Console.ResetColor();
                chooseLevel();
            }
        }
        static void printHighScores()
        {
            Console.Clear();
            header();
            Console.Write("Top 10 High Scores : ");
            Console.Write("\n");
            for (int i = 0; i < scoreCount; i++)
            {
                for (int j = i + 1; j < scoreCount; j++)
                {
                    if (highScore[i] < highScore[j])
                    {
                        int temp = highScore[i];
                        highScore[i] = highScore[j];
                        highScore[j] = temp;
                    }
                }
            }
            for (int i = 0; i < 10; i++)
            {
                Console.Write(i + 1);
                Console.Write(".\t");
                Console.Write(highScore[i]);
                Console.Write("\n");
            }
        }
        static void addHighScore(int hs)
        {
            highScore[scoreCount] = hs;
            scoreCount++;
        }
        static void loadScores()
        {
            //string path = "E:\\2nd Semester\\Projects In C#\\ClearIT\\Data Files\\HighScores.txt";
            string path = "HighScores.txt";
            StreamReader sr = new StreamReader(path);
            int hs;
            while (!sr.EndOfStream)
            {
                hs = int.Parse(sr.ReadLine());
                addHighScore(hs);
            }
            sr.Close();

        }
        static void storeScores()
        {
            string path = "HighScores.txt";
            //string path = "E:\\2nd Semester\\Projects In C#\\ClearIT\\Data Files\\HighScores.txt";
            StreamWriter sw = new StreamWriter(path, false);
            for (int i = 0; i < scoreCount; i++)
            {
                sw.WriteLine(highScore[i]);
            }
            sw.Flush();
            sw.Close();
        }
        static void header()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("*************************************");

            Console.Write("\n");
            Console.Write("*");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("           Clear It                ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.Write("*");
            Console.Write("\n");
            Console.Write("*************************************");
            Console.Write("\n");
            Console.Write("\n");

            Console.ResetColor();
        }
        static void calculateScore()
        {
            score = score + 1;
        }
        static void printScore()
        {

            Console.SetCursorPosition(50, 0);
            Console.Write("Level No. ");
            Console.Write(level);
            Console.SetCursorPosition(50, 1);
            Console.Write("Lives : ");
            Console.Write(lives);
            Console.SetCursorPosition(50, 2);
            Console.Write("Scores : ");
            Console.Write(score);
            if (level == 4)
            {
                Console.SetCursorPosition(50, 3);
                Console.Write("Enemy Health : ");
                Console.Write(enemyLife);
                Console.Write(" ");
            }
            levelCompleted();
        }
        static bool completedLevel()
        {
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 45; j++)
                {
                    if (gameBoard[i, j] == '*' && enemyLife > 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        static void levelCompleted()
        {
            if (completedLevel() && level == 1)
            {
                Console.Clear();
                Console.SetCursorPosition(50, 0);
                Console.Write("Level No. ");
                Console.Write(level);
                Console.SetCursorPosition(50, 1);
                Console.Write("Scores : ");
                Console.Write(score);
                Console.SetCursorPosition(50, 2);
                Console.Write("Congratulations ! Level ");
                Console.Write(level);
                Console.Write(" completed.");
                Console.SetCursorPosition(50, 3);
                Console.Write("Press Any Key To Play Next Level.");
                Thread.Sleep(500);
                Console.SetCursorPosition(50, 4);
                Console.Write("Press Any Key To Continue..."); Console.ReadKey();
                level++;
                playLevel();
            }
            if (completedLevel() && level == 2)
            {

                Console.Clear();
                Console.SetCursorPosition(50, 0);
                Console.Write("Level No. ");
                Console.Write(level);
                Console.SetCursorPosition(50, 1);
                Console.Write("Scores : ");
                Console.Write(score);
                Console.SetCursorPosition(50, 2);
                Console.Write("Congratulations ! Level ");
                Console.Write(level);
                Console.Write(" completed.");
                Console.SetCursorPosition(50, 3);
                Console.Write("Press Any Key To Play Next Level.");
                Thread.Sleep(500);
                Console.SetCursorPosition(50, 4);
                Console.Write("Press Any Key To Continue..."); Console.ReadKey();
                level++;
                playLevel();
            }
            if (completedLevel() && level == 3)
            {

                Console.Clear();
                Console.SetCursorPosition(50, 0);
                Console.Write("Level No. ");
                Console.Write(level);
                Console.SetCursorPosition(50, 1);
                Console.Write("Scores : ");
                Console.Write(score);
                Console.SetCursorPosition(50, 2);
                Console.Write("Congratulations ! Level ");
                Console.Write(level);
                Console.Write(" completed.");
                Console.SetCursorPosition(50, 3);
                Console.Write("Press Any Key To Play Next Level.");
                Thread.Sleep(500);
                Console.SetCursorPosition(50, 4);
                Console.Write("Press Any Key To Continue...");
                Console.ReadKey();
                level++;
                playLevel();
            }
            if (completedLevel() && level == 4)
            {
                Console.Clear();
                Console.Write("You Are Genius.");
                Console.Write("\n");
                Console.SetCursorPosition(50, 0);
                Console.Write("Level No. ");
                Console.Write(level);
                Console.SetCursorPosition(50, 1);
                Console.Write("Scores : ");
                Console.Write(score);
                Console.SetCursorPosition(50, 2);
                Console.Write("Congratulations ! Level ");
                Console.Write(level);
                Console.Write(" completed.");
                Console.SetCursorPosition(50, 3);
                level++;
                Console.SetCursorPosition(50, 4);
                Console.Write("You Have Passed All Levels.");
                Console.SetCursorPosition(50, 5);
                addHighScore(score);
                Thread.Sleep(500);
                Console.Write("Press Any Key To Continue...");
                Console.ReadKey();
                storeScores();
                level = 1;
                score = 0;

                Main();
            }
            if (over)
            {
                over = false;
                Console.SetCursorPosition(50, 0);
                Console.Write("Level No. ");
                Console.Write(level);
                Console.SetCursorPosition(50, 1);
                Console.Write("Lives : ");
                Console.Write(lives);
                Console.SetCursorPosition(50, 2);
                Console.Write("Scores : ");
                Console.Write(score);
                Console.SetCursorPosition(50, 3);
                Console.Write("OOPS ! Game Over.");
                Console.SetCursorPosition(50, 4);
                addHighScore(score);
                Thread.Sleep(500);
                Console.Write("Press Any Key To Continue..."); 
                Console.ReadKey();
                storeScores();
                level = 1;
                score = 0;
                Main();
            }
        }
        static void moveLineDown()
        {
            if (count == 100)
            {
                count = 0;

                for (int i = 19; i >= 0; i--)
                {

                    for (int j = 44; j >= 0; j--)
                    {

                        if (gameBoard[i, j] == '*')
                        {
                            if (gameBoard[i + 2, j] == '#')
                            {
                                gameBoard[i, j] = ' ';
                                Console.SetCursorPosition(j, i + 1);
                                Console.Write("*");
                                Thread.Sleep(100);
                                Console.SetCursorPosition(j, i + 1);
                                Console.Write(" ");
                                gameOver();
                            }
                            else
                            {
                                gameBoard[i + 1, j] = '*';
                                Console.SetCursorPosition(j, i + 1);
                                Console.Write("*");
                            }
                        }
                    }
                }
                for (int i = 1; i < 43; i++)
                {
                    gameBoard[1, i] = '*';
                    Console.SetCursorPosition(i, 1);
                    Console.Write("*");
                }
            }
            count++;
        }
        static void fallbomb()
        {

            if (count2 == -1)
            {
                randomBomb();
            }
            if (count2 == 3)
            {
                count2 = 0;
                gameBoard[x, y] = ch;
                Console.SetCursorPosition(y, x);
                Console.Write(gameBoard[x, y]);
                ch = gameBoard[x + 1, y];
                if (gameBoard[x + 1, y] == '#')
                {
                    gameBoard[x, y] = ' ';
                    Console.SetCursorPosition(y, x);
                    Console.Write(" ");
                    gameOver();
                    randomBomb();
                }
                else if (gameBoard[x + 1, y] == '^')
                {
                    gameBoard[x, y] = ' ';
                    Console.SetCursorPosition(y, x);
                    Console.Write(" ");
                    randomBomb();
                }
                else
                {
                    gameBoard[x + 1, y] = '@';
                    Console.SetCursorPosition(y, x + 1);
                    Console.Write("@");
                    x++;
                }
            }
            count2++;
        }
        static void randomBomb()
        {
            Random r = new Random();
            x = 1+r.Next(11);
            y = 1+r.Next(41);
            ch = gameBoard[x, y];
            gameBoard[x, y] = '@';
            Console.SetCursorPosition(y, x);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("@");
            Console.ResetColor();
        }
        static void gameOver()
        {
            if (lives > 0)
            {
                lives--;
            }

            if (lives == 0)
            {
                over = true;
            }
        }
        static void printEnemy()
        {

            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '|';
            gameBoard[enemyX + 5, enemyY + 4] = '|';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '%';
            gameBoard[enemyX + 6, enemyY + 2] = '%';
            gameBoard[enemyX + 6, enemyY + 3] = '%';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '%';
        }
        static void moveEnemy()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (enemyMove > 0)
            {
                moveEnemyLeft();
                enemyMove--;
                if (enemyMove == 0)
                {
                    enemyMove = -26;
                }
            }
            if (enemyMove <= 0)
            {
                moveEnemyRight();
                enemyMove++;
                if (enemyMove == 0)
                {
                    enemyMove = 26;
                }
            }
            Console.ResetColor();
        }
        static void moveEnemyLeft()
        {
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY--;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\ ");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '|';
            gameBoard[enemyX + 5, enemyY + 4] = '|';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '%';
            gameBoard[enemyX + 6, enemyY + 2] = '%';
            gameBoard[enemyX + 6, enemyY + 3] = '%';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '%';
        }
        static void moveEnemyRight()
        {

            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY++;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '|';
            gameBoard[enemyX + 5, enemyY + 4] = '|';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '%';
            gameBoard[enemyX + 6, enemyY + 2] = '%';
            gameBoard[enemyX + 6, enemyY + 3] = '%';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '%';
        }
        static void creatEnemy()
        {
            gameBoard[enemyX + 8, enemyY + 2] = '*';
            Console.SetCursorPosition(enemyY + 2, enemyX + 8);

            Console.Write("*");
        }
        static void printLine(int j)
        {
            for (int i = 1; i < 43; i++)
            {
                gameBoard[j, i] = '*';
                // gotoxy(i, j);
                // cout << "*";
            }
        }



    }


}
