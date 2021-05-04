using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static System.Console;
using static System.ConsoleKey;

namespace Chat_Bot
{
    static class ConsoleWork
    {

        static int _position;
        static List<string> _variants;
        static List<string> _yesNo = new() { "Да", "Нет" };

        public static string Choose(List<string> variants)
        {
            _variants = variants;
            ConsoleKey key = DownArrow;
            CursorVisible = false;
            SetCursorPosition(0, GetCursorPosition().Top + _variants.Count);
            _position = -1;

            while (true)
            {
                if (key == DownArrow && _position < _variants.Count - 1)
                {
                    _position++;
                    Paint();
                }
                if (key == UpArrow && _position > 0)
                {
                    _position--;
                    Paint();
                }
                if (key == Enter)
                {
                    CursorVisible = true;
                    ForegroundColor = ConsoleColor.White;

                    MatchCollection matches = Regex.Matches(_variants[_position], @"(((\w*\-\w*)|(\w){1,40}))");                  

                    return matches[0].Value;
                }
                key = ReadKey().Key;
            }
        }

        public static bool Choose()
        {
            return Choose(_yesNo) switch
            {
                "Да" => true,
                "Нет" => false,
                _ => false
            };
        }

        static void Paint()
        {
            SetCursorPosition(0, GetCursorPosition().Top - _variants.Count);
            for (int i = 0; i < _variants.Count; i++)
            {
                if (i == _position)
                {
                    ForegroundColor = ConsoleColor.Yellow;
                    WriteLine($"->{_variants[i]}");
                    continue;
                }
                ForegroundColor = ConsoleColor.White;
                WriteLine($"{_variants[i]}  ");
            }
            ForegroundColor = ConsoleColor.Black;
        }
    }
}
