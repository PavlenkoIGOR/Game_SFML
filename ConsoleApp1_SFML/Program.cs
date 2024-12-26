using SFML.Graphics;
using SFML.Learning;
using SFML.System;
using SFML.Window;
using System;

namespace ConsoleApp1_SFML
{
    internal enum Directions
    {
        Left, Right, Down, Up
    }

    internal partial class Program : Game
    {
        static string bgImg = LoadTexture("background.png");
        static string foodImg = LoadTexture("food.png");
        static string playerTextures = LoadTexture("player.png");

        static string bgMusic = LoadMusic("bg_music.wav");
        static string meowSound = LoadSound("meow_sound.wav");
        static string crashSound = LoadSound("cat_crash_sound.wav");

        static (uint, uint) wndSize = (800, 600);
        static string wndTitle = "Meow runner";

        static uint scores = default;
        static uint maxScores = default;
        static uint lastMaxScores = default;
        static bool isLose = false;
        static Directions direction = default;

        static float playerX = 150.0f;
        static float playerY = 250.0f;
        static int playerWidth = 64;
        static int playerHeight = 64;
        static int playerSpeed = 300;

        static int foodX = default;
        static int foodY = default;
        static int foodSizeX = 40;
        static int foodSizeY = 40;
        static void Main(string[] args)
        {
            InitWindow(wndSize.Item1, wndSize.Item2, wndTitle);

            SetFont("comic.ttf");

            Random rnd = new Random();
            foodX = rnd.Next(0, (int)(wndSize.Item1 - foodSizeX));
            foodY = rnd.Next(200+foodSizeY, (int)(wndSize.Item2 - foodSizeY));

            PlayMusic(bgMusic, 5);

            while (true)
            {
                // 1. Расчет
                DispatchEvents();

                // Игровая логика

                // 2. Очистка буфера и окна
                ClearWindow(Color.Yellow);


                #region 3. Отрисовка буфера на окне 
                DrawSprite(bgImg, 0, 0);
                DisplayScores();
                if (isLose == false)
                {                                       
                    DrawPlayer();
                    //CheckLose();
                    PlayerMove();
                    if (playerX + playerWidth > foodX
                        &&
                        playerX < foodX + foodSizeX
                        &&
                        playerY + playerHeight > foodY
                        &&
                        playerY < foodY + foodSizeY)
                    {
                        scores++;
                        PlaySound(meowSound, 20);
                        playerSpeed += 50;
                        Console.WriteLine($"Cookies: {scores}");
                        foodX = rnd.Next(0 + foodSizeX, (int)(wndSize.Item1 - foodSizeX));
                        foodY = rnd.Next(200 + foodSizeY, (int)(wndSize.Item2 - foodSizeY));
                    }
                    CheckLose();                    
                }
                DrawPlayer();
                DrawSprite(foodImg, foodX, foodY);
                if (isLose)
                {
                    SetFillColor(Color.Red);
                    DrawText(193, 300, "Game Over", 80);
                    if (lastMaxScores > scores)
                    {
                        DrawText(193, 500, $"Highest result: {lastMaxScores}", 40);
                    }
                    else 
                    {
                        DrawText(193, 500, $"Highest result: {scores}", 40);
                        lastMaxScores = scores;
                    }
                }
                RestartGame();
                DisplayWindow();
                #endregion
                // 4. Ожидание
                Delay(1);
            }
        }
        static void GetMousePosition(RenderWindow rw)
        {
            // Получаем позицию курсора мыши
            Vector2i mousePosition = Mouse.GetPosition(rw);

            Console.WriteLine($"Mouse Position: X = {mousePosition.X}, Y = {mousePosition.Y}");
        }
        #region player Methods
        static void DrawPlayer()
        {
            if (direction == Directions.Right)
            DrawSprite(playerTextures, playerX,playerY,0,0, playerWidth, playerHeight);
            if (direction == Directions.Left)
                DrawSprite(playerTextures, playerX, playerY, 64, 0, playerWidth, playerHeight);
            if (direction == Directions.Up)
                DrawSprite(playerTextures, playerX, playerY, 64, 64, playerWidth, playerHeight);
            if (direction == Directions.Down)
                DrawSprite(playerTextures, playerX, playerY, 0, 64, playerWidth, playerHeight);
        }
        static void PlayerMove()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                direction = Directions.Up;
                playerY -= playerSpeed * DeltaTime;
            }

            else if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                direction = Directions.Down;
                playerY += playerSpeed * DeltaTime;
            }

            else if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                direction = Directions.Left;
                playerX -= playerSpeed * DeltaTime;
            }

            else if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                direction = Directions.Right;
                playerX += playerSpeed * DeltaTime;
            }
        }
        #endregion

        static void CheckLose()
        {
            if (playerX < 0 || playerY < 150 || playerX + playerWidth > wndSize.Item1 || playerY + playerHeight > wndSize.Item2)
            {
                Console.WriteLine("Loose");
                PlaySound(crashSound, 10);                
                isLose = true;                
            }
        }
        static void RestartGame()
        {
            if (isLose)
            {
                if (GetKeyDown(Keyboard.Key.R))
                {
                    scores = default;
                    isLose = false;

                    playerX = 150.0f;
                    playerY = 250.0f;
                    playerSpeed = 300;
                }
            }
        }

        static void DisplayScores()
        {
            SetFillColor(Color.Blue);
            DrawText(20, 20, $"Scores: {scores}", 24);
        }
    }
}
