namespace Tossd.Jalapeno.Pipeline.Components
{
    public interface ITestComponent<T>
    {
        void Execute(T testScenario);
    }
}