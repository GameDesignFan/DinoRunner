using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using DinoRunner.Particles;
using DinoRunner.StateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DinoRunner.Screens
{
    public class BackgroundScreen : GameScreen, IParticleEmitter
    {
        private ContentManager _content;
        private Texture2D _backgroundTexture;
        private FireworkParticleSystem _firework;
        private DinoRunnerGame _dinoRunnerGame;
        private Random _random = new Random();

        public BackgroundScreen(DinoRunnerGame dinoRunnerGame)
        {
            this._dinoRunnerGame = dinoRunnerGame;
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }

        public override void Activate()
        {
            if (_content == null)
                _content = new ContentManager(ScreenManager.Game.Services, "Content");

            _backgroundTexture = _content.Load<Texture2D>("Sprites/Background/MiddleLayer");

            _firework = new FireworkParticleSystem(_dinoRunnerGame, 3);
            _dinoRunnerGame.Components.Add(_firework);
        }

        public override void Unload()
        {
            _content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            for (int i = 0; i < 10; i++)
            {
                _firework.PlaceFirework(new Vector2(_random.Next(1000), _random.Next(1000)));
            }
            base.Update(gameTime, otherScreenHasFocus, false);
        }

        public override void Draw(GameTime gameTime)
        {
            var spriteBatch = ScreenManager.SpriteBatch;
            var viewport = ScreenManager.GraphicsDevice.Viewport;
            var fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);

            spriteBatch.Begin();
            spriteBatch.Draw(_backgroundTexture, fullscreen, new Color(TransitionAlpha, TransitionAlpha, TransitionAlpha));
            spriteBatch.End();
        }

        public Vector2 Position { get; }
        public Vector2 Velocity { get; }
    }
}
