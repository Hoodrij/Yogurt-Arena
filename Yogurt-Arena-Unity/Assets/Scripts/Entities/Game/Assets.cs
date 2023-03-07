using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class Assets : IComponent
    {
        public readonly SO<Data> Data = new("Data"); 
        
        public readonly Asset<World> World = new Asset<World>("World");
        public readonly Asset<CameraView> Camera = new Asset<CameraView>("Camera");
        public readonly Asset<InputFieldView> InputField = new Asset<InputFieldView>("InputField");
    }
}