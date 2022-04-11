namespace eStore.Services
{
    public interface IGetNameReposatory<T>
    {
        T GetByName(string name);

    }
}
