CREATE DATABASE ProjectWork

USE ProjectWork

CREATE TABLE Utenti
(
	id INT PRIMARY KEY IDENTITY(1,1),
	nominativo VARCHAR(50),
	amministratore BIT,
	email VARCHAR(100) UNIQUE,
	numero VARCHAR(100) UNIQUE,
	residenza VARCHAR(100),
	codiceFiscale VARCHAR(100) UNIQUE,
	passwordHash VARCHAR(64)
);

INSERT INTO Utenti
(nominativo, amministratore, email, numero, residenza, codiceFiscale, passwordHash)
VALUES
('Lorenzo',1, 'lorenzo@example.com', '1234567890', '123 Main Street, City1', 'ABCD123EFGH56789I', HASHBYTES('Sha2_512','password123')),
('Vasile', 0,'vasile@example.com', '9876543210', '456 Elm Street, City2', 'WXYZ456JKLM78901N', HASHBYTES('Sha2_512','password123')),
('Emanuele', 0,'emanuele@example.com', '5555555555', '789 Oak Street, City3', 'PQRS6789TUVW12345X', HASHBYTES('Sha2_512','password123')),
('Shalva', 0, 'shalva@example.com', '1112223333', '101 Pine Street, City4', 'EFGH6789IJKL12345M', HASHBYTES('Sha2_512','password123')),
('Francesco', 0, 'francesco@example.com', '9998887777', '321 Cedar Street, City5', 'MNOP1234QRST56789U', HASHBYTES('Sha2_512','password123'));

SELECT * FROM Utenti
