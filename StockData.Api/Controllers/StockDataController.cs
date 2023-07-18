using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockData.Api.Application.Commands.CreateStockData;
using StockData.Api.Application.Commands.DeleteStockData;
using StockData.Api.Application.Queries;

namespace StockData.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class StockDataController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IStockQueries _storyQueries;

    public StockDataController(IMediator mediator, IStockQueries storyQueries)
    {
        _mediator = mediator;
        _storyQueries = storyQueries;
    }

    /// <summary>
    /// Получение данных о магазине по его id
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///      GET api/v1/stockdata/A6FF20D4-35FA-4FD0-B0F4-0AE3AA7F3F66
    /// 
    /// </remarks>
    /// <param name="id">Id магазина</param>
    /// <returns>Возвращает массив StockData</returns>
    /// <response code="200">Выполнено успешно</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<Application.Models.StockData>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStocksDataByStoreIdAsync(Guid id)
    {
        var stocks = await _storyQueries.GetStocksDataByStoreIdAsync(id, CancellationToken.None);
        return Ok(stocks);
    }

    /// <summary>
    /// Получение результирующих данных о магазине по его id за весь промежуток времени
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///      GET api/v1/stockdata/calc/A6FF20D4-35FA-4FD0-B0F4-0AE3AA7F3F66
    /// 
    /// </remarks>
    /// <param name="id">Id магазина</param>
    /// <returns>Возвращает массив StockData</returns>
    /// <response code="200">Выполнено успешно</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpGet]
    [Route("calc/{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<Application.Models.StockData>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetSumStocksDataByStoreIdAsync(Guid id)
    {
        var stocks = await _storyQueries.GetSumOfStockDataAsync(id, CancellationToken.None);
        return Ok(stocks);
    }


    /// <summary>
    /// Создание записи с данными о магазине
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     POST api/v1/stockdata
    ///     {
    ///       "id": null,
    ///       "storeItemId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    ///       "backstory": 1,
    ///       "frontstore": 8,
    ///       "shoppingWindow": 456,
    ///       "accuracy": 888,
    ///       "onFloorAvailability": 98,
    ///       "meanAge": 2
    ///     }
    ///  
    /// </remarks>
    /// <returns>Возвращает Created</returns>
    /// <response code="201">Создано успешно</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStoreAsync([FromBody] CreateStockDataCommand stockDataCommand)
    {
        var storeId = await _mediator.Send(stockDataCommand);
        return Ok(storeId);
    }

    /// <summary>
    /// Удаление данных о магазине по его id
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     DELETE api/v1/stockdata/C021C102-1B43-42CB-B73C-089CEAFA8640
    /// 
    /// </remarks>
    /// <returns>Возвращает NoContent</returns>
    /// <response code="204">Удалено успешно</response>
    /// <response code="404">Не удалось найти магазин по переданному Id</response>
    [HttpDelete]
    [Route("{storeItemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStoreAsync(Guid storeItemId)
    {
        var command = new DeleteStockDataCommand { StoreItemId = storeItemId };
        await _mediator.Send(command);
        return NoContent();
    }
}