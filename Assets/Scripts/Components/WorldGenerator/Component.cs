namespace SimpleWorldGeneration.WorldGenerator
{
    public class Component
    {
        public IController controller => _controller;

        readonly Interface.Controller _controller;

        public Component()
        {
            var colorCalculator = new Logic.ColorCalculator();
            var chunkGenerator = new Logic.ChunksGenerator(colorCalculator);

            var controller = new Interface.Controller(chunkGenerator);

            _controller = controller;
        }
    }
}
