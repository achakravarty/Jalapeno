using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tossd.Jalapeno.Pipeline.Model
{
    public class Scenario
    {
        public List<string> TestComponents { get; set; }

        public Scenario Copy(Scenario copyTo)
        {
            copyTo.TestComponents = this.TestComponents;
            return copyTo;
        } 
    }

    public enum TestMode
    {
        Smoke, Regression
    };    
}
