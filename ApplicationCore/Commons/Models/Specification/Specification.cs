using System.Linq.Expressions;
using ApplicationCore.Commons.Interfaces.Specifications;

public abstract class Specification<T> : ISpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; protected set; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; protected set; }
    public Expression<Func<T, object>>? OrderByDescending { get; protected set; }
    public int? Skip { get; protected set; }
    public int? Take { get; protected set; }

    public void AndAlso(Expression<Func<T, bool>> newCriteria)
    {
        if (Criteria == null)
        {
            Criteria = newCriteria;
        }
        else
        {
            var parameter = Expression.Parameter(typeof(T));

            var left = Expression.Invoke(Criteria, parameter);
            var right = Expression.Invoke(newCriteria, parameter);
            var body = Expression.AndAlso(left, right);

            Criteria = Expression.Lambda<Func<T, bool>>(body, parameter);
        }
    }
}