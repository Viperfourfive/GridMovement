using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GridMovement
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private Texture2D map;
        private Texture2D player;
        private SpriteFont font;

        KeyboardState direction;
        KeyboardState old;

        private int winCondition = 0;
        private int player_X = 210;
        private int player_Y = 110;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 800;
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player = Content.Load<Texture2D>("player");
            map = Content.Load<Texture2D>("4x4map");
            font = Content.Load<SpriteFont>("font");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            PlayerInput();
            WinCondition();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            if (winCondition == 1)
            {
                graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();
                spriteBatch.DrawString(font, "You WIN!", new Vector2(10, 10), Color.Red);
                spriteBatch.End();
            }
            else
            {
                DrawFloor();
                DrawPlayer();

                spriteBatch.Begin();
                spriteBatch.DrawString(font, "Find the exit in the grid using the arrow keys.", new Vector2(130, 35), Color.Black);
                spriteBatch.End();
            }



            base.Draw(gameTime);
        }

        private void DrawFloor()
        {
            var mapHeight = map.Height;
            var mapWidth = map.Width;

            spriteBatch.Begin();
            spriteBatch.Draw(map, new Rectangle(0, 0, mapWidth, mapHeight), Color.White);
            spriteBatch.End();
        }

        private void DrawPlayer()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(player, new Vector2(player_X, player_Y), Color.White);
            spriteBatch.End();
        }

        private void PlayerInput()
        {
            direction = Keyboard.GetState(); //poll keyboard

            if (direction.IsKeyDown(Keys.Right))
            {
                if (!old.IsKeyDown(Keys.Right))
                {
                    player_X = player_X + 100;
                    player_Y = player_Y + 0;
                }
            }
            else if (direction.IsKeyDown(Keys.Left))
            {
                if (!old.IsKeyDown(Keys.Left))
                {
                    player_X = player_X - 100;
                    player_Y = player_Y - 0;
                }
            }
            else if (direction.IsKeyDown(Keys.Up))
            {
                if (!old.IsKeyDown(Keys.Up))
                {
                    player_X = player_X - 0;
                    player_Y = player_Y  - 100;
                }
            }
            else if (direction.IsKeyDown(Keys.Down))
            {
                if (!old.IsKeyDown(Keys.Down))
                {
                    player_X = player_X + 0;
                    player_Y = player_Y + 100;
                }
            }

            old = direction;
        }
        private void WinCondition()
        {
            if (player_X == 510 && player_Y == 410)
            {
                winCondition = 1;
            }
        }
    }
}
