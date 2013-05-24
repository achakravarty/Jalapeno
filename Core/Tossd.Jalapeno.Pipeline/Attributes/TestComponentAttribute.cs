using System;

namespace Tossd.Jalapeno.Pipeline.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class TestComponentAttribute : Attribute
    {
        private string _name;
        private string _type;

        /// <summary>
        /// The name of the test component
        /// </summary>
        public string Name
        {
            get { return _name ?? string.Empty; }
            set { _name = value; }
        }

        /// <summary>
        /// The type of the test component
        /// </summary>
        public string Type
        {
            get { return _type ?? string.Empty; }
            set { _type = value; }
        }

        public TestComponentAttribute() { }

        public TestComponentAttribute(string name)
        {
            _name = name;
        }

        public TestComponentAttribute(string name, string type)
        {
            _name = name;
            _type = type;
        }
    }
}