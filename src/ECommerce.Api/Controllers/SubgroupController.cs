using ECommerce.Application.Subgroup.Commands.AddProductToSubgroup;
using ECommerce.Application.Subgroup.Commands.ChangeSubgroupName;
using ECommerce.Application.Subgroup.Commands.CreateSubgroup;
using ECommerce.Application.Subgroup.Commands.DeleteSubgroup;
using ECommerce.Application.Subgroup.Commands.DeleteProductFromSubgroup;
using ECommerce.Application.Subgroup.Queries.GetProductsBySubgroup;
using ECommerce.Application.Subgroup.Queries.GetSubgroup;
using ECommerce.Contracts.Product;
using ECommerce.Contracts.Subgroup;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
public class SubgroupController
    (IMediator _mediator) : ApiController
{

    #region GetSubgroup

    [HttpGet]
    public async Task<IActionResult> GetSubgroup([FromQuery] Guid id)
    {
        var command = new GetSubgroupQuery(id);
        var getSubgroupResult = await _mediator.Send(command);
        
        return getSubgroupResult.Match<IActionResult>(
            subgroup => Ok(new SubgroupResponse
            (id: subgroup.Id,
                name: subgroup.Name)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetProductBySubgroup([FromQuery] Guid id)
    {
        var command = new GetProductsBySubgroupQuery(id);
        var getProductResult = await _mediator.Send(command);
        
        List<ProductResponse> products = new List<ProductResponse>();
        foreach (var product in getProductResult.Value)
        {
            products.Add(new ProductResponse
            (productId: product.Id,
                name: product.Name,
                price: product.Price,
                shortDesc: product.ShortDesc,
                fullDesc: product.FullDesc,
                allowUserComments: product.AllowUserComments,
                isDelete: product.IsDelete,
                createDate: product.CreateDate,
                modifiedDate: product.ModifiedDate));
        }
        
        return getProductResult.Match<IActionResult>(
            _ => Ok(products),
            Problem);
    }

    #endregion

    #region CreateSubgroup

    [HttpPost]
    public async Task<IActionResult> CreateSubgroup([FromBody] CreateSubgroupRequest request)
    {
        var command = new CreateSubgroupCommand(request.groupId, request.name);
        var createSubgroupResult = await _mediator.Send(command);

        return createSubgroupResult.Match<IActionResult>(
            subgroup => CreatedAtAction(nameof(GetSubgroup), new { id = subgroup.Id }, new SubgroupResponse
            (id: subgroup.Id,
                name: subgroup.Name,
                groupId: subgroup.GroupId)),
            Problem);
    }

    #endregion

    #region UpdateSubgroup

    [HttpPatch("{subgroupId}/{name}")]
    public async Task<IActionResult> ChangeSubgroupName([FromRoute] Guid subgroupId, [FromRoute] string name)
    {
        var command = new ChangeSubgroupNameCommand(subgroupId, name);
        var changeSubgroupResult = await _mediator.Send(command);
        
        return changeSubgroupResult.Match(
            _ => NoContent(),
            Problem);
    }

    #endregion

    #region DeleteSubgroup

    [HttpDelete]
    public async Task<IActionResult> DeleteSubgroup([FromQuery] Guid id)
    {
        var command = new DeleteSubgroupCommand(id);
        var deleteSubgroupResult = await _mediator.Send(command);

        return deleteSubgroupResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion

    #region ProductActions

    [HttpPost]
    public async Task<IActionResult> AddProductToSubgroup([FromBody]AddProductToSubgroupRequest request)
    {
        var command = new AddProductToSubgroupCommand(request.subgroupId, request.productId);
        var addProductResult = await _mediator.Send(command);

        return addProductResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }
    
    [HttpDelete("{productId}/{subgroupId}")]
    public async Task<IActionResult> DeleteProductFromSubgroup([FromRoute] Guid productId , [FromRoute] Guid subgroupId)
    {
        var command = new DeleteProductFromSubgroupCommand(subgroupId, productId);
        var deleteProductResult = await _mediator.Send(command);
        
        return deleteProductResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion

}