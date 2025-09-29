using Alten.ProductMaster.SharedKirnel;

namespace Alten.ProductMasterTrial.Domain.Errors
{
    public static class DomainErrors
    {
        public static class Product
        {
            public static readonly Error NotFound = new( "Product.NotFound", "The Product was not found");

            public static readonly Error EmptyCode = new("Product.EmptyCode","The Product code is empty");

            public static readonly Error CodeAlreadyInUse = new("Product.CodelAlreadyInUse", "the product code is already in use");

            public static readonly Error EmptyName = new("Product.EmptyName", "The product name is empty");

            public static readonly Error EmptyImage = new("Product.EmptyImage", "The product image is empty");


            public static readonly Error PriceIsNegative = new("Product.PriceIsNegative", "The Product price is negative");
            
            public static readonly Error QuantityIsNegative = new("Product.QuantityIsNegative", "The Product Quantity is negative");

            public static readonly Error EmptyCategory = new("Product.EmptyCategory", "The Product category is empty");

            public static readonly Error EmptyInternalReference = new("Product.EmptyInternalReference", "the product InternalReference is empty");

            public static readonly Error InternalReferenceAlreadyInUse = new("Product.InternalReferenceAlreadyInUse", "the product InternalReference is already in use");

            public static readonly Error ShellIdIsNegative = new("Product.ShellIdIsNegative", "The Product ShellIdIsNegative is negative");
            
            public static readonly Error ShellIdAlreadyInUse = new("Product.ShellIdAlreadyInUse", "the product ShellId is already in use");

        }

        public static class UserNameErrors
        {
            public static readonly Error EmptyUserName = new("UserName.EmptyUserName", "The userName is empty");

            public static readonly Error UserNameTooLong = new("UserName.UserNameTooLong", "The userName is too long");
        }

        public static class EmailErrors
        {
            public static readonly Error EmptyEmail = new("Email.EmptyEmail", "The email is empty");

            public static readonly Error EmailTooLong = new("Email.EmailTooLong", "The email is too long");

            public static readonly Error EmailInvalidFormat = new("Email.InvalidFormat", "Email format is invalid");
        }

        public static class Member
        {
            public static readonly Error InvalidCredentials = new("Member.InvalidCredentials", "The provided credentials are invalid");

            public static readonly Error UserNameAlreadyInUse = new("Member.userNameAlreadyInUse", "The userName is already in use");
            
            public static readonly Error EmailAlreadyInUse = new("Member.EmailAlreadyInUse", "The email is already in use");

            public static readonly Error FirstNameEmpty = new("Member.EmptyFirstName", "The firstName is empty");

            public static readonly Error PasswordEmpty = new("Member.PasswordEmpty", "The password is empty");
        }
        public static class CartItemErrors
        {
            public static readonly Error EmptyProduct = new("CartItem.EmptyProduct", "The product id is empty");

            public static readonly Error NegativeQuantity = new("CartItem.NegativeQuantity", "The Product Quantity is negative");
        }

        public static class CartErrors
        {
            public static readonly Error EmptyUserId = new("Cart.EmptyUserId", "The user id is empty");

            public static readonly Error EmptyItem = new("Cart.EmptyItem", "The item is empty");

            public static readonly Error ItemNotFound = new("Cart.ItemNotFound", "The item was not found");

            public static readonly Error CartNotFound = new("Cart.CartNotFound", "The cart was not found");

            public static readonly Error ProductNotFound = new("Cart.ProductNotFound", "The product was not found");

            public static readonly Error MemberNotFound = new("Cart.MemberNotFound", "The member was not found");

            
        }

        public static class WishlistItems
        {
            public static readonly Error EmptyProductId = new("WishlistItem.EmptyProductId", "The product id is empty");
        }

        public static class WhishLists
        {
            public static readonly Error EmptyUserId= new("WishList.EmptyProductId", "The user id is empty");

            public static readonly Error WishlistItemEmpty = new("WishList.WishlistItemEmpty", "The WishlistItem  is empty");

            public static readonly Error WishlistItemAlreadyExist = new("WishList.WishlistItemAlreadyExist", "The WishlistItem already exist");

            public static readonly Error ProductNotFound = new("WishList.ProductNotFound", "The product was not found");

            public static readonly Error WishListNotFound = new("WishList.WishListNotFound", "The WishList was not found");

        }

    }
}
