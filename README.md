# MarketFinance pair programming interview 

This task was assigned to me during pair programming interview at MarketFinance company for the postion of Senior Software Developer.

# Shopping Cart code kata

We’re going to see how far we can get in implementing a supermarket checkout that calculates the total price of items. 

In a normal supermarket, things are identified using Stock Keeping Units, or SKUs. In our store, we’ll use individual letters of the alphabet (A, B, C, and so on). Our goods are priced individually. Also, some items are multi priced: buy 'n' of them, and they’ll cost you 'y' pounds. For example, item ‘A’ might cost 50 pounds individually, but this week we have a special offer: buy three ‘A’s and they’ll cost you 130. 

The price and offer table:

|Item|Price|Offer     |
|----|-----|----------|
|A   |50   |3 for 130 |
|B   |30   |2 for 45  |
|C   |20   |          |
|D   |15   |          |

* Offers can be applied multiple times

Our checkout accepts items in any order, so that if we scan a B, an A, and another B, we’ll recognize the two B’s and price them at 45 (for a total price so far of 95). Example input "ACDABA".

## Getting Started

The C# interface to your checkout has been provided, you will find it when you open the solution. Here is a single test case to get you started, add any other tests you feel are needed, in any order you prefer:

``` csharp
	[DataTestMethod]
	[DataRow("A", 50)]
	[DataRow("B", 30)]
	[DataRow("C", 20)]
	[DataRow("D", 15)]
	public void Checkout_GetTotal_WhenCalledWithOneItem_ShouldReturnSingleItemPrice(string item, double price)
	{
		_sut.GetTotal(item).Should().Be(price);
	}
```

credits: adapted from Dave Thomas http://codekata.com/kata/kata09-back-to-the-checkout/

# Implementation notes from yaRus

## Major topics on which you should focus

Based on the task definition the most important things interviewer should show while working on this task:

1. Math computations in computer science:
   - float vs double vs decimal
   - math devision issues
   - computing accuracy
   - computing performance

2. Domain Driven approach
   - Defining domain model for the subject (Checkout, CartItem, ShoppingCart, ShoppingProduct)
   - SOLID principles (major focus on Single Responsibility and Inversion of Control)

3. Test Driven development
   - Sufficient test coverage
   - Unit tests for edge cases (negative testing vs positive testing)
   - Write the test first and then make it green

4. Design Patterns
   - Strategy Pattern was used in this example to implement Discounts

## Implementation flow

### Clarifying requirements

When I strated to work on the task first thing I did I read requirements and asked some questions to interviewer to clarify them.
Despite the fact that requirements seems to be straight forward it is important to show your thought process on how you can catch details.
There might be some other important questions which you should ask but I asked the following:

1. Can we apply multiple discounts to the same product or we can assume that only one discount can be available per product?

   Note: Requirements are showing that there could be only 1 discount per product and we also have a specific type of discount specified which can be called as QuantityDiscount (3 for 130).
   But we should think about future requirements for the system which most likely would request multiple discounts per product. This force us to implement Strategy design pattern for Discounts.

2. Is there any specific reason to use 'double' type as a return value for GetTotal or we can be flexible here?

   Note: Usually for money computations in C# we use Decimal time which provides better precision than double type but hit computations performance. Since we already have an interface defined
   we need to confirm that this interface can be changed to follow the best practices for math computations in C#.

3. Do we want total for the ShoppingCart be calculated and available before checkout process start or we want calculations to happen only during checkout process?

   Note: The current interface assumes that we can calculate total during checkout process (ICheckout interface already defined to get total). But there might be useful to allow user to see the
   current total of his or her cart products before checkout. The answer to this question will affect relations between our domain model entities and responsibilities of those entities.

### Define the development plan

It is important to build up a plan how you want to proceed with the task implementation. It is good to start from some simplified approach and improve it over time to cover requirements.
I decided to start from building the simple algorithm of calculating total for my shopping cart which would be just a multiplication of price and quantity of each product.
When this simple scenario will be covered I can proceed with discounts implementation.

### Defining Domain-model

If you want to follow Test-Driven Development (TDD) approach at full scale - you have to write failing test first and than make it green.
From other perspective we want to start defining primitives for our domain-model like 'Product', 'ShoppingCart', 'CartItem'.
So it is up to you to define from what you want to start. I started from defining primitives and than wrote some tests.

#### ShoppingProduct

Based on requriements a ShoppingProduct can be defined as an object which has ProductId ('A','B','C','D') and Price (50,30,20,15). 
Since the concept of SKU was defined in requirements as an individual letter of an alphabet, I decided to use 'char' type to define ProductId.
But it is important to mention that in real system the ProductId field will be either string or GUID.
since Price should store amount of money which one should pay for the product it is better to use decimal type for this field.
To follow Open-Close principle we dont want to allow changes in ShoppingProduct fields so we define ProductId and Price as read-only properies.

#### CartItem

The responsibility of this entity is to store the quantity of specific product in our shopping cart and provide an interface to increase this quantity.

#### ShoppingCart

The responsibility of this entity is to keep tracking of Products added into our track. It provide an interface to add product into the cart and retrieve all products which we already added.
Since we have unique identifier for the product we can use it as a Dictionary key to allow O(1) read time when accessing CartItem.

#### Checkout

The main responsibility of this class is to calculate the total price for products in our ShoppingCart. This class was already added when I started to work on the task.
The only thing which has to be discussed regarding this class is the type of value which is get returned from GetTotal function. As I already mentioned it was double
but it is better to use decimal for money calculations. We are using string as an import parameter to GetTotal method and our ProductId is 1 symbol of that string.
We can iterate over chars in that string and try to add it into the ShoppingCart. When all required items added, we should calculate Total by multiplying each cart item
quantity to its price.

### Some real tests

Now we are ready to define a failing test to calculate total. At this moment I realized that I dont have any component which will store all types of products
with its prices. I created IProductRepository interface for that. Also, in test project I added Stubs folder and implemented TestProductRepository which
stores values in Dictionary. In real system actual implementation will use database for that purpose but for tests we are using in-memory version.
We also have to update our Checkout component and add IProductRepository as constructor parameter so it could be injected outside using DI Container in future.
In our test class, we are going to create an instance of TestProductRepository and fill it products and prices which we have in our requirements.
When this will be done we should be able to run the test and make it green.

### Implement discounts

Now we are ready to define the second part of our requirements - discounts. In requirements we have 1 type of discounts defined - quantity discounts.

|A   |50   |3 for 130 |

Base price of 3 products 'A' is 50*3=150. But we have a discount which means that instead of paying 150, client can pay 130. This discount can be applied for every 3 items
of this product in our ShoppingCart.

First thing to do is to define a IDiscount interface. Since discount is product-based, than it should have ProductId of the product to which this discount can be applied.
Also, it should have GetDiscountedTotal method which going to return discounted total price for the item. Having this interface defined we can now implement our QuantityDiscout class.

Since total price per product may vary based on applied discounts we need to define a new class called CheckoutItem which would store a CartItem and its total price with all discounts applied.

In order to register and retrieve available discounts we are going to implement IDiscountRepository inteface and in our test project we are going to create TestDiscountRepository to use it as a stub.

Now we just need to glue everything together and define test scenarios to cover our discounts flow.

### Cover edge cases

Q: Can I break your component somehow?

A: We need to make sure that all product code which we provide as input are exist in our ProductRepository. If product is not registered we should fail with some custom exception. This scenario has to be covered.
We also need to validate if we can set our products code list to null.

Q: How to apply multiple discounts to the same product?

A: Currently we can apply multiple discounts per product but they will be applied one after another. It is better to also support another scenario when only discount could be selected per product but
the one with the biggest discount value. So we can define some kind of field which going to store discount appliement type and then define discount applier component which will handle this scenario.