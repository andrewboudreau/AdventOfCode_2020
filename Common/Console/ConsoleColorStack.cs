using System;
using System.Collections.Generic;

namespace AdventOfCode_2020.Common
{
    public static class ConsoleColorStack
    {
        public static readonly Stack<(ConsoleColor Foreground, ConsoleColor Background)> colors = new Stack<(ConsoleColor, ConsoleColor)>();

        public static void Push(ConsoleColor foreground, ConsoleColor background)
        {
            colors.Push((Console.ForegroundColor, Console.BackgroundColor));
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }

        public static void Pop()
        {
            var (Foreground, Background) = colors.Pop();
            Console.ForegroundColor = Foreground;
            Console.BackgroundColor = Background;
        }
    }

    public class DisposableConsoleColor : IDisposable
    {
        public static DisposableConsoleColor UseColors(ConsoleColor foreground, ConsoleColor background)
        {
            return new DisposableConsoleColor(foreground, background);
        }

        protected DisposableConsoleColor(ConsoleColor foreground, ConsoleColor background)
        {
            ConsoleColorStack.Push(foreground, background);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            ConsoleColorStack.Pop();
        }
    }
}
