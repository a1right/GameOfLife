﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameController
    {
        public int Generation { get; private set; } = 0;
        public static event EventHandler<GenerationEventArgs> UpdateGeneration;

        public void StartGame()
        {
            UpdateLoop();
        }
        private void UpdateLoop()
        {
            while (true)
            {
                UpdateGeneration(this, new GenerationEventArgs(Generation));
                //Thread.Sleep(300);
                Generation++;
                Console.SetCursorPosition(100, 0);
                Console.Write($"Текущее поколение: {Generation}");
            }
        }

        
    }
}
