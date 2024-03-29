namespace Domain.Factories;

public interface IFactory<out TEntity>
{
    TEntity Build();
}