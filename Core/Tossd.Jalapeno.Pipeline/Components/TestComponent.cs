namespace Tossd.Jalapeno.Pipeline.Components
{
    public abstract class TestComponent<T> : ITestComponent<T>
    {
        public abstract void Execute(T testScenario);
    }
}