using System.Collections.Generic;
using System.Linq;

namespace TesteTailorIT.Application
{
    public abstract class UseCaseResponseMessage
    {
        public IEnumerable<string> Errors { get; }

        public IEnumerable<string> Information { get; }

        protected UseCaseResponseMessage(IEnumerable<string> messages, bool error = true)
        {
            if (error)
                Errors = messages;
            else
                Information = messages;
        }

        protected UseCaseResponseMessage(string message, bool error = true)
        {
            var msg = new List<string> { message };

            if (error)
                Errors = msg;
            else
                Information = msg;
        }

        protected UseCaseResponseMessage()
        {
        }

        public bool IsValid()
        {
            return Errors == null || !Errors.Any();
        }
    }
}