using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace DinoRunner.Screens
{
    public class MainMenuScreen : MenuScreen
    {
        private ContentManager _content;
        private SoundEffect _clickPlayEffect;
        public MainMenuScreen() : base("Dino Runners")
        {
            TitleColor = Color.Black;

            var play = new MenuEntry("Play Game");
            var exit = new MenuEntry("Exit");

            play.Selected += ClickPlay;
            exit.Selected += ClickExit;

            MenuEntries.Add(play);
            MenuEntries.Add(exit);
        }

        private void ClickExit(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

        private void ClickPlay(object sender, PlayerIndexEventArgs e)
        {
            _content = new ContentManager(ScreenManager.Game.Services, "Content");
            _clickPlayEffect = _content.Load<SoundEffect>("Sounds/Confirm 1");
            _clickPlayEffect.Play();
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }
    }
}
