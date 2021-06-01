using System;
using System.Collections.Generic;
using System.Linq;

namespace SonglistGenerator
{
    public static class CaretsWrapper
    {
        private const string startWrap = @"$\mathrm{";
        private const string endWrap = @"}$";

        public static string WrapCarets(string textToWrap)
        {
            if (textToWrap.Contains(startWrap))
            {
                // carets in song are already wrapped, so returning input
                return textToWrap;
            }

            var caretIndexes = new List<int>();
            for (int i = textToWrap.IndexOf('^'); i > -1; i = textToWrap.IndexOf('^', i + 1))
            {
                // for loop end when i=-1 ('a' not found)
                caretIndexes.Add(i);
            }

            caretIndexes.Reverse(); //to start replacing from the end

            foreach (var caret in caretIndexes)
            {
                var endOfBracedSection = new[] 
                { 
                    textToWrap.IndexOf(' ', caret),
                    textToWrap.IndexOf('\\', caret),
                    textToWrap.IndexOf(Environment.NewLine, caret),
                    textToWrap.IndexOf('\n', caret),
                    textToWrap.IndexOf(')', caret),
                }.Where(x => x >= 0).Min();

                textToWrap = textToWrap.Insert(endOfBracedSection, endWrap);

                var startOfBracedSection = new[]
                {
                    textToWrap.LastIndexOf(' ', caret),
                    textToWrap.LastIndexOf('(', caret),
                    textToWrap.LastIndexOf('\t', caret),
                }.Where(x => x >= 0).Max();

                textToWrap = textToWrap.Insert(startOfBracedSection + 1, startWrap);
            }

            return textToWrap;
        }
    }
}
