using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WishlistManager.Models;

namespace WishlistManager.Controllers
{
    [Produces("application/json")]
    [Route("api/Message")]
    public class MessageController : Controller
    {

        private readonly MessageContext _context;

        //Constructor
        public MessageController(MessageContext context)
        {
            _context = context;

            if (_context.Messages.Count() == 0) //Generate basic value incase context empty - For demonstration and testpurposes - a few things will be generated (easier to clear DB and refill with base values)
            {

                _context.SaveChanges();
            }
            /*
        public long Id { get; set; }                //Message Id
        public long SenderId { get; set; }          //Id of user that send the message
        public long ReceiverId { get; set; }        //Id of intended user to receive message
        public bool IsInvite { get; set; }          //Bool to determine purpose of message
        public bool IsAccepted { get; set; }        //Bool to determine state of message
        public long WishlistId { get; set; }        //Id of subject wishlist
        public string Content { get; set; }         //String with content of message
        public DateTime DateCreated { get; set; }   //Date of message creation
            */
        }

        //GET CALLS
        [HttpGet]
        public IEnumerable<MessageItem> GetAll()
        {
            return _context.Messages.ToList();
        }

    }
}