CREATE TABLE Books
(Id SERIAL PRIMARY KEY,
Title TEXT,
Author TEXT
);

INSERT INTO Books (Title, Author)
VALUES ('Harry Potter', 'JK Rowlings'),
('Code', 'Charles Petzold'),
('Homo Deus', 'Yuval Noah Harari'),
('A game of thrones', 'George R.R. Martin');

