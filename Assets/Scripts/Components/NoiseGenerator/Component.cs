namespace SimpleWorldGeneration.NoiseGenerator
{
    public class Component
    {
        public IController controller => _controller;

        readonly Interface.Controller _controller;

        public Component(int initialSeed)
        {
            var perlinNoise = new Logic.PerlinNoise(initialSeed);
            var whiteNoise = new Logic.WhiteNoise(initialSeed);

            var controller = new Interface.Controller(perlinNoise, whiteNoise);

            _controller = controller;
        }
    }
}
