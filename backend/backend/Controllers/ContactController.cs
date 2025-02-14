using backend.Application.Services;
using backend.Contracts;
using backend.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace backend.Controllers
{
    [ApiController]
    [Route("/Contacts")]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ContactResponse>>> ContactGetAll()
        {
            var contacts = await _contactService.ContactGetAll();

            var responce = contacts.Select(x => new ContactResponse(x.Id, x.Name, x.Number, x.Description));

            return Ok(responce);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> ContactCreate([FromBody] ContactRequest request)
        {
            var (contact, error) = Contact.Create(request.Name, request.Number, request.Description);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var contactId = await _contactService.ContactCreate(contact);

            return Ok(contactId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> ContactUpdate(Guid id, [FromBody] ContactRequest request)
        {
            var contactId = await _contactService.ContactUpdate(id, request.Name, request.Number, request.Description);

            return Ok(contactId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> ContactDelete(Guid id)
        {
            var contactId = await _contactService.ContactDelete(id);

            return Ok(contactId);
        }
    }
}
