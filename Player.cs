using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DinoRunner
{
    public class Player
    {
        private Texture2D _texture;
        private Vector2 _position = new Vector2(10, 340);

        public Vector2 Position => _position;

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Sprites/Characters/Sprite");
        }

        /// <summary>
        /// Updates the player
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        public void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (keyboardState.IsKeyDown(Keys.A)) _position -= Vector2.UnitX * 100 * t;
            if (keyboardState.IsKeyDown(Keys.D)) _position += Vector2.UnitX * 100 * t;
        }

        /// <summary>
        /// Draws the player sprite
        /// </summary>
        /// <param name="gameTime">An object representing time in the game</param>
        /// <param name="spriteBatch">The SpriteBatch to draw the player with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
