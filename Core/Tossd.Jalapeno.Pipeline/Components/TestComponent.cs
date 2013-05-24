namespace Tossd.Jalapeno.Pipeline.Components
{
    public abstract class TestComponent<T> : ITestComponent<T>
    {
        /// <summary>
        /// Executes the component logic based on the secnario that is passed to it. This is an abstratc implementation of the interface
        /// </summary>
        /// <param name="testScenario">(Generic) In a specific implementation, contains the scenario object that contains the test data</param>
        public abstract void Execute(T testScenario);
    }
}