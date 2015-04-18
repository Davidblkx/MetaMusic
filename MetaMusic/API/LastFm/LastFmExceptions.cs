using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaMusic.API.LastFm
{
    public class LastFmApiExceptionArgs
    {
        public LastFmApiExceptionArgs(int error, string message)
        {
            ErrorCode = error;
            Message = message;
        }

        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }

    public class LastFmApiException : Exception
    {
        public LastFmApiExceptionArgs Args { get; private set; }

        public LastFmApiException(LastFmApiExceptionArgs args)
            : base(args.Message)
        {
            Args = args;
        }
    }
}
