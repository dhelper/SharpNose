using System;

namespace SharpNose.Core
{
    public class MessageRecievedEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public MessageRecievedEventArgs(string message)
        {
            Message = message;
        }
    }
}