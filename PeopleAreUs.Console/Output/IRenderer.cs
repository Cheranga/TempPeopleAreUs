using System.Threading.Tasks;

namespace PeopleAreUs.Console.Output
{
    public interface IRenderer<in T>
    {
        Task RenderAsync(T data);
    }
}