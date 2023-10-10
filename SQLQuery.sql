create database ProjectWork

use projectWork

create table Utenti
(
	id int primary key identity(1,1),
	nominativo varchar(50),
	amministratore bit,
	email varchar(100) unique,
	numero varchar(100) unique,
	residenza varchar(100),
	codiceFiscale varchar(100) unique,
	sezione varchar(100),
	team varchar(100),
	descrizione varchar(200),
	passwordHash varchar(64)
);

insert into Utenti
(nominativo, amministratore, email, numero, residenza, codiceFiscale, passwordHash)
values
('Lorenzo',1, 'lorenzo@example.com', '1234567890', '123 Main Street, City1', 'ABCD123EFGH56789I', HASHBYTES('Sha2_512','password123')),
('Vasile', 0,'vasile@example.com', '9876543210', '456 Elm Street, City2', 'WXYZ456JKLM78901N', HASHBYTES('Sha2_512','password123')),
('Emanuele', 0,'emanuele@example.com', '5555555555', '789 Oak Street, City3', 'PQRS6789TUVW12345X', HASHBYTES('Sha2_512','password123')),
('Shalva', 0, 'shalva@example.com', '1112223333', '101 Pine Street, City4', 'EFGH6789IJKL12345M', HASHBYTES('Sha2_512','password123')),
('Francesco', 0, 'francesco@example.com', '9998887777', '321 Cedar Street, City5', 'MNOP1234QRST56789U', HASHBYTES('Sha2_512','password123'));

select * from Utenti

drop table Utenti

create table Posts
(
	id int primary key identity(1,1),
	idUtente int,
	idPadre int,
	contenuto text,
	dataEora datetime,
	miPiace int,
	foreign key (idUtente) references Utenti(id),
	foreign key (idPadre) references Posts(id)
);

insert into Posts
(idUtente, idPadre, contenuto, dataEora, miPiace)
values
(1, 1, 'Oggi è proprio una bella giornata', '03-10-2023 12:07', 0);

select * from Posts

drop table Posts

create table Followers
(
	idUtente int,
	idFollower int,
	primary key (idUtente, idFollower),
	foreign key (idUtente) references Utenti(id),
	foreign key (idFollower) references Utenti(id)
);

insert into Followers
(idUtente, idFollower)
values
(1, 2);

select * from Followers