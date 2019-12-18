namespace GameOfLife
{
    public class GameOfLife
    {
        private readonly int numOfGenerations;
        private readonly int consolePrintSleep;
        private LifeForm life;

        public GameOfLife(int worldSize, Pattern lifePattern, int numOfGenerations, int consolePrintSleep)
        {
            var world = new World(worldSize)
                .SetPattern(lifePattern);
            life = new LifeForm(world);

            this.numOfGenerations = numOfGenerations;
            this.consolePrintSleep = consolePrintSleep;
        }

        public GameOfLife Start()
        {
            life.BeginLife(numOfGenerations, consolePrintSleep);
            return this;
        }
    }
}
