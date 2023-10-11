create database ProjectWork

use projectWork

DROP TABLE Followers;
DROP TABLE Likes;
DROP TABLE Posts;
DROP TABLE Utenti;

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

INSERT INTO Utenti (nominativo, amministratore, email, numero, residenza, codiceFiscale, passwordHash)
VALUES
	('Lorenzo Cuvertino',1, 'lorenzo@example.com', '1234567890', '123 Main Street, City1', 'ABCD123EFGH56789I', HASHBYTES('Sha2_512','password123')),
	('Vasile Blanaru', 1, 'vasile@example.com', '9876543210', '456 Elm Street, City2', 'WXYZ456JKLM78901N', HASHBYTES('Sha2_512','password123')),
	('Emanuele Zonetti', 0, 'emanuele@example.com', '5555555555', '789 Oak Street, City3', 'PQRS6789TUVW12345X', HASHBYTES('Sha2_512','password123')),
	('Shalva Arabuli', 0, 'shalva@example.com', '1112223333', '101 Pine Street, City4', 'EFGH6789IJKL12345M', HASHBYTES('Sha2_512','password123')),
	('Francesco Vicario', 0, 'francesco@example.com', '9998887777', '321 Cedar Street, City5', 'MNOP1234QRST56789U', HASHBYTES('Sha2_512','password123'));

select * from Utenti

create table Posts
(
	id int primary key identity(1,1),
	idUtente int,
	idPadre int,
	contenuto text,
	dataEora datetime,
	miPiace int,
	immagine VARCHAR(255),
	foreign key (idUtente) references Utenti(id),
	foreign key (idPadre) references Posts(id)
);

insert into Posts (idUtente, idPadre, contenuto, dataEora, miPiace, immagine)
values
	(1, NULL, 'Oggi è proprio una bella giornata', '03-10-2023 12:07', 1, NULL),
	(2, 1, 'Questo è un commento al primo post', '11-10-2023 14:19', 3, ''),
	(2, NULL, 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.', '11-10-2023 14:19', 14, ''),
	(3, 3, 'Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.', '11-10-2023 15:31', 2, NULL),
	(4, NULL, 'Ultricies lacus sed turpis tincidunt id aliquet risus feugiat. Nisi lacus sed viverra tellus in hac habitasse platea. Vel pharetra vel turpis nunc eget lorem dolor sed viverra.', '11-10-2023 16:54', 9, NULL),
	(5, NULL, 'Risus quis varius quam quisque. Nisl pretium fusce id velit. Dolor morbi non arcu risus quis. Fames ac turpis egestas maecenas pharetra convallis posuere.', '11-10-2023 17:04', 0, NULL);

select * from Posts

CREATE TABLE Likes (
	idUtente INT,
	idPost INT,
	
	PRIMARY KEY (idUtente, idPost),
	FOREIGN KEY (idUtente) REFERENCES Utenti(id),
	FOREIGN KEY (idPost) REFERENCES Posts(id)
);

INSERT INTO Likes (idUtente, idPost)
VALUES (2, 1), (2, 4), (2, 5);

create table Followers
(
	idUtente int,
	idFollower int,
	primary key (idUtente, idFollower),
	foreign key (idUtente) references Utenti(id),
	foreign key (idFollower) references Utenti(id)
);

insert into Followers (idUtente, idFollower)
values (1, 2);

select * from Followers