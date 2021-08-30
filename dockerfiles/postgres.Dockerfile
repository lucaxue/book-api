FROM postgres:13.3

ENV PGPASSWORD=${POSTGRES_PASSWORD}

COPY ./src/Scripts/create_books_table.sql /docker-entrypoint-initdb.d/