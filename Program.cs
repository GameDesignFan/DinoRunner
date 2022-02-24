using System;

namespace DinoRunner
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DinoRunnerGame())
                game.Run();
        }
    }
}
