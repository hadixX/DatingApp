using AutoMapper;
using DatingApp.API.DTO;
using DatingApp.API.handlers;
using DatingApp.API.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DatingApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/user/{userId}/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IDatingRepository _repo;

        private readonly IMapper _mapper;

        public MessagesController(IDatingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _repo.GetMessage(id);

            if (messageFromRepo == null)
            {
                return NotFound();
            }

            return Ok(messageFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetMesaagesForUser(int userId, [FromQuery] MessageParams messageParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageParams.Id = userId;

            var messageFromRepo = await _repo.GetMessagesForUser(messageParams);

            var message = _mapper.Map<IEnumerable<MessageToReturn>>(messageFromRepo);

            Response.AddPagination(messageFromRepo.CurrentPage, messageFromRepo.PageSize, messageFromRepo.TotalCount, messageFromRepo.TotalPages);

            return Ok(message);
        }

        [HttpGet("thread/{recipiantId}")]
        public async Task<IActionResult> GetMessageThread(int userId, int recipiantId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _repo.GetMessageThread(userId, recipiantId);

            var messages = _mapper.Map<IEnumerable<MessageToReturn>>(messageFromRepo);

            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> CreatMessage(int userId, MessageForCreation messageForCreation)
        {
            var sender = await _repo.GetUser(userId);

            if (sender.id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageForCreation.SenderId = userId;

            var recipient = await _repo.GetUser(messageForCreation.RecipientId);

            if (recipient == null)
            {
                return BadRequest("Could not find user");
            }

            var message = _mapper.Map<Message>(messageForCreation);

            _repo.Add(message);

            if (await _repo.SaveAll())
            {
                var messageToReturn = _mapper.Map<MessageToReturn>(message);
                return CreatedAtRoute("GetMessage", new { userId, id = message.id }, messageToReturn);

            }
            throw new Exception("Creating the message failed on save");

        }

        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteMessage(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messsageFromRepo = await _repo.GetMessage(id);

            if (messsageFromRepo.SenderId == userId)
                messsageFromRepo.senderDelete = true;

            if (messsageFromRepo.RecipientId == userId)
                messsageFromRepo.RecipientDeleted = true;
            if (messsageFromRepo.senderDelete && messsageFromRepo.RecipientDeleted)
                _repo.Delete(messsageFromRepo);
            if (await _repo.SaveAll())
                return NoContent();
            throw new Exception("Error deleting the message");
        }

        [HttpPost("{id}/read")]
        public async Task<IActionResult> MarkMessageAsRead(int userId,int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            var message = await _repo.GetMessage(id);

            if (message.RecipientId != userId)
                return Unauthorized();

            message.IsRead = true;
            message.DateRead = DateTime.Now;

            await _repo.SaveAll();

            return NoContent();
        }
    }
}
