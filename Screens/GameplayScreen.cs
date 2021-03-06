using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DinoRunner.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace DinoRunner.Screens
{
    // This screen implements the actual game logic. It is just a
    // placeholder to get the idea across: you'll probably want to
    // put some more interesting gameplay in here!
    public class GameplayScreen : GameScreen
    {
        private ContentManager _content;
        private SpriteFont _gameFont;

        private float _pauseAlpha;

        private Texture2D _topLayer;
        private Texture2D _light;
        private Texture2D _middleLayer;
        private Texture2D _downLayer;
        private Texture2D _sky;

        private Texture2D _sword;

        private Player _player;
        private Song _backgroundMusic;

        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        // Load graphics content for the game
        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _gameFont = _content.Load<SpriteFont>("Fonts/PressStart2P");

            _topLayer = _content.Load<Texture2D>("Sprites/Background/TopLayer");
            _light = _content.Load<Texture2D>("Sprites/Background/Light");
            _middleLayer = _content.Load<Texture2D>("Sprites/Background/MiddleLayer");
            _downLayer = _content.Load<Texture2D>("Sprites/Background/DownLayer");
            _sky = _content.Load<Texture2D>("Sprites/Background/Sky");

            _sword = _content.Load<Texture2D>("Sprites/Items/sword");

            _player = new Player();
            _player.LoadContent(_content);

            _backgroundMusic = _content.Load<Song>("Sounds/NDKG_CreepyAtmosphere_Looped");

            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            // while, giving you a chance to admire the beautiful loading screen.
            Thread.Sleep(1000);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(_backgroundMusic);
        }


        public override void Deactivate()
        {
            base.Deactivate();
        }

        public override void Unload()
        {
            _content.Unload();
        }

        // This method checks the GameScreen.IsActive property, so the game will
        // stop updating when the pause menu is active, or if you tab away to a different application.
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                _pauseAlpha = Math.Min(_pauseAlpha + 1f / 32, 1);
            else
                _pauseAlpha = Math.Max(_pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                // Do something here
                _player.Update(gameTime);
            }
        }

        // Unlike the Update method, this will only be called when the gameplay screen is active.
        public override void HandleInput(GameTime gameTime, InputState input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            var keyboardState = input.CurrentKeyboardStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!

            var movement = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.Left))
                movement.X--;

            if (keyboardState.IsKeyDown(Keys.Right))
                movement.X++;

            if (keyboardState.IsKeyDown(Keys.Up))
                movement.Y--;

            if (keyboardState.IsKeyDown(Keys.Down))
                movement.Y++;

            if (movement.Length() > 1)
                movement.Normalize();
        }

        public override void Draw(GameTime gameTime)
        {
            // This game has a blue background. Why? Because!
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 0, 0);

            // Our player and enemy are both actually just text strings.
            var spriteBatch = ScreenManager.SpriteBatch;

            Matrix transform;


            var scale = Matrix.CreateScale(4, 3, 480f);

            var playerX = MathHelper.Clamp(_player.Position.X, 300, 13600);
            var offsetX = 300 - playerX;

            // Sky
            transform = Matrix.CreateTranslation(offsetX * 0.111f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform * scale);
            spriteBatch.Draw(_sky, Vector2.Zero, Color.White);
            spriteBatch.End();

            // Down Layer
            transform = Matrix.CreateTranslation(offsetX * 0.111f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform * scale);
            spriteBatch.Draw(_downLayer, Vector2.Zero, Color.White);
            spriteBatch.End();

            // Middle Layer
            transform = Matrix.CreateTranslation(offsetX * 0.111f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform * scale);
            spriteBatch.Draw(_middleLayer, Vector2.Zero, Color.White);
            spriteBatch.End();

            // Light
            transform = Matrix.CreateTranslation(offsetX * 0.111f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform * scale);
            spriteBatch.Draw(_light, Vector2.Zero, Color.White);
            spriteBatch.End();

            // Top Layer
            transform = Matrix.CreateTranslation(offsetX * 0.111f, 0, 0);
            spriteBatch.Begin(transformMatrix: transform * scale);
            spriteBatch.Draw(_topLayer, Vector2.Zero, Color.White);
            spriteBatch.End();


            // Player
            spriteBatch.Begin();
            _player.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            // First Sword
            spriteBatch.Begin();
            spriteBatch.Draw(_sword, new Vector2(385, 380), null, Color.White, 0.4f, new Vector2(_sword.Width / 2, _sword.Height / 2), 1.0f, SpriteEffects.None, 0);
            spriteBatch.End();
        }
    }
}
