using backend.Application.Services;
using backend.Contracts;
using backend.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/contacts")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<GetContactResponse>> ContactGetAll([FromQuery] int? page)
        {

            if (page < 0) return BadRequest("Page value must be greater than zero!");

            (List<Contact> contacts,int count) = await _contactService.ContactGetAll(GetUserId(),page??1);

            var contactsResponce = contacts.Select(x => new ContactResponse(x.Id,x.UserId,x.IsGlobal, x.Name, x.Number, x.Description));

            var responce = new GetContactResponse(contactsResponce, count);

            return Ok(responce);
        }

        [Route("search")]
        [HttpGet]
        public async Task<ActionResult<GetContactResponse>> ContactSearch([FromQuery]string? search,int? page)
        {
            if (page < 0) return BadRequest("Page value must be greater than zero!");

            (List<Contact> contacts, int count) = await _contactService.ContactSearch(GetUserId(),search, page ?? 1);

            var contactsResponce = contacts.Select(x => new ContactResponse(x.Id, x.UserId, x.IsGlobal, x.Name, x.Number, x.Description));

            var responce = new GetContactResponse(contactsResponce, count);

            return Ok(responce);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Guid>> ContactCreate([FromBody] ContactRequest request)
        {
            var contact = new Contact(new Guid(), (Guid)GetUserId()!,request.IsGlobal, request.Name, request.Number, request.Description!);

            var contactId = await _contactService.ContactCreate(contact);

            return Ok(contactId);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Guid>> ContactUpdate(Guid id, [FromBody] ContactRequest request)
        {
            var contactId = await _contactService.ContactUpdate(id, (Guid)GetUserId()!,request.IsGlobal, request.Name, request.Number, request.Description!);

            return Ok(contactId);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Guid>> ContactDelete(Guid id)
        {
            var contactId = await _contactService.ContactDelete((Guid)GetUserId()!, id);

            return Ok(contactId);
        }

        private Guid? GetUserId()
        {
            var user = HttpContext.User.Identity;

            Guid? userId = null;

            if (user is not null && user.IsAuthenticated)
            {
                string? nameIdentifier = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (nameIdentifier is not null)
                {
                    userId = new Guid(nameIdentifier);
                }
            }

            return userId;
        }
    }
}
