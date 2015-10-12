using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Collections.Generic;

namespace Shopping
{
    [TestClass]
    public class Shopping
    {
        public Product[] shoppingCart = { new Product("milk", 4.23), new Product("water", 2.16), new Product("bread", 1.20), new Product("chocolate", 5.56) };

        [TestMethod]
        public void TestTotalShoppingCartCost()
        {
            Assert.AreEqual(13.15, CalculateTotalShoppingCartCost(shoppingCart));
        }

        [TestMethod]
        public void TestCheapestProductFromCart()
        {
            Assert.AreEqual(new Product("bread", 1.20), FindCheapestProduct(shoppingCart));
        }

        [TestMethod]
        public void TestEliminateMostExpensiveProduct()
        {
            Product[] expectedShoppingCart = { new Product("milk", 4.23), new Product("water", 2.16), new Product("bread", 1.20) };
            CollectionAssert.AreEqual(expectedShoppingCart, EliminateMostExpensiveProduct(shoppingCart));
        }

        [TestMethod]
        public void TestAddNewProductToShoppingCart()
        {
            Product newProduct = new Product("muffins", 6.23);
            Product[] expectedShoppingCart = { new Product("milk", 4.23), new Product("water", 2.16), new Product("bread", 1.20), new Product("chocolate", 5.56), newProduct };
            CollectionAssert.AreEqual(expectedShoppingCart, AddNewProductToShoppingCart(shoppingCart, newProduct));
        }

        [TestMethod]
        public void TestCalculateMediumPrice()
        {
            Assert.AreEqual(3.2875, CalculateMediumPrice(shoppingCart));
        }

        [TestMethod]
        public void TestCalculateMediumPriceWhenTheMostExpensiveProductHasBeenRemovedAndNewOneAdded()
        {
            Product newProduct = new Product("muffins", 6.23);
            Product[] shoppingCartWithoutMostExpensive = EliminateMostExpensiveProduct(shoppingCart);
            Product[] shoppingCartWithNewProduct = AddNewProductToShoppingCart(shoppingCartWithoutMostExpensive, newProduct);
            Assert.AreEqual(3.455, CalculateMediumPrice(shoppingCartWithNewProduct));
        }

        public struct Product
        {
            public string name;
            public double price;
            public Product(string name_var, double price_var)
            {
                name = name_var;
                price = price_var;
            }
        }

        public static double CalculateTotalShoppingCartCost(Product[] shoppingCart)
        {
            double totalPrice = 0;
            for (int i = 0; i < shoppingCart.Length; i++)
            {
                totalPrice += shoppingCart[i].price;
            }
            return totalPrice;
        }

        public static Product FindCheapestProduct(Product[] shoppingCart)
        {
            Product cheapestProduct = shoppingCart[0];
            for (int i = 1; i < shoppingCart.Length; i++)
            {
                if(cheapestProduct.price > shoppingCart[i].price)
                    cheapestProduct = shoppingCart[i];
            }
            return cheapestProduct;
        }

        public static Product[] EliminateMostExpensiveProduct(Product[] shoppingCart)
        {
            Product expensiveProduct = shoppingCart[0];
            for (int i = 1; i < shoppingCart.Length; i++)
            {
                if (expensiveProduct.price < shoppingCart[i].price)
                    expensiveProduct = shoppingCart[i];
            }

            shoppingCart = shoppingCart.Where(val => ((val.name != expensiveProduct.name) && (val.price != expensiveProduct.price))).ToArray();
            
            return shoppingCart;
        }

        public static Product[] AddNewProductToShoppingCart(Product[] shoppingCart, Product newProduct)
        {
            Array.Resize(ref shoppingCart, shoppingCart.Length + 1);
            shoppingCart[shoppingCart.Length - 1] = newProduct;
            return shoppingCart;
        }

        public static double CalculateMediumPrice(Product[] shoppingCart)
        {
            return  CalculateTotalShoppingCartCost(shoppingCart)/shoppingCart.Length;
        }

    }
}
