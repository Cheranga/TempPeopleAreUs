using System.Threading.Tasks;
using PeopleAreUs.Console.Requests;

namespace PeopleAreUs.Console
{
    public interface IPeopleMediator
    {
        Task ShowPetsAsync(ShowPetsRequest request);
    }
}