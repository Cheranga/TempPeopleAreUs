using System.Threading.Tasks;
using PeopleAreUs.Console.Application.Requests;

namespace PeopleAreUs.Console.Application
{
    public interface IPeopleManager
    {
        Task ShowPetsAsync(ShowPetsRequest request);
    }
}