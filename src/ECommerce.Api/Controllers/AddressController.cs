using System.ComponentModel.DataAnnotations;
using Azure;
using ECommerce.Application.Address.Command.CreateAddress;
using ECommerce.Application.Address.Command.DeleteAddress;
using ECommerce.Application.Address.Command.UpdateAddress;
using ECommerce.Application.Address.Queries.GetAddress;
using ECommerce.Application.Address.Queries.GetAddressByUser;
using ECommerce.Application.Address.Queries.GetAddressesByUser;
using ECommerce.Contracts.Address;
using ECommerce.Domain.Address;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Route("api/[controller]/[action]")]
public class AddressController
    (IMediator _mediator) : ApiController
{
    #region GetAddress

    [HttpGet("{userId}/{addressId}")]
    public async Task<IActionResult> GetAddressByUser([FromRoute] Guid userId, [FromRoute] Guid addressId)
    {
        var command = new GetAddressByUserQuery(userId , addressId);
        var getAddressByUserResult = await _mediator.Send(command);
        
        return getAddressByUserResult.Match<IActionResult>(
            address => Ok(new AddressResponse
            (addressId: address.Id,
                userId: address.UserId,
                country: address.Country,
                city: address.City,
                street: address.Street,
                alley: address.Alley,
                plate: address.Plate)),
            Problem);
    }

    [HttpGet]
    public async Task<IActionResult> GetAddressesByUser([FromQuery] Guid userId)
    {
        var command = new GetAddressesByUserQuery(userId);
        var getAddressesByUserResult = await _mediator.Send(command);
        
        List<AddressResponse> addresses = new List<AddressResponse>();
        foreach (var address in getAddressesByUserResult.Value)
        {
            addresses.Add(new AddressResponse
            (addressId: address.Id,
                userId: address.UserId,
                country: address.Country,
                city: address.City,
                street: address.Street,
                alley: address.Alley,
                plate: address.Plate));            
        }
        
        return getAddressesByUserResult.Match<IActionResult>(
            _ => Ok(addresses),
            Problem);
    }
    
    #endregion

    #region CreateAddress

    [HttpPost]
    public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest request)
    {
        var command = new CreateAddressCommand
        (userId: request.userId,
            country: request.country,
            city: request.city,
            street: request.street,
            alley: request.alley,
            plate : request.plate);
        var createAddressResult = await _mediator.Send(command);

        return createAddressResult.Match<IActionResult>(
            address => CreatedAtAction(nameof(GetAddressByUser),
                new {userId = address.UserId, addressId = address.Id},
                new AddressResponse
                (addressId: address.Id,
                    userId: address.UserId,
                    country: address.Country,
                    city: address.City,
                    street: address.Street,
                    alley: address.Alley,
                    plate: address.Plate)),
            Problem);
    }

    #endregion

    #region UpdateAddress

    [HttpPut("{addressId:guid}")]
    public async Task<IActionResult> PutAddress([FromRoute] Guid userId, [FromBody] PutAddressRequest request)
    {
        var command = new UpdateAddressCommand
            (id: userId,
                country: request.Country,
                city: request.City,
                street: request.Street,
                alley: request.Alley,
                plate : request.Plate);
        
        var updateAddressResult = await _mediator.Send(command);

        return updateAddressResult.Match(
            _ => NoContent(),
            Problem);
    }

    public async Task<IActionResult> PatchAddress(Guid addressId, [FromBody] JsonPatchDocument<PatchAddressRequestDTO> patchDocument)
    {
        var getAddressCmd = new GetAddressQuery(addressId);
        var getAddressResult = await _mediator.Send(getAddressCmd);

        var addressToPatch = new PatchAddressRequestDTO(
            Country: getAddressResult.Value.Country,
            City: getAddressResult.Value.City,
            Street: getAddressResult.Value.Street,
            Alley: getAddressResult.Value.Alley,
            Plate: getAddressResult.Value.Plate);
        
        patchDocument.ApplyTo(addressToPatch , ModelState);

        var command = new UpdateAddressCommand(
            id: addressId,
            country: addressToPatch.Country,
            city: addressToPatch.City,
            street: addressToPatch.Street,
            alley: addressToPatch.Alley,
            plate : addressToPatch.Plate);
        var response = await _mediator.Send(command);
        
        return response.Match(_ => NoContent(), Problem);
    }

    #endregion

    #region DeleteAddress

    [HttpDelete("{userId}/{addressId}")]
    public async Task<IActionResult> DeleteAddress([FromRoute] Guid userId, [FromRoute] Guid addressId)
    {
        var command = new DeleteAddressCommand(userId, addressId);
        var deleteAddressResult = await _mediator.Send(command);

        return deleteAddressResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion

}