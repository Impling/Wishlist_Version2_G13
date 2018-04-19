using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishlistManager.Models
{
    public class MessageItem
    {
        public long Id { get; set; }                //Message Id
        public long SenderId { get; set; }          //Id of user that send the message
        public long ReceiverId { get; set; }        //Id of intended user to receive message
        public bool IsInvite { get; set; }          //Bool to determine purpose of message
        public bool IsAccepted { get; set; }        //Bool to determine state of message
        public long WishlistId { get; set; }        //Id of subject wishlist
        public string Content { get; set; }         //String with content of message
        public DateTime DateCreated { get; set; }   //Date of message creation

    }

}
