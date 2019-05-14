namespace PeopleAreUs.Core
{
    public interface IMapper<in TSource, out TTarget> where TSource : class where TTarget : class, new()
    {
        TTarget Map(TSource source);
    }
}