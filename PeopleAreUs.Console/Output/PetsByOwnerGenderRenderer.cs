using System;
using System.Linq;
using System.Threading.Tasks;
using PeopleAreUs.Console.ViewModels;

namespace PeopleAreUs.Console.Output
{
    public class PetsByOwnerGenderRenderer:IRenderer<PetsByOwnerGenderViewModel>
    {
        public async Task RenderAsync(PetsByOwnerGenderViewModel data)
        {
            if (data?.PetsMappedByOwnersGender == null || !data.PetsMappedByOwnersGender.Any())
            {
                await PrintInStyleAsync("No pets!", ConsoleColor.Red);

                return;
            }

            foreach (var (key, value) in data.PetsMappedByOwnersGender)
            {
                await PrintInStyleAsync($"{key.ToString()}\n", ConsoleColor.Green);
                foreach (var subItem in value)
                {
                    await PrintInStyleAsync($"* {subItem}\n", ConsoleColor.Yellow);
                }

                await System.Console.Out.WriteLineAsync();
            }
        }

        private async Task PrintInStyleAsync(string value, ConsoleColor textColor)
        {
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            try
            {
                System.Console.ForegroundColor = textColor;
                await System.Console.Out.WriteAsync(value);
            }
            finally
            {
                System.Console.ResetColor();
            }
        }
    }
}