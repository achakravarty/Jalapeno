using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Tossd.Jalapeno.Model;

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
        { }

        public ControlActionException(string message)
            : base(message)
        { }

        public ControlActionException(string message, Exception innerException)
            : base(message, innerException)
        { }

        protected ControlActionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            if (info != null)
            {
                _action = info.GetString("Action");
                _controlInfo = info.GetString("ControlInfo");
            }
        }

        public ControlActionException(string action, string controlInfo)
            : this()
        {
            _action = action;
            _controlInfo = controlInfo;
        }

        public ControlActionException(string message, string action, string controlInfo)
            : this(message)
        {
            _action = action;
            _controlInfo = controlInfo;
        }

        public ControlActionException(string message, Exception innerException, string action, string controlInfo)
            : this(message, innerException)
        {
            _action = action;
            _controlInfo = controlInfo;
        }

        public ControlActionException(Exception innerException, string action, string controlInfo)
            : this(string.Format("{0} action failed for control with locator name {1}", action, controlInfo), innerException)
        {
            _action = action;
            _controlInfo = controlInfo;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            if (info != null)
            {
                info.AddValue("Action", _action);
                info.AddValue("ControlInfo", _controlInfo);
            }
        }
    }
}
