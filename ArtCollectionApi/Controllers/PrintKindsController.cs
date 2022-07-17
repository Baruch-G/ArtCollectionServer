using ArtCollectionApi.Models;
using ArtCollectionApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ArtCollectionApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PrintKindsController : ControllerBase
{
    private readonly PrintKindsService _printKindsService;
    public PrintKindsController(PrintKindsService printKindsService)
    {
        _printKindsService = printKindsService;
    }

    [HttpGet]
    public async Task<List<PrintKind>> Get() =>
        await _printKindsService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<PrintKind>> Get(string id)
    {
        var print = await _printKindsService.GetAsync(id);

        if (print is null)
        {
            return NotFound();
        }

        return print;
    }

    [HttpPost]
    public async Task<IActionResult> Post(PrintKind newPrint)
    {
        await _printKindsService.CreateAsync(newPrint);

        return CreatedAtAction(nameof(Get), new { id = newPrint.Id }, newPrint);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, PrintKind updatedPrint)
    {
        var print = await _printKindsService.GetAsync(id);

        if (print is null)
        {
            return NotFound();
        }

        updatedPrint.Id = print.Id;

        await _printKindsService.UpdateAsync(id, updatedPrint);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var print = await _printKindsService.GetAsync(id);

        if (print is null)
        {
            return NotFound();
        }

        await _printKindsService.RemoveAsync(id);

        return NoContent();
    }

}
