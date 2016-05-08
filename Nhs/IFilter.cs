namespace Nhs
{
    public interface IFilter<T>
    {
        void Execute(T entity);
    }
}