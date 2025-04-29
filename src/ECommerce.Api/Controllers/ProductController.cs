using ECommerce.Application.Product.Command.CreateProduct;
using ECommerce.Application.Product.Command.DeleteProduct;
using ECommerce.Application.Product.Command.UpdateProduct;
using ECommerce.Application.Product.Queries.GetProduct;
using ECommerce.Application.Subgroup.Queries.GetSubgroup;
using ECommerce.Contracts.Product;
using ECommerce.Contracts.Subgroup;
using ECommerce.Domain.Product;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
public class ProductController
    (IMediator _mediator) : ApiController
{
    
    #region GetProduct

    [HttpGet]
    public async Task<IActionResult> GetProduct([FromQuery] Guid id)
    {
        var getProductCmd = new GetProductQuery(id);
        var getProductResult = await _mediator.Send(getProductCmd);
        
        var getSubgroupCmd = new GetSubgroupQuery(getProductResult.Value.SubgroupId);
        var getSubgroupResult = await _mediator.Send(getSubgroupCmd);

        SubgroupResponse exitingSubgroup = new SubgroupResponse(
            id: getSubgroupResult.Value.Id,
            name: getSubgroupResult.Value.Name,
            groupId: getSubgroupResult.Value.GroupId);

        return getProductResult.Match<IActionResult>(
            product => Ok(new ProductResponse
            (productId: product.Id,
                name: product.Name,
                price: product.Price,
                shortDesc: product.ShortDesc,
                fullDesc: product.FullDesc,
                allowUserComments: product.AllowUserComments,
                isDelete: product.IsDelete,
                subgroupId: exitingSubgroup,
                createDate: product.CreateDate,
                modifiedDate: product.ModifiedDate)),
            Problem);
    }

    #endregion

    #region CreateProduct

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequestDTO requestDto)
    {
        var command = new CreateProductCommand
        (name: requestDto.name,
            price: requestDto.price,
            shortDesc: requestDto.shortDesc,
            fullDesc: requestDto.fullDesc,
            allowUserComments: requestDto.allowUserComment);
        var creationResult = await _mediator.Send(command);

        return creationResult.Match<IActionResult>(
            product => CreatedAtAction(nameof(GetProduct) , new {id = product.Id} ,
                new ProductResponse
                (productId: product.Id,
                    name: product.Name,
                    price: product.Price,
                    shortDesc: product.ShortDesc,
                    fullDesc: product.FullDesc,
                    allowUserComments: product.AllowUserComments,
                    isDelete: product.IsDelete,
                    createDate: product.CreateDate,
                    modifiedDate: product.ModifiedDate)),
            Problem);
    }

    #endregion

    #region UpdateProduct

    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> PutProduct([FromRoute] Guid productId ,[FromBody] UpdateProductRequestDTO requestDto)
    {
        var command = new UpdateProductCommand
            (id : productId,
                name: requestDto.Name,
                price: requestDto.Price,
                shortDesc: requestDto.ShortDesc,
                fullDesc: requestDto.FullDesc,
                allowUserComments: requestDto.AllowUserComments);
        
        var updateProductResult = await _mediator.Send(command);
        
        return updateProductResult.Match(
            _ => NoContent(),
            Problem);
    }

    [HttpPatch("{productId:guid}")]
    public async Task<IActionResult> PatchUser([FromRoute] Guid productId , [FromBody] JsonPatchDocument<PatchUpdateProductRequestDTO> patchDocument)
    {
        var exitingProduct = await _mediator.Send(new GetProductQuery(productId));
        if (exitingProduct.IsError)
        {
            return NotFound();
        }
        
        var productToPatch = new PatchUpdateProductRequestDTO(
            Name: exitingProduct.Value.Name,
            Price: exitingProduct.Value.Price,
            ShortDesc: exitingProduct.Value.ShortDesc,
            FullDesc: exitingProduct.Value.FullDesc,
            AllowUserComments: exitingProduct.Value.AllowUserComments);
        
        patchDocument.ApplyTo(productToPatch , ModelState);

        var command = new UpdateProductCommand(
            id: productId,
            name: productToPatch.Name,
            price: productToPatch.Price,
            shortDesc: productToPatch.ShortDesc,
            fullDesc: productToPatch.FullDesc,
            allowUserComments: productToPatch.AllowUserComments);
        
        var response = await _mediator.Send(command);
        
        return response.Match(
            _ => NoContent(),
            Problem);
    }
    
    #endregion

    #region DeleteProduct

    [HttpDelete]
    public async Task<IActionResult> DeleteProduct([FromQuery] Guid id)
    {
        var command = new DeleteProductCommand(id);
        var deletePoductResult = await _mediator.Send(command);

        return deletePoductResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion
}