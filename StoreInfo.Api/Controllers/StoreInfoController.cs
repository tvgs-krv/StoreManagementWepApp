using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreInfo.Api.Application.Commands.CreateStoreItem;
using StoreInfo.Api.Application.Commands.DeleteStoreItem;
using StoreInfo.Api.Application.Commands.UpdateStoreItem;
using StoreInfo.Api.Application.Models;
using StoreInfo.Api.Application.Queries;
using StoreInfo.Api.Application.Queries.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace StoreInfo.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class StoreInfoController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IStoryQueries _storyQueries;

    public StoreInfoController(IMediator mediator, IStoryQueries storyQueries)
    {
        _mediator = mediator;
        _storyQueries = storyQueries;
    }

    /// <summary>
    /// Получение списка магазинов
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     GET api/v1/storeinfo[?pageSize=3&amp;pageIndex=10]
    /// 
    /// </remarks>
    /// <param name="pageSize">Количество магазинов для отображения на страницу</param>
    /// <param name="pageIndex">Номер страницы</param>
    /// <returns>Возвращает масссив StoreItems</returns>
    /// <response code="200">Выполнено успешно</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedItemsVm<StoreItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<StoreItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var stories = await _storyQueries.GetAllStoresAsync(pageSize, pageIndex, CancellationToken.None);
        return Ok(stories.Data);
    }

    /// <summary>
    /// Получение магазина по его Id
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///      GET  api/v1/storeinfo/21976100-4588-4A44-B78C-418ADCDE257F
    /// 
    /// </remarks>
    /// <param name="id">Id магазина</param>
    /// <returns>Возвращает StoreItem</returns>
    /// <response code="200">Выполнено успешно</response>
    /// <response code="404">По переданному Id не удалось найти магазин</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpGet]
    [Route("{id:guid}")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> StoreByIdAsync(Guid id)
    {
        var store = await _storyQueries.GetStoreItemAsync(id);
        return Ok(store);
    }

    /// <summary>
    /// Получение магазинов по наименованию
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///      GET api/v1/storeinfo/search/samplename[?pageSize=3&amp;pageIndex=10]
    /// 
    /// </remarks>
    /// <param name="name">Наименование магазина</param>
    /// <param name="pageSize">Количество магазинов для отображения на страницу</param>
    /// <param name="pageIndex">Номер страницы</param>
    /// <returns>Возвращает массив StoreItem</returns>
    /// <response code="200">Выполнено успешно</response>
    /// <response code="404">По переданному имени не удалось найти магазины</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpGet]
    [Route("search/{name:minlength(1)}")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStoreByNameAsync(string name, [FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
    {
        var stories = await _storyQueries.GetStoresByNameAsync(name, pageSize, pageIndex, CancellationToken.None);
        return Ok(stories.Data);
    }

    /// <summary>
    /// Обновление данных в существущем магазине
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     PUT api/v1/storeinfo
    ///     {
    ///         "id": "C021C102-1B43-42CB-B73C-089CEAFA8640",
    ///         "name": "TestName",
    ///         "countryCode": "DE",
    ///         "email": "test@mail.com",
    ///         "category": 3,
    ///         "managerInfo": {
    ///             "firstName": "Kevin",
    ///             "lastName": "Smith",
    ///             "email": "kesmi@testname.com"
    ///         }
    ///     }
    /// 
    /// </remarks>
    /// <returns>Возвращает NoContent</returns>
    /// <response code="204">Выполнено успешно</response>
    /// <response code="404">По переданному имени не удалось найти магазины</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateStoreAsync([FromBody] UpdateStoreItemCommand storeToUpdate)
    {
        await _mediator.Send(storeToUpdate);
        return NoContent();
    }

    /// <summary>
    /// Создание магазина
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     POST api/v1/storeinfo
    ///     {
    ///         "id": null,
    ///         "name": "TestName",
    ///         "countryCode": "DE",
    ///         "email": "test@mail.com",
    ///         "category": 3,
    ///         "managerInfo": {
    ///             "firstName": "Kevin",
    ///             "lastName": "Smith",
    ///             "email": "kesmi@testname.com"
    ///         }
    ///     }
    /// 
    /// </remarks>
    /// <returns>Возвращает Created</returns>
    /// <response code="201">Создано успешно</response>
    /// <response code="400">Некорректный запрос</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStoreAsync([FromBody] CreateStoreItemCommand storeItemCommand)
    {
        var storeId = await _mediator.Send(storeItemCommand);
        return Ok(storeId);
    }

    /// <summary>
    /// Удаление магазина по id
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    /// 
    ///     DELETE api/v1/storeinfo/C021C102-1B43-42CB-B73C-089CEAFA8640
    /// 
    /// </remarks>
    /// <returns>Возвращает NoContent</returns>
    /// <response code="204">Удалено успешно</response>
    /// <response code="404">Не удалось найти магазин по переданному Id</response>
    [HttpDelete]
    [Route("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteStoreAsync(Guid id)
    {
        var command = new DeleteStoreItemCommand { Id = id };
        await _mediator.Send(command);
        return NoContent();
    }
}