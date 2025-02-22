#region File Description
//-----------------------------------------------------------------------------
// Program.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

using System;

namespace TheLostLevels
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        
        static void Main(string[] args)
        {
            
            
            //using (MainGame game = new MainGame())
            //{
            //    game.Run();
            //}
            using (TheLostLevelsGame game = new TheLostLevelsGame())
            {
                game.Run();
            }

            //using (LevelEditor game = new LevelEditor())
            //{
            //    game.Run();
            //}
        }
    }
#endif
}

