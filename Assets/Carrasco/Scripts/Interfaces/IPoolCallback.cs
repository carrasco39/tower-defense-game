namespace Carrasco.Interfaces
{
    public interface IPoolCallback
    {
        void OnRecycleCallback();
        void OnSpawnCallback();
    }
}