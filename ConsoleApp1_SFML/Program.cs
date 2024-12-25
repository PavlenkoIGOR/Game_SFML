using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Learning;
using SFML.System;
using SFML.Window;

namespace ConsoleApp1_SFML
{
    internal partial class Program : Game
    {

        static (uint, uint) wndSize = (800, 800);
        static string wndTitle = "Meow runner";

        static uint scores = default;
        static bool isLose = false;

        static float playerX = 150.0f;
        static float playerY = 150.0f;
        static float playerWidth = 40.0f;
        static float playerHeight = 40.0f;
        static float playerSpeed = 300.0f;

        static float foodX = default;
        static float foodY = default;
        static float foodSizeX = 40.0f;
        static float foodSizeY = 40.0f;
        static void Main(string[] args)
        {
            InitWindow(wndSize.Item1, wndSize.Item2, wndTitle);

            Random rnd = new Random();
            foodX = rnd.Next(0, (int)(wndSize.Item1 - foodSizeX));
            foodY = rnd.Next(0, (int)(wndSize.Item2 - foodSizeY));

            while (true)
            {
                // 1. Расчет
                DispatchEvents();

                // Игровая логика

                // 2. Очистка буфера и окна
                ClearWindow(Color.Yellow);

                CheckLose();
                if (!isLose)
                {
                    #region 3. Отрисовка буфера на окне                    
                    DrawPlayer();
                    CheckLose();
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
                        playerSpeed += 10.0f;
                        Console.WriteLine($"Cookies: {scores}");
                        foodX = rnd.Next((int)(0 + foodSizeX), (int)(wndSize.Item1 - foodSizeX));
                        foodY = rnd.Next((int)(0 + foodSizeY), (int)(wndSize.Item2 - foodSizeY));
                    }
                    DrawFood();
                    #endregion
                }
                RestartGame();
                DisplayWindow();

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
            FillRectangle(playerX, playerY, playerWidth, playerHeight);
        }
        static void PlayerMove()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
                playerY -= playerSpeed * DeltaTime;

            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
                playerY += playerSpeed * DeltaTime;

            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
                playerX -= playerSpeed * DeltaTime;

            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
                playerX += playerSpeed * DeltaTime;
        }
        #endregion
        #region food Methods
        static void DrawFood()
        {
            FillRectangle(foodX, foodY, foodSizeX, foodSizeY);
        }
        #endregion

        static void CheckLose()
        {
            if (playerX < 0 || playerY < 0 || playerX + playerWidth > wndSize.Item1 || playerY + playerHeight > wndSize.Item2)
            {
                Console.WriteLine("Loose");
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
                    playerY = 150.0f;
                    playerSpeed = 300.0f;
                }
            }
        }
    }
}
