using System;

namespace SharpNose.Core
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public MessageRecievedEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}