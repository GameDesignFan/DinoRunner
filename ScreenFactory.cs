﻿using System;
using System.Collections.Generic;
using System.Text;
using DinoRunner.StateManagement;

namespace DinoRunner
{
    // Our game's implementation of IScreenFactory which can handle creating the screens
    public class ScreenFactory : IScreenFactory
    {
        public GameScreen CreateScreen(Type screenType)
        {
            // All of our screens have empty constructors so we can just use Activator
            return Activator.CreateInstance(screenType) as GameScreen;
        }
    }
}
