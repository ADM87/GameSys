namespace GameSystems.Utility
{
    public static class MathHelper
    {
        public static int SafeMod(int value, int max)
        {
            return (max + (value % max)) % max;
        }

        public static float SafeMod(float value, float max)
        {
            return (max + (value % max)) % max;
        }
    }
}
