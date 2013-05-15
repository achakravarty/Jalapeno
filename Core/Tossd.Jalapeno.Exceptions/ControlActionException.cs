using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Tossd.Jalapeno.Exceptions
{
    [Serializable]
    public class ControlActionException : Exception
    {
        private string _action;

        public string Action
        {
            get { return _action; }
            set { _action = value; }
        }

        private string _controlInfo;

        public string ControlInfo
        {
            get { return _controlInfo; }
            set { _controlInfo = value; }
        }

        public ControlActionException()
            : base()
        {

        }

        public ControlActionException(string message)
            : base(message)
        {

        }

        public ControlActionException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public ControlActionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
