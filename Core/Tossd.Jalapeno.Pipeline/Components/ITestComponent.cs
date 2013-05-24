namespace Tossd.Jalapeno.Pipeline.Components
{
    public interface ITestComponent<T>
    {
        /// <summary>
        /// Executes the component logic based on the secnario that is passed to it
        /// </summary>
        /// <param name="testScenario">(Generic) In a specific implementation, contains the scenario object that contains the test data</param>
        void Execute(T testScenario);
    }
}