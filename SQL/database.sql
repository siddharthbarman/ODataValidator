create table Book
(
	Id int not null primary key identity(1, 1),
	Author varchar(256) not null,
	Price money not null,
	Title varchar(256) not null,
	PublishYear int not null
);

insert into Book(Author, Price, Title, PublishYear) values('Stephen King', 25, 'IT', 1986);
insert into Book(Author, Price, Title, PublishYear) values('Isaac Asimov', 15, 'I, Robot', 1950);
insert into Book(Author, Price, Title, PublishYear) values('Isaac Asimov', 15, 'Robot Dreams', 1986);
insert into Book(Author, Price, Title, PublishYear) values('Yuval Harai', 45, 'Homo Deus', 2016);
insert into Book(Author, Price, Title, PublishYear) values('Linus Torvalds', 25, 'Just for fun', 2002);

	