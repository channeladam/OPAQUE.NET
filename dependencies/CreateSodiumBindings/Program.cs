using System;
using CppSharp;

namespace CreateSodiumBindings
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleDriver.Run(new SodiumBindingLibrary());
        }
    }
}
