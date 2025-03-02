namespace backend.Configurations
{
    public static class CustomServiceEnv
    {
        public static string GetEnv(string name)
        {
            string env = Environment.GetEnvironmentVariable(name) ??
                throw new InvalidOperationException($"Environment variable \"{name}\" not found.");

            return env;
        }
    }
}
