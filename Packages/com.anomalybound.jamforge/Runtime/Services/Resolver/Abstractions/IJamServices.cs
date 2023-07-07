namespace JamForge
{
    public interface IJamServices
    {
        public void Register<TContract, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton)
            where TImplementation : TContract;

        public void Register<TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton);

        public void RegisterInstance<TInstance>(TInstance instance);

        public void RegisterAll<TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Singleton);

        public void Build();
    }

    public enum ServiceLifetime
    {
        Transient,
        Singleton,
        Scoped,
    }
}
