create database ToDoAppDB

create table Category
(
Id int IDENTITY(1, 1) not null primary key,
Name varchar(20)
)

create table Task
(
Id int IDENTITY(1,1) not null primary key,
CategoryId int not null foreign key (CategoryId) 
	references category(id) on delete cascade on update no action,
Name varchar(20) not null,
Deadline datetime not null,
IsCompleted bit DEFAULT 0
)



