using SFML.Graphics;
using SFML.Learning;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1_SFML
{
    internal class Player
    {
        internal float[] startCoord = { 20.0f, 20.0f };
        internal PlayerShape playerShape;

        internal Player() : base()
        {
                playerShape = new PlayerShape();

            playerShape.shape = new RectangleShape(new SFML.System.Vector2f(30.0f, 30.0f));
            playerShape.shape.Position = new SFML.System.Vector2f(startCoord[0], startCoord[1]);
            playerShape.shape.FillColor = Color.Green;
            
        }
        internal void PlayerMove(float startX, float startY)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.W))
            {
                startY--;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.S))
            {
                startY ++;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                startX --;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.D))
            {
                startX ++;
            }
        }
    }

}
