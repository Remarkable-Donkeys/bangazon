# Bangazon ASP.NET API

This is an ASP.NET web API to perform all CRUD operations for Bangazon, Inc. Bangazon API is set up to build employees, training programs, computers, customers, products, etc. 

# Routes and Methods
1. Computer
purpose: add/update/delete for Computer
methods: 
    - GET list of all Computers
    - GET single Computer: `api/computer/[ComputerId]`
    - POST a new Computer or assign a computer to an employee: `api/comupter/employee`
    - PUT change information on a Computer `api/computer/[ComputerId]`
    - DELETE a Computer  `api/computer/[ComputerId]` 

2. Customer
purpose: add/update/get customers
methods: 
    - GET list of all customers `api/customer`
    - GET`{?active=false}` list of inactive customers(customers who haven't placed an order) 
    - GET{id} get a single customer `api/customer/{id}`
    - POST new a new customer `api/customer`
    - PUT change information on a customer `api/customer/{id}`

3. Department
purpose: add/update/delete for Department
methods: 
    - GET list of all Departments `api/department`
    - GET single Department `api/department/[DepartmentId]`
    - POST a new Department `api/department`
    - PUT change information on a Department `api/department/[DepartmentId]`

4. Employee
purpose: add/update/get employees
methods: 
    - GET list of all employees `api/employee`
    - GET{id} get a single employee `api/employee/[employeeId]`
    - POST new a new employee `api/employee`
    - PUT change information on a customer `api/employee/[employeeId]`

5. PaymentType
purpose: add/update/get payment types
methods: 
    - GET list of all employees `api/paymentType`
    - GET{id} get a single paymentType `api/paymentType/[paymentTypeId]`
    - POST new a new paymentType `api/paymentType`
    - PUT change information on a customer `api/paymentType/[paymentTypeId]`

6. Product
purpose: add/update/delete a product  for customers
methods: 
    - GET list of all product `api/product`
    - GET single product `api/product/[ProductId]`
    - POST new a new product `api/product`
    - PUT change information on a product `api/ProductType/[ProductId]`
    - DELETE a product `api/Product/[ProductId]`

7. ProductType
purpose: add/update/delete a product type for customers
methods: 
    - GET list of all product types `api/productType`
    - GET single product type `api/productType/[ProductTypeId]`
    - POST new a new product type `api/productType`
    - PUT change information on a product type `api/productType/[ProductTypeId]`
    - DELETE a product type `api/productType/[ProductTypeId]`

8. ShoppingCart
purpose: add/update/delete for Shopping Carts
methods: 
    - GET list of all Shopping Carts `api/ShoppingCart`
    - GET single Shopping Cart `api/ShoppingCart/[ShoppingCartId]`
    - POST a new Shopping Cart or add a product to a Shopping Cart `api/ShoppingCart`
    - PUT change information on a Shopping Cart `api/ShoppingCart/[ShoppingCartId]`
    - DELETE a Shopping Cart `api/ShoppingCart/[ShoppingCartId]`

9. TrainingProgram
purpose: add/update/delete for Training Program
methods: 
    - GET list of all Training Programs `api/TrainingProgram`
    - GET single Training Program `api/TrainingProgram/[TrainingProgramId]`
    - POST a new Training Program or add a product to a Training Program `api/TrainingProgram`
    - PUT change information on a Training Program `api/TrainingProgram/[TrainingProgramId]`
    - DELETE a Training Program `api/TrainingProgram/[TrainingProgramId]`


# Installation
- DB file
Create a .db file

# System configuration
Windows users: 
1. Setting up environment
    1. Go to environment variable settings
    2. Add BANGAZON variable that is = to db path
2. Add bangazon.com and www.bangazon.com as local host aliases
    1. Press the Windows key on your keyboard, type Notepad, but do NOT press Enter.
    2. Right click on Notepad and choose Run as Administrator.
    3. Log in (or have a someone with admin credentials log in).
    4. Click File > Open.
    5. Navigate to C:\Windows\System32\drivers\etc.
    6. In the lower right corner of the Open dialog box, change the Text Documents (*.txt) to All Files.
    7. Double click on hosts.

Mac users: 
1. Setting up the environment
    `export BANGAZON="PATH/TO/DB/FILE"`
2. Add bangazon.com and www.bangazon.com as local host aliases
    1. `sudo nano /etc/hosts`
    2. under `127.0.0.1 localhost` add `127.0.0.1 bangazon.com` and then `127.0.0.1 www.bangazon.com`
    *make sure everything lines up so they look like the aliases that are already in your host file
    3. exit the file and confirm changes

Both Windows and Mac users:
1. Migration
    To access database, in CLI: `dotnet ef database update`
2. Dependencies: Using dotnet version 2.1.3
3. To test to make sure that CORS is properly working follow the below steps:
    1. Outside of your Bangazon directory, download the test files for CORS `git clone https://github.com/Remarkable-Donkeys/testingCors`
    2. Have two terminal windows open.
    3. have bangazon open and run `dotnet run` in one
    4. have your test folder open and run `http-server` in the other
    5. In your browser, open dev tools and set the url as bangazon.com:8080
    6. To see if the request was successful, in Dev Tools: go to the Network tab, click customer and the preview. Customer information from your database should appear.


# Releases
V1: January 29, 2018

