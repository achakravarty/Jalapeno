using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tossd.Jalapeno.Pipeline.Components
{
    public interface ITestPipeline<T> : IEnumerable<ITestComponent<T>>
    {
        List<ITestComponent<T>> PipelineComponents { get; }

        bool Initialize(List<string> components);
    }
}
