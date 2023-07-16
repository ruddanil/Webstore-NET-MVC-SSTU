# Webstore-NET-MVC-SSTU
A web application developed based on a three-tier architecture and the Model-View-Controller (MVC) design pattern. It utilizes the MS SQL DBMS.

Description of the implemented functionality:

1. The presentation of a list of products with their descriptions and prices to the customer has been implemented. The product information is retrieved from the database. The database also stores the quantity of available products in stock.
2. Customers have the ability to add selected items to the shopping cart, indicating the quantity of each item. Checking the availability of products in stock has been implemented. If a customer requests more items than are available in stock, the system prohibits them from doing so. The shopping cart is not saved in the database, so when users revisit the website, their cart will be empty.
3. The ability for users to place orders has been implemented. All necessary information (full name, phone number, email) is saved in the database, along with information about the placed orders. A user can place multiple orders, and within each order, they can select multiple items.
4. The system allows administrators to log in with their username and password and view the list of saved orders.
5. The functionality for administrators to add new products to the database and modify the quantity of existing products has also been implemented.

![image](https://github.com/ruddanil/Webstore-NET-SSTU/assets/25799951/68625907-e143-4eb1-9b21-e287cdea798e)

Project structure:

![image](https://github.com/ruddanil/Webstore-NET-SSTU/assets/25799951/b5c0cbde-1a8f-4930-9227-3b99f2f18721) ![image](https://github.com/ruddanil/Webstore-NET-SSTU/assets/25799951/5abffe15-0425-4869-bff9-bf11ac8eef5c)




