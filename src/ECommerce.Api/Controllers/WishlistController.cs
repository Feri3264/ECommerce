using ECommerce.Application.Wishlist.Commands.AddProductToWishlist;
using ECommerce.Application.Wishlist.Commands.DeleteProductFromWishlist;
using ECommerce.Application.Wishlist.Queries.GetProductsByWishlist;
using ECommerce.Application.Wishlist.Queries.GetWishlist;
using ECommerce.Contracts.Product;
using ECommerce.Contracts.Wishlist;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
public class WishlistController
    (IMediator _mediator): ApiController
{

    #region GetWishlist

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetWishlist([FromQuery] Guid id)
    {
        var exitingWishlist= new GetWishlistQuery(id);
        var wishlistResponse = await _mediator.Send(exitingWishlist);

        var exitingProducts = new GetProductsByWishlistCommand(id);
        var productsModels = await _mediator.Send(exitingProducts);

        List<ProductResponse> products = new List<ProductResponse>();
        foreach (var productModel in productsModels.Value)
        {
            products.Add(new ProductResponse(
                productId: productModel.Id,
                name: productModel.Name,
                price: productModel.Price,
                shortDesc: productModel.ShortDesc,
                fullDesc: productModel.FullDesc,
                allowUserComments: productModel.AllowUserComments,
                isDelete: productModel.IsDelete,
                createDate: productModel.CreateDate,
                modifiedDate: productModel.ModifiedDate));
        }
        
        var response = new WishlistResponse(
            id: id,
            products: products);
        
        return Ok(response);
    }

    #endregion

    #region ProductActions

    [HttpPost]
    public async Task<IActionResult> AddProductToWishlist([FromBody] AddProductToWishlistRequest request)
    {
        var command = new AddProductToWishlistCommand(request.wishlistId, request.productId);
        var addProductResult = await _mediator.Send(command);

        return addProductResult.Match<IActionResult>(
            _ => Ok(),
            Problem);
    }

    [HttpDelete("{wishlistId}/{productId}")]
    public async Task<IActionResult> RemoveProductFromWishlist([FromRoute] Guid wishlistId, [FromRoute] Guid productId)
    {
        var command = new DeleteProductFromWishlistCommand(wishlistId, productId);
        var removeProductResult = await _mediator.Send(command);

        return removeProductResult.Match<IActionResult>(
            _ => Ok(),
            Problem);
    }

    #endregion

}