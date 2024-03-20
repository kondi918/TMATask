TMA Warehouse Solution

TMAWarehouseSolution is a warehouse management application that allows users to log in, manage items in the warehouse, create purchase requests for those items, and access the admin panel. Users are divided into roles: EMP (Employee), CO (Coordinator), and ADM (Administrator).

IMPORTANT
Due to the implementation of the login system in my project, I left method that allows to initialize first users. 
To use that method at login panel you should give following details:
login: inituserstmawarehouse
password: initusershashedpasswords
That will create 3 users:
1)
Username: employee
Password: emp123
Role: EMP

2)
Username: coordinator
Password: cor123
Role: CO

3)
Username: administrator
Password: adm123
Role: ADM

Method should be deleted after initializing first users
