
namespace game.city.view
{
    public static class Constants
    {
        public const string MetaDataFile = "MetaData.json";
        public const string GameStateFile = "GameState.json";
        public const int GridWidth = 12;
        public const int GridLength = 12;
        
        public const double SaveGameDelay = 5;

        // Keeping the constants here. No time to add a flow from Meta to Views. Proper way would have having the Size in Shaft, Elevator and Warehouse respective.
        public const int MineLength = 8;
        public const int WarehouseDistance = 10;
        public const int ShaftDistance = 4;

        public static float FORCE_MULTIPLIER = 19800f;
        public static float BUBBLE_GRAVITY = 50f;//9.8f;
        public static float BUBBLE_GRAVITY_BUFFER = 0.02f;
        public static float FISH_LINEAR_DRAG = 6f;
    }
}
