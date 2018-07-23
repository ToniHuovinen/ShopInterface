# ShopInterface
Shopping Interface done with Winforms and C#

This software emulates a type of shop interface you could find on any self-service cashiers. Usually they work so that you scan your products bardcodes into the system, and then the product is added to the shopping cart and eventually you pay the purchases. Since I don't have real products to sell, I created separate form which you can use to create products (you can give it: barcode, name, unit price) and you can then send that product to debug list.

From that list you can move the products into shopping cart, emulating the purchase event. Shopping cart calculates the amount of your products and total prices.

Project is now quite functional, might have few bugs but I will find those eventually

Things to add:

Validation to accept barcodes of certain length only and check if there is already a product with same barcode in the database

Maybe a search function that would allow you to fetch items directly from database using barcode. Like a real store.
