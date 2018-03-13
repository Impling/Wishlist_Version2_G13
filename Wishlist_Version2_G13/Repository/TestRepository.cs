using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wishlist_Version2_G13.Models;

namespace Wishlist_Version2_G13.Repository
{
    public class TestRepository
    {
        public static List<User> Users { get; set; }

        public TestRepository()
        {

            Users = new List<User>();

            User UserOne = new User("Timo", "Spanhove", "timo.spanhove@hotmail.com");
            User UserTwo = new User("Sander", "De Sutter", "Sander.desutter@hotmail.com");
            User UserThree = new User("Victor", "Van Weyenberg", "Vic.VW@hotmail.com");

            UserOne.UserId = 1;
            UserTwo.UserId = 2;
            UserThree.UserId = 3;




            Wishlist wishlist1 = new Wishlist(UserOne, "Fuck da police", "Kerstmis", new DateTime(2018, 2, 28));
            Wishlist wishlist2 = new Wishlist(UserTwo, "Gimme stuff", "Birthday", new DateTime(2017, 11, 25));
            Wishlist wishlist3 = new Wishlist(UserTwo, "Jolly fat man", "sinterklaas", new DateTime(2017, 12, 25));
            Wishlist wishlist4 = new Wishlist(UserOne, "Nihilism", "barbecue", new DateTime(2019, 12, 30));
            wishlist3.IsOpen = false;
            wishlist2.IsOpen = true;

            wishlist1.addBuyer(UserThree);
            wishlist2.addBuyer(UserThree);
            wishlist3.addBuyer(UserOne);

            Item gift1 = new Item("Gameboy", Category.Tech, "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.");
            Item gift2 = new Item("Lawn mower", Category.Garden, "Why the hell not");
            Item gift3 = new Item("50 shades of gray", Category.Literature, "Because I hate myself");
            Item gift4 = new Item("The illegalest of drugs", Category.Consumable, "Wouldn't be a party otherwise");
            gift1.Buyer = UserTwo;
            gift1.Image = "/Images/testImage.png";
            gift1.WebLink = "https://www.youtube.com/watch?v=qwykNDZBl-M";

            ObservableCollection<Item> fav1 = new ObservableCollection<Item>();
            ObservableCollection<Item> fav2 = new ObservableCollection<Item>();

            fav1.Add(gift4); fav1.Add(gift3);
            fav2.Add(gift2); fav2.Add(gift1);

            wishlist1.addItem(gift1);
            wishlist1.addItem(gift4);
            wishlist3.addItem(gift2);
            wishlist3.addItem(gift3);

            UserOne.addWishlist(wishlist1);
            UserOne.addWishlist(wishlist4);
            UserTwo.addWishlist(wishlist2);
            UserTwo.addWishlist(wishlist3);
            UserThree.addWishlist(wishlist1);
            UserThree.addWishlist(wishlist4);


            UserOne.FillFavorites(fav1);
            UserThree.FillFavorites(fav2);

            UserOne.addContact(UserThree);
            UserOne.addContact(UserTwo);
            //UserOne.addContact(UserOne);
            UserTwo.addContact(UserOne);
            UserTwo.addContact(UserThree);

            Users.Add(UserOne);
            Users.Add(UserTwo);
            Users.Add(UserThree);



        }

        public List<User> GetUsers()
        {
            return Users;
        }

    }
}
