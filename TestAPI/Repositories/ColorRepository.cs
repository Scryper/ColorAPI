using Microsoft.EntityFrameworkCore;
using TestAPI.Models;
using TestAPI.ViewModels;

namespace TestAPI.Repositories
{
    public class ColorRepository
    {
        private readonly TestContext testContext;

        public ColorRepository(TestContext testCtx)
        {
            this.testContext = testCtx;
        }

        public async Task<IEnumerable<ColorViewModel>> GetAllAsync()
        {
            var colors = await this.testContext.Colors
                            .AsNoTracking()
                            .ToListAsync(); // .AsEnumerable();

            return colors.Select(color => new ColorViewModel
            {
                Id = color.Id,
                Name = color.Name,
            });
        }

        public async Task<ColorViewModel?> CreateColorAsync(ColorViewModel colorViewModel)
        {
            var color = await this.testContext.Colors
                            .Where(color => Equals(color.Name, colorViewModel.Name))
                            .FirstOrDefaultAsync();

            if(color is not null) // color != null
            {
                return null;
            }

            color = new Color
            {
                Name = colorViewModel.Name,
            };

            await this.testContext.Colors.AddAsync(color);
            await this.testContext.SaveChangesAsync();

            colorViewModel.Id = color.Id; 
            return colorViewModel;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            //var color = new Color { Id = id };

            //this.testContext.Colors.Attach(color);
            //this.testContext.Colors.Remove(color);
            //await this.testContext.SaveChangesAsync();

            var color = await this.testContext.Colors.FindAsync(id);

            if(color is null) // color == null
            {
                return false;
            }

            this.testContext.Colors.Remove(color);

            return (await this.testContext.SaveChangesAsync()) > 0;
        }
    }
}
