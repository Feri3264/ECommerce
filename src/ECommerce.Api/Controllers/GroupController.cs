using System.Security.Cryptography.X509Certificates;
using ECommerce.Application.Group.Commands.AddSubgroup;
using ECommerce.Application.Group.Commands.CreateGroup;
using ECommerce.Application.Group.Commands.DeleteGroup;
using ECommerce.Application.Group.Commands.DeleteSubgroupFromGroup;
using ECommerce.Application.Group.Commands.ChangeGroupName;
using ECommerce.Application.Group.Queries.GetGroup;
using ECommerce.Application.Group.Queries.GetGroups;
using ECommerce.Application.Group.Queries.GetSubgroupsByGroup;
using ECommerce.Contracts.Group;
using ECommerce.Contracts.Subgroup;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Controllers;

[Authorize(Roles = "admin")]
[Route("api/[controller]/[action]")]
public class GroupController
    (IMediator _mediator) : ApiController
{
    
    #region GetGroup

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetGroup([FromQuery] Guid id)
    {
        var exitingGroup = new GetGroupQuery(id);
        var groupResult = await _mediator.Send(exitingGroup);

        var exitingSubgroups = new GetSubgroupsByGroupQuery(id , false , "createDate");
        var subgroupsModels = await _mediator.Send(exitingSubgroups);

        List<SubgroupResponse> subgroups = new List<SubgroupResponse>();
        if (subgroupsModels.Value is not null)
        {
            foreach (var subgroupModel in subgroupsModels.Value)
            {
                subgroups.Add(new SubgroupResponse(
                    id: subgroupModel.Id,
                    name: subgroupModel.Name,
                    groupId: id));
            }
        }

        GroupResponse response = new GroupResponse(
            groupId: id,
            name: groupResult.Value.Name,
            Subgroups: subgroups);
        
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetGroups([FromQuery] string? sort , [FromQuery] bool descending = false)
    {
        var command = new GetGroupsQuery(descending , sort);
        var getGroupsResult = await _mediator.Send(command);
        
        List<GroupResponse> groups = new List<GroupResponse>();
        foreach (var group in getGroupsResult)
        {
            groups.Add(new(group.Id, group.Name));
        }

        return Ok(groups);
    }

    [HttpGet]
    public async Task<IActionResult> GetSubgroupsByGroup([FromQuery] Guid groupId , [FromQuery] string? sort , bool descending = false)
    {
        var command = new GetSubgroupsByGroupQuery(groupId , descending , sort);
        var getSubgroupsResult = await _mediator.Send(command);
        
        List<SubgroupResponse> subgroups = new List<SubgroupResponse>();
        foreach (var subgroup in getSubgroupsResult.Value)
        {
            subgroups.Add(new (subgroup.Id, subgroup.Name , subgroup.GroupId));
        }
        
        return getSubgroupsResult.Match(
            _ => Ok(subgroups),
            Problem);
    }

    #endregion

    #region CreateGroup
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> CreateGroup([FromBody] CreateGroupRequest request)
    {
        var command = new CreateGroupCommand(request.name);
        var createGroupResult = await _mediator.Send(command);

        return createGroupResult.Match<IActionResult>(
            group => CreatedAtAction(nameof(GetGroup), new { id = group.Id }, new GroupResponse
            (groupId: group.Id,
                name: group.Name)),
            Problem);
    }

    #endregion

    #region ChangeGroupName
    [Authorize(Roles = "admin")]
    [HttpPatch("{groupId:guid}/{name}")]
    public async Task<IActionResult> ChangeGroupName([FromRoute] Guid groupId, [FromRoute] string name)
    {
        var command = new ChangeGroupNameCommand(groupId, name);
        var changeGroupResult = await _mediator.Send(command);
        
        return changeGroupResult.Match(
            _ => NoContent(),
            Problem);
    }
    
    #endregion

    #region DeleteGroup
    [Authorize(Roles = "admin")]
    [HttpDelete]
    public async Task<IActionResult> DeleteGroup([FromQuery] Guid id)
    {
        var command = new DeleteGroupCommand(id);
        var deleteGroupResult = await _mediator.Send(command);

        return deleteGroupResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }
    
    #endregion

    #region SubgroupActions
    [Authorize(Roles = "admin")]
    [HttpPost]
    public async Task<IActionResult> AddSubgroupToGroup([FromBody] AddSubgroupToGroupRequest request)
    {
        var command = new AddSubgroupToGroupCommand(request.groupId, request.subgroupId);
        var addSubgroupResult = await _mediator.Send(command);

        return addSubgroupResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }
    
    [Authorize(Roles = "admin")]
    [HttpDelete("{groupId}/{subgroupId}")]
    public async Task<IActionResult> DeleteSubgroupFromGroup([FromRoute] Guid groupId , [FromRoute] Guid subgroupId)
    {
        var command = new DeleteSubgroupFromGroupCommand(groupId, subgroupId);
        var deleteSubgroupResult = await _mediator.Send(command);
        
        return deleteSubgroupResult.Match<IActionResult>(
            _ => NoContent(),
            Problem);
    }

    #endregion

}