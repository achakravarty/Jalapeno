using System;
using System.Collections.Generic;
using Tossd.Jalapeno.Pipeline.Components;

namespace Tossd.Jalapeno.Pipeline.Core
{
    public abstract class TestPipeline<T> : List<ITestComponent<T>>, ITestPipeline<T>
    {
        public abstract List<ITestComponent<T>> PipelineComponents { get; }

        public virtual bool Initialize(List<string> components)
        {
            try
            {
                if (TestComponentManager.ComponentTypes == null || TestComponentManager.ComponentTypes.Count <= 0)
                {
                    TestComponentManager.LoadComponentTypes();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Pipeline Initialization failed", ex);
            }
        }

        public abstract void Execute(T testScenario);
    }
}