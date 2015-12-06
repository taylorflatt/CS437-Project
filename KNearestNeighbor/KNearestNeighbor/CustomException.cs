using System;

namespace KNearestNeighbor
{
    class CustomException
    {
        public class XCoordinateException : Exception
        {

            public XCoordinateException() { }

            public XCoordinateException(string message)
                : base(message)
            { }

            public XCoordinateException(string message, Exception inner)
                : base(message, inner)
            { }
        }

        public class YCoordinateException : Exception
        {

            public YCoordinateException() { }

            public YCoordinateException(string message)
                : base(message)
            { }

            public YCoordinateException(string message, Exception inner)
                : base(message, inner)
            { }
        }

        public class CoordinateException : Exception
        {

            public CoordinateException() { }

            public CoordinateException(string message)
                : base(message)
            { }

            public CoordinateException(string message, Exception inner)
                : base(message, inner)
            { }
        }
    }
}
