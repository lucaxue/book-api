CREATE TABLE Books
(
  Id SERIAL PRIMARY KEY,
  Title TEXT,
  Author TEXT
);

INSERT INTO Books 
  (Title, Author)
VALUES 
  ('Code', 'Charles Petzold'),
  ('Homo Deus', 'Yuval Noah Harari'),
  ('A game of thrones', 'George R.R. Martin');

