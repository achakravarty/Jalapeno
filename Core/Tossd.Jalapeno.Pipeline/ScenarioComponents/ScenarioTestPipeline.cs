using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tossd.Jalapeno.Pipeline.Attributes;
using Tossd.Jalapeno.Pipeline.Model;

namespace Tossd.Jalapeno.Pipeline.Components
{
    public class ScenarioTestPipeline : TestPipeline<Scenario>
    {
        public TestMode TestMode { get; set; }

        public override List<ITestComponent<Scenario>> PipelineComponents
        {
            get { return _pipelineComponents; }
        }

        private List<ITestComponent<Scenario>> _pipelineComponents = new List<ITestComponent<Scenario>>();

        public override bool Initialize(List<string> components)
        {
            try
            {
                if (TestComponentManager.ComponentTypes == null || TestComponentManager.ComponentTypes.Count <= 0)
                {
                    TestComponentManager.LoadComponentTypes();
                }

                foreach (var component in components)
                {
                    if (TestComponentManager.ComponentTypes == null || TestComponentManager.ComponentTypes.Count <= 0)
                        throw new Exception("No component types found marked with TestComponent attribute");
                    var componentType =
                        TestComponentManager.ComponentTypes.FindAll(
                            c =>
                            c.GetCustomAttributes(false)
                                .Select(x =>
                                        ((TestComponentAttribute)x).Name).Contains(component))
                            .FirstOrDefault();
                    if (componentType == null)
                        throw new Exception(string.Format("Cannot find component marked with attribute {0}", component));
                    var pipelineComponent = Activator.CreateInstance(componentType) as ITestComponent<Scenario>;
                    if (pipelineComponent == null)
                        throw new Exception(string.Format("Component cannot be a part of Test Actions Pipeline as it does not inherit from ITestComponent"));
                    
                    _pipelineComponents.Add(pipelineComponent);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Test Actions Pipeline Initialization failed", ex);
            }
        }

        public override void Execute(Scenario testScenario)
        {
            if (_pipelineComponents != null && _pipelineComponents.Count > 0)
            {
                _pipelineComponents.ForEach(x =>
                    {
                        ((ScenarioTestComponent)x).TestMode = TestMode;
                        ((ScenarioTestComponent)x).Execute(testScenario);
                    });
            }
            else throw new Exception("Pipeline is empty. No components configured for the pipeline");
        }
    }
}
