using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.API.Extensions;
using Shop.Application.Extensions;
using Shop.Core.Entities;
using Shop.Core.HelperTypes;
using Shop.Core.Interfaces;
using Shop.Shared.Dtos;

namespace Shop.API.Controllers;

[Authorize]
public class MessagesController : BaseApiController
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IMapper _mapper;

    public MessagesController(IUserRepository userRepository, IMessageRepository messageRepository, IMapper mapper)
    {
        _mapper = mapper;
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        var username = User.GetUserName();

        if (username == createMessageDto.RecipientUsername.ToLower())
            return BadRequest("You cannot send messages to yourself");

        var sender = await _userRepository.GetUserByUsernameAsync(username);
        var recipient = await _userRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);

        if (recipient == null)
            return NotFound();

        var message = new Message
        {
            Sender = sender,
            Recipient = recipient,
            SenderUsername = sender.UserName,
            RecipientUsername = recipient.UserName,
            Content = createMessageDto.Content
        };

        _messageRepository.AddMessage(message);

        if (await _messageRepository.SaveAllAsync())
            return Ok(_mapper.Map<MessageDto>(message));

        return BadRequest("Failed to send message");
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser([FromQuery]
            MessageParams messageParams)
    {
        messageParams.Username = User.GetUserName();

        var messages = await _messageRepository.GetMessagesForUser(messageParams);

        Response.AddPaginationHeader(messages.PageIndex, messages.PageSize, messages.TotalCount, messages.TotalPages);

        return messages;
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        var username = User.GetUserName();

        var message = await _messageRepository.GetMessage(id);

        if (message.Sender.UserName != username && message.Recipient.UserName != username)
            return Unauthorized();

        if (message.Sender.UserName == username)
            message.SenderDeleted = true;

        if (message.Recipient.UserName == username)
            message.RecipientDeleted = true;

        if (message.SenderDeleted && message.RecipientDeleted)
            _messageRepository.DeleteMessage(message);

        if (await _messageRepository.SaveAllAsync())
            return Ok();

        return BadRequest("Problem deleting the message");
    }
}
