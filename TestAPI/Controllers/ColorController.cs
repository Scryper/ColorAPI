using Microsoft.AspNetCore.Mvc;
using TestAPI.Repositories;
using TestAPI.ViewModels;

namespace TestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ColorController : ControllerBase
    {
        private readonly ColorRepository colorRepository;

        public ColorController(ColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ColorViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var colors = await this.colorRepository.GetAllAsync();

            return this.Ok(colors);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ColorViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateColorAsync(ColorViewModel colorViewModel)
        {
            if(!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var addedColor = await this.colorRepository.CreateColorAsync(colorViewModel);

            //if(addedColor != null)
            //{
            //    return this.Ok(addedColor);
            //}

            //return this.Conflict();

            return addedColor != null
                ? this.Ok(addedColor)
                : this.Conflict();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(int), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if(id <= 0)
            {
                return this.BadRequest(id);
            }

            var isDeleted = await this.colorRepository.DeleteAsync(id);

            return isDeleted ? this.Ok() : this.NotFound(id);
        }
    }
}
