# StoreManagementWepApp
 
Само приложение - [http://95.163.241.37/](http://95.163.241.37/)

Web api  - [http://95.163.241.37:5000/swagger/index.html](http://95.163.241.37:5000/swagger/index.html)

Все развернуто через контейнеры docker в linux

Для того чтобы запустить локально:

без docker:

в свойствах решения, зайти в свойства, выбрать пункт Multiple startup project и выбрать все доступные web api. Подключение к БД идет на тестовый сервер, поэтому должны сразу появиться существующие магазины

с docker:

запустить решения из Visual Studio через docker