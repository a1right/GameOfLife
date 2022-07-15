using System;
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
                //Thread.Sleep(10);
                Generation++;
                UpdateGeneration(this, new GenerationEventArgs(Generation));
                Console.SetCursorPosition(100, 0);
                Console.Write($"Текущее поколение: {Generation}");
            }
        }

        
    }
}
