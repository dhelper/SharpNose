using System;
using System.Linq;

namespace SharpNose.SDK
{
    public static class ProgressingOrSpinningCursor
    {
        private static int _cursorSpinCount;
        private static readonly char[] CursorSpinChars = {'|', '/', '-', '\\'};

        private static char SpinCursorChar
        {
            get
            {
                int cursorIndex = _cursorSpinCount%CursorSpinChars.Count();

                _cursorSpinCount++;

                if (_cursorSpinCount == int.MaxValue)
                {
                    _cursorSpinCount = 0;
                }

                return CursorSpinChars[cursorIndex];
            }
        }

        private static bool _ioExceptionThrown = false;

        private static bool CanSpinCursor
        {
            get { return Console.OpenStandardOutput().CanSeek && !_ioExceptionThrown; }
        }

        private static void SpinCursor()
        {
            try
            {
                int cursorleft = Console.CursorLeft;
                int cursorTop = Console.CursorTop;
                Console.Write(SpinCursorChar);
                Console.CursorLeft = cursorleft;
                Console.CursorTop = cursorTop;
            }
            catch (System.IO.IOException)
            {
                _ioExceptionThrown = true;
                ProgressCursor();
            }
        }

        public static void ProgressOrSpinCursor()
        {
            if (CanSpinCursor)
            {
                SpinCursor();
            }
            else
            {
                ProgressCursor();
            }
        }

        private static void ProgressCursor()
        {
            Console.Write('.');
        }
    }
}
