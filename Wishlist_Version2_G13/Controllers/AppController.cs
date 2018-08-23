using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist_Version2_G13.Data;
using Wishlist_Version2_G13.Models;

namespace Wishlist_Version2_G13.Controllers
{
    public class AppController
    {

        //Variable declaration and getters and setters
        public User User { get; set; }      //currently logged in user on the app
        public Wishlist SelectedWishlist { get; set; }
        public Item SelectedItem { get; set; }

        //Constructors
        #region Constructors
        public AppController() { }

        public AppController(User loggedInUser)
        {
            User = loggedInUser;
        }
        #endregion

        //Functions
        #region Methods
        public User LoginUser(string email, string password) {

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    return context.Users.First(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase));
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                
            }

            return null;

        }
        public void SetupLoggedInUser(User u) {

            User = u;

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    //Set Contact list
                    
                    User.Contacts = new ObservableCollection<User>(GetContactsByUserId(User.UserId) as List<User>);

                    //Set Wishlists
                    //Set Wishlist of favorites
                    User.Favorites = GetFavoritesByUserId(User.UserId);
                    //Reset lists
                    User.MyWishlists = new ObservableCollection<Wishlist>();
                    User.OthersWishlists = new ObservableCollection<Wishlist>();
                    User.Notifications = new ObservableCollection<Message>();
                    //Set Own Wishlists
                    foreach (Wishlist w in GetOwnedWishlistsByUserId(User.UserId)) {
                        User.MyWishlists.Add(w);
                    }
                    //Set all open wishlist of users contacts which user can participate in 
                    foreach (Wishlist w in GetOpenParticipatingWishlistsByUserId(User.UserId)) {
                        User.OthersWishlists.Add(w);
                    }
                    //Set closed wishlists user is participating with                  
                    foreach (Wishlist w in GetClosedParticipatingWishlistsByUserId(User.UserId)){
                        User.OthersWishlists.Add(w);
                    }
                    //Set closed wishlist user is capable of requesting to join
                    foreach (Wishlist w in GetClosedNonParticipatingWishlistsByUserId(User.UserId)) {
                        User.OthersWishlists.Add(w);
                    }

                    //Set Notifications
                    foreach (Message m in GetWaitingMessagesLoggedInUser()) {
                        m.Sender = GetUserById(m.IdSender);
                        User.Notifications.Add(m);
                    }
                    

                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }

        }
        public void SetupSelectedWishlist(Wishlist w) {

            SelectedWishlist = w;

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    //Set Wishlist owner
                    SelectedWishlist.Owner = GetOwnerByWishlistId(SelectedWishlist.WishlistId);
                    //Set Items of wishlist
                    SelectedWishlist.Items = new ObservableCollection<Item>();
                    SelectedWishlist.Gifts = new ObservableCollection<WishlistItem>();
                    SelectedWishlist.Buyers = new ObservableCollection<User>();

                    foreach (Item i in GetItemsByWishlistId(SelectedWishlist.WishlistId)) {
                        i.SetCategory();
                        SelectedWishlist.Items.Add(i);
                        SelectedWishlist.Gifts.Add(context.WishlistItems.FirstOrDefault(wi => wi.ItemId==i.ItemId && wi.WishlistId==SelectedWishlist.WishlistId));
                        if (i.BuyerId.GetValueOrDefault() != 0) {
                            i.Buyer = GetUserById(i.BuyerId.GetValueOrDefault());
                        }
                    }
                    
                    //Set Wishlist participants
                    foreach (User u in GetParticipantsByWishlistId(SelectedWishlist.WishlistId)) {
                        SelectedWishlist.Buyers.Add(u);
                    }



                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }


        }
        public bool CheckContactList(string email) {

            foreach (User c in User.Contacts) {
                if (c.Email.Equals(email)) {
                    return false;
                }
            }
            return true;
        }
        public void SendContactInvite(string email) {

            User Contact = GetUserByEmail(email);
            Message msg = new Message(User, Contact, true);

            MessageUser mu = new MessageUser(msg.MessageId, Contact.UserId, msg, null);//Insert id error as it also tries overwritting the user receiver, versioning error
            AddMessageUser(mu);

        }

        public void SendJoinWishlistRequest(int wishlistId)
        {
            User wishlistOwner = GetOwnerByWishlistId(wishlistId);
            Message msg = new Message(User, wishlistOwner, false, GetWishlistById(wishlistId));
            msg.RelatedWishlist = null;
            MessageUser mu = new MessageUser(msg.MessageId, wishlistOwner.UserId, msg, null);//Insert id error as it also tries overwritting the user receiver, versioning error
            AddMessageUser(mu);

        }
        public void SendJoinWishlistInvite(string email, int wishlistId)
        {
            User Contact = GetUserByEmail(email);
            Message msg = new Message(User, Contact, true, GetWishlistById(wishlistId));
            msg.RelatedWishlist = null; //Prevent id overwrite error

            MessageUser mu = new MessageUser(msg.MessageId, Contact.UserId, msg, null);//Insert id error as it also tries overwritting the user receiver, versioning error
            AddMessageUser(mu);

        }
        public void RespondToMessage(Message msg) { 

            //Message response;
            //User Receiver = GetUserById(msg.IdSender);
            //MessageUser mu;

            //update message in db, Updates IsAccepted status
            UpdateMessage(msg);

            //If Message accepted
            if (msg.IsAccepted.GetValueOrDefault())
            {
                //if wishlistid null add sender and user to usercontact in db
                if (msg.WishlistId == null)
                {
                    AddContact(msg);
                    //response = new Message(User, msg.Sender, false);
                }
                //if wishlistid not null, add related user and relatedwishlist to wishparticipant in db
                else
                {
                    //If responding to request to join
                    if (msg.MessageContent.Contains("Has invited you")) {
                        WishlistParticipant wp = new WishlistParticipant(msg.WishlistId.GetValueOrDefault(), User.UserId, null, null);  //getvalueorDefault to deal with nullability of wishlistid
                        AddParticipant(wp);
                    }
                    //if responding to request to be added to wishlist
                    else if (msg.MessageContent.Contains("Wishes to participate")) {
                        WishlistParticipant wp = new WishlistParticipant(msg.WishlistId.GetValueOrDefault(), msg.IdSender, null, null);  //getvalueorDefault to deal with nullability of wishlistid
                        AddParticipant(wp);
                    }
                }
            }

            //Update loggedinuser to shown new wishlists or messages
            SetupLoggedInUser(User);

        }
        public void AddContact(Message msg)
        {
            //Contact and user need to be set in both directions
            UserContact uc = new UserContact(User.UserId, msg.IdSender, null, null); //Keep objects null to prevent insert id error
            AddUserContact(uc);

            //User and Contact need to be set in both directions
            uc = new UserContact(msg.IdSender, User.UserId, null, null); //Keep objects null to prevent insert id error
            AddUserContact(uc);

            //Update contact list of logged in user
            User.Contacts = new ObservableCollection<User>(GetContactsByUserId(User.UserId) as List<User>);
        }
        
        //Function) AddWishlist - add wishlist to currently logged in user widouth users or items
        public void addWishlist(Wishlist w, bool isFavorite)
        {
            CreateWishlist(w, isFavorite);
            User.addWishlist(w);        //Prepare for calling UpdateUser Method
            UpdateLoggedInUser();
        }
        //Function remove wishlist
        public void RemoveWishlist(Wishlist w) {
            User.MyWishlists.Remove(w);
            DeleteWishlist(w);
        }
        public void AddItem(Item i)
        {
            WishlistItem wi = new WishlistItem(i.ItemId, SelectedWishlist.WishlistId, i, null); //Set null for wishlistobject or errors on attempted id overwrite will pop up
            AddWishlistItem(wi);
        }
        public void RemoveItem(Item i)
        {
            SelectedWishlist.Items.Remove(i);
            DeleteItem(i);
            UpdateWishlist(SelectedWishlist);
        }
        public Message CheckIfMessageExists(Message msg) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    Message message;

                    //First get all messages of receiver
                    foreach (int messageId in context.Notifications.Where(n => n.ReceiverId == msg.Receiver.ReceiverId).Select(n => n.MessageId).ToList()){
                         message = GetMessageById(messageId);
                        if (message.MessageContent.Equals(msg.MessageContent)) {
                            return message;
                        }
                    }
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
            return null;
        }
        public bool CheckIfUserParticipates(int wishlistId) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    if (context.Participants.Where(p => p.WishlistId == wishlistId).Select(p => p.ParticipantId).Contains(User.UserId)) {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return false;
            }
        }
        public bool CheckIfBought(int itemId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    int item = context.Items.FirstOrDefault(it => it.ItemId == itemId).BuyerId.GetValueOrDefault();
                    if (item != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return false;
            }
        }

        #endregion

        //DB METHODS
        #region DBmethods GET/READ
        public User GetUserById(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Users.FirstOrDefault(u => u.UserId == userId);
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public User GetUserByEmail(string email)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Users.FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.CurrentCultureIgnoreCase));
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public Wishlist GetWishlistById(int? wishlistId) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Wishlists.FirstOrDefault(w => w.WishlistId == wishlistId);
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public Wishlist GetFavoritesByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Wishlists.FirstOrDefault(w => w.WishlistId == context.OwnedWishlists.FirstOrDefault(ow => ow.IsFavorite && ow.OwnerId == userId).WishlistId);
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public List<Wishlist> GetOwnedWishlistsByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    List<int> wishlistIds = context.OwnedWishlists
                                                .Where(ow => !ow.IsFavorite && ow.OwnerId == userId)
                                                .Select(ow => ow.WishlistId).ToList();

                    List<Wishlist> wishlists = new List<Wishlist>();
                    foreach (int id in wishlistIds)
                    {
                        context.Wishlists.FirstOrDefault(w => w.WishlistId == id).SetDeadlineText();
                        wishlists.Add(context.Wishlists.FirstOrDefault(w => w.WishlistId == id));
                    }
                    return wishlists;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public List<Wishlist> GetClosedParticipatingWishlistsByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    List<Wishlist> ws = new List<Wishlist>();

                    foreach (int id in context.Participants.Where(p => p.ParticipantId == User.UserId).Select(p => p.WishlistId).ToList())
                    {
                        Wishlist wl = context.Wishlists.FirstOrDefault(w => w.WishlistId == id);
                        wl.SetDeadlineText();
                        wl.Owner = context.Users.FirstOrDefault(o => o.UserId == context.OwnedWishlists.FirstOrDefault(ow => ow.WishlistId == id).OwnerId);
                        ws.Add(wl);
                    }
                    return ws;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public List<Wishlist> GetClosedNonParticipatingWishlistsByUserId(int userId) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    List<Wishlist> ws = new List<Wishlist>();
                    List<Wishlist> wl = new List<Wishlist>();

                    foreach (int id in context.Contacts.Where(c => c.UserId == User.UserId).Select(c => c.ContactId).ToList())
                    {
                        wl = GetOwnedWishlistsByUserId(id);
                        foreach (Wishlist wishlist in wl)
                        {
                            //add all wishlists that arent open and that haven't been joined yet by logged in user
                            if ((!wishlist.IsOpen) && (GetParticipantsByWishlistId(wishlist.WishlistId).FirstOrDefault(p => p.UserId == userId) == null)) //&& GetOwnerByWishlistId(wishlist.WishlistId).UserId != User.UserId
                            {
                                wishlist.SetDeadlineText();
                                wishlist.Owner = context.Users.FirstOrDefault(o => o.UserId == context.OwnedWishlists.FirstOrDefault(ow => ow.WishlistId == wishlist.WishlistId).OwnerId);
                                ws.Add(wishlist);
                            }
                        }
                    }


                    return ws;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public List<Wishlist> GetOpenParticipatingWishlistsByUserId(int userId)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();

                    List<Wishlist> ws = new List<Wishlist>();

                    foreach (User c in User.Contacts)
                    {
                        foreach (int wi in context.OwnedWishlists.Where(ow => ow.OwnerId == c.UserId && !ow.IsFavorite).Select(wo => wo.WishlistId).ToList())
                        {
                            if (context.Wishlists.FirstOrDefault(w => w.WishlistId == wi).IsOpen)
                            {
                                Wishlist wl = context.Wishlists.FirstOrDefault(w => w.WishlistId == wi);
                                wl.SetDeadlineText();
                                wl.Owner = context.Users.FirstOrDefault(o => o.UserId == context.OwnedWishlists.FirstOrDefault(ow => ow.WishlistId == wi).OwnerId);
                                ws.Add(wl);
                            }
                        }
                    }
                    return ws;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public List<User> GetContactsByUserId(int userId)
        {

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Database.EnsureCreated();
                    return context.Contacts
                                    .Where(c => c.UserId == User.UserId)
                                    .Select(c => c.Contact)
                                    .ToList();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }

        }
        public User GetOwnerByWishlistId(int wishlistId) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    int ownerId = context.OwnedWishlists.FirstOrDefault(ow => ow.WishlistId == wishlistId).OwnerId;
                    return GetUserById(ownerId);
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public List<Item> GetItemsByWishlistId(int wishlistId) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    List<int> giftIds = new List<int>();
                    List<Item> gifts = new List<Item>();
                    giftIds = context.WishlistItems
                                        .Where(wi => wi.WishlistId == wishlistId)
                                        .Select(wi => wi.ItemId)
                                        .ToList();

                    foreach (int id in giftIds) {
                        gifts.Add(context.Items.FirstOrDefault(i => i.ItemId == id));
                    }
                    //ObservableCollection<Item> gifts = new ObservableCollection<Item>(context.Items.Where(i => w.WishlistId == wishlistId).Select(w => w.Gifts) as List<Item>);
                    return gifts;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        public List<User> GetParticipantsByWishlistId(int wishlistId) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    List< User> participants = new List<User>();

                    List<int> particpantIds = context.Participants.Where(p => p.WishlistId == wishlistId).Select(p => p.ParticipantId).ToList();
                    foreach (int id in particpantIds) {
                        participants.Add(context.Users.FirstOrDefault(u => u.UserId == id));
                    }

                    return participants;
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        //Get all messages that have not been answered yet
        public List<Message> GetWaitingMessagesLoggedInUser()
        {
            List<Message> messages = new List<Message>();

            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    //All messages where IsAccepted is null have not been responded to yet
                    //messages = context.Messages.Where(msg => msg.Receiver.UserId == User.UserId && msg.IsAccepted == null).ToList();
                    List<int> msgIds = context.Notifications.Where(n => n.ReceiverId == User.UserId).Select(n => n.MessageId).ToList();
                    foreach (int id in msgIds) {
                        Message m = context.Messages.FirstOrDefault(msg => msg.MessageId == id);
                        if (m.IsAccepted == null) {
                            messages.Add(m);
                        }
                    }
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }

            return messages;
        }
        public Message GetMessageById (int messageId) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    return context.Messages.FirstOrDefault(m => m.MessageId == messageId);
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
                return null;
            }
        }
        //Returns list of user that can be added to closed wishlist by owner
        public List<User> GetPotentialBuyers(int wishlistId) {

            List<int> participantIds = GetParticipantsByWishlistId(wishlistId).Select(p => p.UserId).ToList();

            //Returns all contacts that are not yet participating in wishlist
            return User.Contacts.Where(c => !participantIds.Contains(c.UserId)).ToList();

        }

        #endregion

        #region DBMethods POST/CREATE
        public void CreateUser(User u) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Users.Add(u);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void CreateWishlist(Wishlist w, bool favorite) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {

                    //Update joined table
                    UserWishlist uw = new UserWishlist(User.UserId, w.WishlistId, User, w, favorite);
                    context.OwnedWishlists.Add(uw);
                    w.WishlistOwner = uw;

                    context.Wishlists.Add(w);
                    context.SaveChanges();

                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void CreateItem(Item i) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {

                    context.Items.Add(i);
                    context.SaveChanges();

                    
                    //context.WishlistItems.Add(wi);
                    //context.Update(SelectedWishlist);
                    //context.SaveChanges();

                    //i.Wishlist = wi;
                    //UpdateItem(i);

                    /*
                    WishlistItem wi = new WishlistItem(i.ItemId, SelectedWishlist.WishlistId, i, SelectedWishlist);
                    context.WishlistItems.Add(wi);
                    i.Wishlist = wi;
                    // SelectedWishlist.Gifts.Add(wi);
                    //context.SaveChanges();
                    context.Items.Add(i);
                    context.Update(SelectedWishlist);
                    context.Update(wi);
                    context.SaveChanges();
                    */
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void AddWishlistItem(WishlistItem wi)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {

                    context.WishlistItems.Add(wi);
                    context.SaveChanges();

                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void AddMessageUser(MessageUser mu) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {

                    context.Notifications.Add(mu);
                    context.SaveChanges();

                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void AddUserContact(UserContact uc)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {

                    context.Contacts.Add(uc);
                    context.SaveChanges();

                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void AddParticipant(WishlistParticipant wp)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {

                    context.Participants.Add(wp);
                    context.SaveChanges();

                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void CreateMessage(Message msg) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Messages.Add(msg);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }

        #endregion

        #region DBMethods PUT/UPDATE
        public void UpdateLoggedInUser()
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Update(User);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void UpdateUser(User u) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Update(u);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }

        public void UpdateWishlist(Wishlist w) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Update(w);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }

        public void UpdateItem(Item i) {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Update(i);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        public void UpdateMessage(Message m)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    context.Update(m);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }
        #endregion

        #region DBMethods REMOVE/DELETE
        public void DeleteWishlist(Wishlist w)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {

                    UserWishlist uw = context.OwnedWishlists.FirstOrDefault(ow => ow.OwnerId == User.UserId && ow.WishlistId == w.WishlistId);
                    context.Remove(uw);
                    
                    if (GetItemsByWishlistId(w.WishlistId)!= null || GetItemsByWishlistId(w.WishlistId).Count > 0) {
                        foreach (Item i in GetItemsByWishlistId(w.WishlistId)) {
                          DeleteItem(i);
                        }
                    }
                    

                    context.Remove(w);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }

        public void DeleteItem(Item i)
        {
            try
            {
                using (WishlistDbContext context = new WishlistDbContext())
                {
                    WishlistItem wi = context.WishlistItems.FirstOrDefault(wis => wis.ItemId == i.ItemId && wis.WishlistId == SelectedWishlist.WishlistId);
                    context.Remove(wi);

                    context.Remove(i);
                    context.SaveChanges();
                }
            }
            catch (Exception eContext)
            {
                Debug.WriteLine("Exception: " + eContext.Message);
            }
        }

        #endregion

    }

}
