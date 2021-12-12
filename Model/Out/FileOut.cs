using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Out
{
    public class FileOut
    {
        public FileOut(Guid id, long sizeInBytes)
        {
            Id = id;
            SizeInBytes = sizeInBytes;
        }
        public FileOut(Exception exception)
        {
            Error = exception;
        }
        public Guid Id { get;  }
        public long SizeInBytes { get;  }

        public Exception Error { get; }
    }
}
