using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class Assets : IComponent
    {
        public Asset<World> World = new Asset<World>("World");
        public Asset<CameraView> Camera = new Asset<CameraView>("Camera");
        public Asset<InputFieldView> InputField = new Asset<InputFieldView>("InputField");
    }
}