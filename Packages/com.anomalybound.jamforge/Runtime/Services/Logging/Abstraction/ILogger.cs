namespace JamForge.Logging
{
    public interface ILogger
    {
        void T(string message);
        void D(string message);
        void I(string message);
        void W(string message);
        void E(string message);
        void F(string message);
    }
}
