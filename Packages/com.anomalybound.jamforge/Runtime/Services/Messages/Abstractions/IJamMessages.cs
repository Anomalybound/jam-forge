namespace JamForge
{
    public interface IJamMessages
    {
        public void RegisterFor<TMessage>();

        public void UnregisterFor<TMessage>();

        public void Build();
    }
}
