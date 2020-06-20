using System;
using System.Collections.Generic;
using System.Linq;
using WMPLib;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace AirSpace
{
    class Program
    {
        public ConsoleKeyInfo keypress = new ConsoleKeyInfo();
        public Random rand = new Random();

        public StreamReader sr;
        public StreamWriter sw;

        public int score, highscore, wayX, wayY, height, width, falldelay, wingDelay, raiseSpeed, fallSpeed;

        public int[,] shipX = new int[5, 5];
        public int[,] shipY = new int[5, 5];

        public char[,] Ship = new char[5, 5];
        private char wing;

        public int[,] pipeX = new int[30, 30];
        public int[,] pipeY = new int[30, 30];
        public char[,] pipe = new char[30, 30];
        public int splitStart, splitLength, pipePivotX, pipeWidth, r;

        public int[,] pipeX2 = new int[30, 30];
        public int[,] pipeY2 = new int[30, 30];
        public char[,] pipe2 = new char[30, 30];
        public int splitStart2, splitLength2, pipePivotX2;

        private bool gameOver, restart, isfly, isPrinted;

        private string dirFile = @"C:\Users";
        private string highscoreFile = @"HS.txt";

        static void Line1()
        {
            Console.WriteLine("||========================================================||");
        }

        static void Line2()
        {
            Console.Write("||--------------------------------------------------------||");
        }

        void ShowMainMenu()
        {
            int choiceID;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Line1();
            Line2();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(@" 
      /\    ---  ----      ----  ----    /\    ----  ----
     /  \    |  |    |    |     |    |  /  \  |     |
    /----\   |  |----      ---- |----  /----\ |      ----
   /      \ _|_ |    \     ____||     /      \ ---- |____

                          (AIR SPACE)");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Line1();
            Line2();


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t\t\t1 -  PLAY GAME");
            Console.WriteLine();
            Console.WriteLine("\t\t\t2 -  HIGHSCORE");
            Console.WriteLine();
            Console.WriteLine("\t\t\t3 -  HELP");
            Console.WriteLine();
            Console.WriteLine("\t\t\t4 -  QUIT GAME");
            Console.WriteLine();
            Console.WriteLine();


            while (true)
            {

                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.D1)
                {
                    choiceID = 1;
                    break;
                }
                else if (keypress.Key == ConsoleKey.D2)
                {
                    choiceID = 2;
                    break;
                }
                else if (keypress.Key == ConsoleKey.D3)
                {
                    choiceID = 3;
                    break;
                }
                else if (keypress.Key == ConsoleKey.D4)
                {
                    choiceID = 4;
                    break;
                }
            }

            switch (choiceID)
            {
                case 1:
                    LoadScene();
                    break;

                case 2:
                    ViewHighScore();
                    break;

                case 3:
                    ViewHelp();
                    break;

                case 4:
                    AreUsure("quit");
                    break;
            }
        }

        void ViewHighScore()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Line1();
            Line2();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(@" 
      /\    ---  ----      ----  ----    /\    ----  ----
     /  \    |  |    |    |     |    |  /  \  |     |
    /----\   |  |----      ---- |----  /----\ |      ----
   /      \ _|_ |    \     ____||     /      \ ---- |____

                          (AIR SPACE)");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Line1();
            Line2();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\t\t\tHigh score: " + highscore);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\t\t-- Press ESC to return to Main Menu --");
            while (true)
            {
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.Escape)
                    break;
            }
            ShowMainMenu();
        }

        void ViewHelp()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Line1();
            Line2();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(@" 
      /\    ---  ----      ----  ----    /\    ----  ----
     /  \    |  |    |    |     |    |  /  \  |     |
    /----\   |  |----      ---- |----  /----\ |      ----
   /      \ _|_ |    \     ____||     /      \ ---- |____

                          (AIR SPACE)");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Line1();
            Line2();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();


            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"             1 - Keep the Ship flying and 
                    avoiding the pipes");
            Console.WriteLine();
            Console.WriteLine(@"        2 - Don't let him touch the ground or
                    the top of window");
            Console.WriteLine();
            Console.WriteLine("\t\t3 - Keyboard buttons: ");
            Console.WriteLine();
            Console.WriteLine("\t\t- Press Spacebar to raise the Ship");
            Console.WriteLine("\t\t- Press R to restart game");
            Console.WriteLine();
            Console.WriteLine("\t\t- Press ESC to pause game");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("           -- Press ESC to return to Main Menu --");
            while (true)
            {
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.Escape)
                    break;
            }
            ShowMainMenu();
        }

        void AreUsure(string Case)
        {
            while (true)
            {
                if (Case == "quit")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.CursorTop = 15;
                    Console.WriteLine();
                    Console.WriteLine(" |--------------------------------------------------------|");
                    Console.WriteLine();
                    Console.WriteLine(" |                DO YOU WANT TO QUIT GAME?               |");
                    Console.WriteLine();
                    Console.WriteLine(" |                    Press 1 - Continue                  |");
                    Console.WriteLine();
                    Console.WriteLine(" |                      Press 2 - Quit                     |");
                    Console.WriteLine();
                    Console.Write(" |--------------------------------------------------------|");

                    keypress = Console.ReadKey(true);
                    if (keypress.Key == ConsoleKey.D1)
                        ShowMainMenu();
                    if (keypress.Key == ConsoleKey.D2)
                        Environment.Exit(1);
                }

            }
        }

        void Pause()
        {

            Console.CursorTop = 10;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("\t============================================");
            Console.WriteLine("\t\t\tGAME PAUSED");
            Console.WriteLine();
            Console.WriteLine("\t\tPress 1 - Resume game");
            Console.WriteLine();
            Console.WriteLine("\t\tPress 2 - Restart game");
            Console.WriteLine();
            Console.WriteLine("\t\tPress 3 - Return to Main Menu");
            Console.WriteLine("\t============================================        ");
            Console.ForegroundColor = ConsoleColor.Green;

            while (true)
            {
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.D1)
                {

                }
                else if (keypress.Key == ConsoleKey.D2)
                {
                    restart = true;
                    goto through2;
                }
                else if (keypress.Key == ConsoleKey.D3)
                    ShowMainMenu();

                break;
            }

        through2: ;

        }

        void Lose()
        {
            Console.CursorTop = 10;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine();
            Console.WriteLine("\t============================================");
            Console.WriteLine("\t\t\tGAME OVER");
            Console.WriteLine("\t\t      Your score: {0}", score);
            Console.WriteLine("\t\t      High score: {0}", highscore);
            Console.WriteLine("\t\tPress 1 -  Return to Main Menu");
            Console.WriteLine("\t\tPress 2 -  Restart game");
            Console.WriteLine("\t============================================");

            Console.ForegroundColor = ConsoleColor.Green;
            while (true)
            {
                keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.D1)
                    ShowMainMenu();
                if (keypress.Key == ConsoleKey.D2)
                {
                    goto through2;
                }
            }

        through2: ;
            restart = true;
        }

        void SetBirdInfo(char wch, char ech)
        {
            Ship[0, 0] = 'o'; shipX[0, 0] = wayX - 2; shipY[0, 0] = wayY - 1;
            Ship[0, 1] = '='; shipX[0, 1] = wayX - 1; shipY[0, 1] = wayY - 1;
            Ship[0, 2] = '='; shipX[0, 2] = wayX; shipY[0, 2] = wayY - 1;
            Ship[0, 3] = '='; shipX[0, 3] = wayX + 1; shipY[0, 3] = wayY - 1;
            Ship[0, 4] = '-'; shipX[0, 4] = wayX + 2; shipY[0, 4] = wayY - 1;

            Ship[1, 0] = ' '; shipX[1, 0] = wayX - 2; shipY[1, 0] = wayY;
            Ship[1, 1] = wch; shipX[1, 1] = wayX - 1; shipY[1, 1] = wayY;
            Ship[1, 2] = '='; shipX[1, 2] = wayX; shipY[1, 2] = wayY;
            Ship[1, 3] = ech; shipX[1, 3] = wayX + 1; shipY[1, 3] = wayY;
            Ship[1, 4] = '>'; shipX[1, 4] = wayX + 2; shipY[1, 4] = wayY;

            Ship[2, 0] = 'o'; shipX[2, 0] = wayX - 2; shipY[2, 0] = wayY + 1;
            Ship[2, 1] = '='; shipX[2, 1] = wayX - 1; shipY[2, 1] = wayY + 1;
            Ship[2, 2] = '='; shipX[2, 2] = wayX; shipY[2, 2] = wayY + 1;
            Ship[2, 3] = '='; shipX[2, 3] = wayX + 1; shipY[2, 3] = wayY + 1;
            Ship[2, 4] = '-'; shipX[2, 4] = wayX + 2; shipY[2, 4] = wayY + 1;
        }

        void SetPipeInfo()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < pipeWidth; j++)
                {
                    if (j < r)
                        pipeX[i, j] = pipePivotX - (r - j);
                    else if (j > r)
                        pipeX[i, j] = pipePivotX + (j - r);
                    else if (j == r)
                        pipeX[i, j] = pipePivotX;

                    pipeY[i, j] = i;
                    pipe[i, j] = '*';
                }
            }
            for (int k = splitStart; k < splitLength + splitStart; k++)
            {
                for (int l = 0; l < pipeWidth; l++)
                {
                    pipe[k, l] = ' ';
                }
            }
        }

        void SetPipe2Info()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < pipeWidth; j++)
                {
                    if (j < r)
                        pipeX2[i, j] = pipePivotX2 - (r - j);
                    else if (j > r)
                        pipeX2[i, j] = pipePivotX2 + (j - r);
                    else if (j == r)
                        pipeX2[i, j] = pipePivotX2;

                    pipeY2[i, j] = i;
                    pipe2[i, j] = '*';
                }
            }
            for (int k = splitStart2; k < splitLength2 + splitStart2; k++)
            {
                for (int l = 0; l < pipeWidth; l++)
                {
                    pipe2[k, l] = ' ';
                }
            }
        }

        void DeadCheck()
        {
            if (wayY + 1 <= 2 || wayY + 1 >= height - 1)
            {
                SetBirdInfo(wing, 'x');
                Render();
                gameOver = true;
            }

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (shipY[i, j] <= pipeY[splitStart, 0] - 1 || shipY[i, j] >= pipeY[splitStart + splitLength, 0])
                    {
                        if (shipX[i, j] >= pipePivotX - r && shipX[i, j] <= pipePivotX + r - 1)
                        {
                            SetBirdInfo(wing, 'x');
                            Render();
                            gameOver = true;
                        }
                    }
                    if (shipY[i, j] <= pipeY2[splitStart2, 0] - 1 || shipY[i, j] >= pipeY2[splitStart2 + splitLength2, 0])
                    {
                        if (shipX[i, j] >= pipePivotX2 - r && shipX[i, j] <= pipePivotX2 + r + 1)
                        {
                            SetBirdInfo(wing, 'x');
                            Render();
                            gameOver = true;
                        }
                    }
                }
            }
        }


        void Setup()
        {
            height = 30;
            width = 60;

            Console.SetWindowSize(width, height + 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorVisible = false;

            score = 0;
            falldelay = 0;
            raiseSpeed = 3;
            fallSpeed = 1;
            wing = 'v';

            gameOver = false;
            restart = false;
            isfly = false;

            wayX = 20;
            wayY = height / 2;

            splitStart = rand.Next(5, height - 10);
            splitStart2 = rand.Next(3, height - 13);
            splitLength = splitLength2 = 9;
            pipePivotX = 60;
            pipePivotX2 = pipePivotX + pipeWidth + 21;
            pipeWidth = 17;
            r = pipeWidth / 2;

        }

        void GameCheckInput()
        {
            while (Console.KeyAvailable)
            {
                if (!gameOver)
                    keypress = Console.ReadKey(true);
                if (keypress.Key == ConsoleKey.Spacebar)
                {
                    isfly = true;
                }
                if (keypress.Key == ConsoleKey.Escape)
                    Pause();
            }
        }

        void Logic()
        {
            pipePivotX--;
            pipePivotX2--;
            falldelay++;
            wingDelay++;

            if (wingDelay == 1)
                wing = '=';
            if (falldelay == 1)
            {
                wayY += fallSpeed;
                falldelay = 0;
            }
            if (isfly)
            {
                wayY -= raiseSpeed;

                falldelay = -1;
                wingDelay = -1;
                isfly = false;
            }

            if (pipePivotX == wayX - r || pipePivotX2 == wayX - r)
            {
                score++;
                if (score > highscore)
                {
                    highscore = score;
                }
            }

            if (pipePivotX == -r)
            {
                pipePivotX = width + r;
                splitStart = rand.Next(3, height - splitLength - 3);
            }

            if (pipePivotX2 == -r)
            {
                pipePivotX2 = width + r;
                splitStart2 = rand.Next(3, height - 13);
            }

            SetPipeInfo();
            SetPipe2Info();

            SetBirdInfo(wing, '=');

        }

        void Render()
        {
            if (!gameOver)
            {
                Console.SetCursorPosition(0, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        isPrinted = false;
                        for (int m = 0; m < 3; m++)
                        {
                            for (int n = 0; n < 5; n++)
                            {
                                if (j == shipX[m, n] && i == shipY[m, n])
                                {
                                    Console.Write(Ship[m, n]);
                                    isPrinted = true;
                                }
                            }
                        }
                        if (!isPrinted)
                        {
                            for (int a = 0; a < height; a++)
                            {
                                for (int b = 0; b < pipeWidth; b++)
                                {
                                    if (j == pipeX[a, b] && i == pipeY[a, b])
                                    {
                                        Console.ForegroundColor = ConsoleColor.Yellow;
                                        Console.Write(pipe[a, b]);
                                        isPrinted = true;
                                    }
                                }
                            }
                        }
                        if (!isPrinted)
                        {
                            for (int y = 0; y < height; y++)
                            {
                                for (int x = 0; x < pipeWidth; x++)
                                {
                                    if (j == pipeX2[y, x] && i == pipeY2[y, x])
                                    {
                                        Console.ForegroundColor = ConsoleColor.Red;
                                        Console.Write(pipe2[y, x]);
                                        isPrinted = true;
                                    }
                                }
                            }
                        }
                        if (!isPrinted)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(' ');
                        }
                    }
                    Console.WriteLine();
                }

                Console.SetCursorPosition(0, height);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-----------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Your score: " + score);

            }
        }

        void ReadHighScoreFromFile()
        {
            try
            {
                string num;
                sr = new StreamReader(highscoreFile);
                while ((num = sr.ReadLine()) != null)
                {
                    highscore = int.Parse(num);
                }
                sr.Close();
            }
            catch
            {
                Directory.CreateDirectory(dirFile);
                File.Create(highscoreFile);
                highscore = 0;

            }
        }
        void WriteHighScore()
        {
            sw = new StreamWriter(highscoreFile);
            sw.WriteLine(highscore);
            sw.Close();


        }
        void Update()
        {
            Console.Clear();
            while (true)
            {
                GameCheckInput();
                Logic();
                Render();
                DeadCheck();
                if (gameOver || restart)
                    break;

            }
            if (gameOver)
            {
                try
                {
                     WriteHighScore();
                }
                catch
                { }

                Lose();
            }
        }

        void LoadScene()
        {
            Setup();
            Update();
        }

        static void Main(string[] args)
        {
            Console.Title = "Air Space";
            Program Fb = new Program();
            Fb.ReadHighScoreFromFile();
            Fb.Setup();
            Fb.ShowMainMenu();

            while (true)
            {
                Fb.LoadScene();

            }
        }
    }
}
