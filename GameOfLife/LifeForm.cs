using System.Threading.Tasks;

namespace GameOfLife
{
    public class LifeForm
    {
        private World world;
        private Task golTask;
        public int Generation { get; private set; }
        private bool[,] newGeneration;

        public LifeForm(World world)
        {
            this.world = world;
            newGeneration = new bool[world.Size, world.Size];

        }

        public void BeginLife(int numOfGenerations = 10, int consolePrintSleep = 100)
        {

            BeginGeneration();
            golTask?.Wait();
            world.PrintWorld(consolePrintSleep);
            Generation = 1;

            while (Generation < numOfGenerations)
            {
                UpdateWorld();
                world.PrintWorld(consolePrintSleep);
                golTask?.Wait();

            }

        }
        private void BeginGeneration()
        {
            if (golTask == null || golTask.IsCompleted)
            {
                golTask = ProcessGeneration();
            }
        }

        private void UpdateWorld()
        {
            if (golTask == null || golTask.IsCompleted)
            {
                world.UpdateWorld(newGeneration);
                Generation++;
                golTask = ProcessGeneration();
            }
        }


        private Task ProcessGeneration()
        {
            return Task.Factory.StartNew(() =>
            {
                Parallel.For(0, world.Size, x =>
                {
                    Parallel.For(0, world.Size, y =>
                    {
                        int numberOfNeighbors = IsNeighborAlive(world.WorldBoard, world.Size, x, y, -1, 0)
                            + IsNeighborAlive(world.WorldBoard, world.Size, x, y, -1, 1)
                            + IsNeighborAlive(world.WorldBoard, world.Size, x, y, 0, 1)
                            + IsNeighborAlive(world.WorldBoard, world.Size, x, y, 1, 1)
                            + IsNeighborAlive(world.WorldBoard, world.Size, x, y, 1, 0)
                            + IsNeighborAlive(world.WorldBoard, world.Size, x, y, 1, -1)
                            + IsNeighborAlive(world.WorldBoard, world.Size, x, y, 0, -1)
                            + IsNeighborAlive(world.WorldBoard, world.Size, x, y, -1, -1);

                        bool shouldLive = false;
                        bool isAlive = world[x, y];

                        if (isAlive && (numberOfNeighbors == 2 || numberOfNeighbors == 3))
                        {
                            shouldLive = true;
                        }
                        else if (!isAlive && numberOfNeighbors == 3)
                        {
                            shouldLive = true;
                        }

                        newGeneration[x, y] = shouldLive;

                    });
                });
            });
        }

        private int IsNeighborAlive(bool[,] worldBoard, int size, int x, int y, int offsetx, int offsety)
        {
            int result = 0;

            int newOffsetX = x + offsetx;
            int newOffsetY = y + offsety;
            bool outOfBounds = newOffsetX < 0 || newOffsetX >= size | newOffsetY < 0 || newOffsetY >= size;
            if (!outOfBounds)
            {
                result = worldBoard[x + offsetx, y + offsety] ? 1 : 0;
            }
            return result;
        }

    }
}
