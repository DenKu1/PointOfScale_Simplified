using System;

namespace PointOfScale.Lib.API.Exceptions
{
    public class PointOfScaleException : Exception
    {
        public PointOfScaleException() : base()
        {
        }

        public PointOfScaleException(string message) : base(message)
        { 
        }
    }
} 