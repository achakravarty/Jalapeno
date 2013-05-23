using System;
using System.Collections.Generic;
using Tossd.Jalapeno.Pipeline.Model;

namespace Tossd.Jalapeno.Pipeline.Components
{
    public abstract class ScenarioTestComponent : TestComponent<Scenario>
    {
        protected Scenario Scenario { get; set; }

        public TestMode TestMode { get; set; }

        public override void Execute(Scenario testScenario)
        {
            Scenario = testScenario;
            if (TestMode == TestMode.Smoke)
            {
                RunSmoke();
            }
            else
            {
                RunRegression();
            }
        }

        protected virtual void RunSmoke() { }

        protected virtual void RunRegression() { }
    }
}