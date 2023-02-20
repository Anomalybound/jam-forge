using JamForge.Serialization;
using VContainer;

namespace JamForge
{
    public partial class Jam
    {
        [Inject]
        private IJsonSerializer _catJson;

        public static IJsonSerializer CatJson => Instance._catJson;
    }
}
