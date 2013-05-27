using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tossd.Jalapeno.Pipeline.Components;

namespace Tossd.Jalapeno.Pipeline.Core
{
    public interface ITestPipeline<T> : IEnumerable<ITestComponent<T>>
    {
        /// <summary>
        /// A list of the components that make up the pipeline
        /// </summary>
        List<ITestComponent<T>> PipelineComponents { get; }

        /// <summary>
        /// Initializes the pipeline with the components
        /// </summary>
        /// <param name="components">The component names that need to be added into the pipeline</param>
        /// <returns>Success of the initialization process</returns>
        bool Initialize(List<string> components);
    }
}
