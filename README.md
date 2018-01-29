# Bangazon ASP.NET API

This is an ASP.NET web API to perform all CRUD operations for Bangazon, Inc. Bangazon API is set up to build employees, training programs, computers, customers, products, etc. 

# Routes and Methods
- Computer
purpose: add/update/delete for Computer
methods: 
    GET list of all Computers
    GET single Computer: `api/computer/[ComputerId]`
    POST a new Computer or assign a computer to an employee: `api/comupter/employee`
    PUT change information on a Computer `api/computer/[ComputerId]`
    DELETE a Computer  `api/computer/[ComputerId]` 

- Customer
purpose: add/update/get customers
methods: 
    GET list of all customers `api/customer`
    GET`{?active=false}` list of inactive customers(customers who haven't placed an order) 
    GET{id} get a single customer `api/customer/{id}`
    POST new a new customer `api/customer`
    PUT change information on a customer `api/customer/{id}`

- Department
purpose: add/update/delete for Department
methods: 
    GET list of all Departments `api/department`
    GET single Department `api/department/[DepartmentId]`
    POST a new Department `api/department`
    PUT change information on a Department `api/department/[DepartmentId]`

- Employee
purpose: add/update/get employees
methods: 
    GET list of all employees `api/employee`
    GET{id} get a single employee `api/employee/[employeeId]`
    POST new a new employee `api/employee`
    PUT change information on a customer `api/employee/[employeeId]`

- PaymentType
purpose: add/update/get payment types
methods: 
    GET list of all employees `api/paymentType`
    GET{id} get a single paymentType `api/paymentType/[paymentTypeId]`
    POST new a new paymentType `api/paymentType`
    PUT change information on a customer `api/paymentType/[paymentTypeId]`

- Product
purpose: add/update/delete a product  for customers
methods: 
    GET list of all product `api/product`
    GET single product `api/product/[ProductId]`
    POST new a new product `api/product`
    PUT change information on a product `api/ProductType/[ProductId]`
    DELETE a product `api/Product/[ProductId]`

- ProductType
purpose: add/update/delete a product type for customers
methods: 
    GET list of all product types `api/productType`
    GET single product type `api/productType/[ProductTypeId]`
    POST new a new product type `api/productType`
    PUT change information on a product type `api/productType/[ProductTypeId]`
    DELETE a product type `api/productType/[ProductTypeId]`

- ShoppingCart
purpose: add/update/delete for Shopping Carts
methods: 
    GET list of all Shopping Carts `api/ShoppingCart`
    GET single Shopping Cart `api/ShoppingCart/[ShoppingCartId]`
    POST a new Shopping Cart or add a product to a Shopping Cart `api/ShoppingCart`
    PUT change information on a Shopping Cart `api/ShoppingCart/[ShoppingCartId]`
    DELETE a Shopping Cart `api/ShoppingCart/[ShoppingCartId]`

- TrainingProgram
purpose: add/update/delete for Training Program
methods: 
    GET list of all Training Programs `api/TrainingProgram`
    GET single Training Program `api/TrainingProgram/[TrainingProgramId]`
    POST a new Training Program or add a product to a Training Program `api/TrainingProgram`
    PUT change information on a Training Program `api/TrainingProgram/[TrainingProgramId]`
    DELETE a Training Program `api/TrainingProgram/[TrainingProgramId]`


# Install code
- DB file
Create a .db file in your code editor

# System configuration
- zshrc
In CLI: `code ~/.zshrc`
Add the following line of code to your zshrc file: 
`export: BANGAZON="PATH TO YOUR DATABASE FILE"`
BANGAZON is already set up in the `Startup.cs` file
- Migration
To access database, in CLI: `dotnet ef database update`