using ArtCollectionApi.Models;
using ArtCollectionApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtCollectionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PrintsController : ControllerBase
{
    private readonly PrintsService _printsService;
    public PrintsController(PrintsService printsService)
    {
        _printsService = printsService;
    }

     [HttpGet]
    public async Task<List<Print>> Get() =>
        await _printsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Print>> Get(string id)
    {
        var print = await _printsService.GetAsync(id);

        if (print is null)
        {
            return NotFound();
        }

        return print;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Print newPrint)
    {
        await _printsService.CreateAsync(newPrint);

        return CreatedAtAction(nameof(Get), new { id = newPrint.Id }, newPrint);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Print updatedPrint)
    {
        var print = await _printsService.GetAsync(id);

        if (print is null)
        {
            return NotFound();
        }

        updatedPrint.Id = print.Id;

        await _printsService.UpdateAsync(id, updatedPrint);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var print = await _printsService.GetAsync(id);

        if (print is null)
        {
            return NotFound();
        }

        await _printsService.RemoveAsync(id);

        return NoContent();
    }

}
