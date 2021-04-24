# Dapper Npgsql ASP.NET Core API

## A simple REST API for a books table.

### `/books`

- Get all books
- Get book by id
- Post book
- Update book
- Delete book
- Search books by title or author - case insensitive, and any part of the word
  - `/books?search=code`
- Limit books returned
  - `/books?limit=5`
- Paginate books returned, the limit is the amount of books per page
  - `/books?limit=5&page=2`


<br/>
<br/>
<details>
  <summary>Click to see how to setup this API</summary>  

  - Clone this repository
  - <details>
      <summary>Make a Heroku App with the Heroku Postgres add-on</summary>
      <p>
    
      - Login to your Heroku account and go to your [apps](https://dashboard.heroku.com/apps)
      - Create a new app
      - Go to Resources and add the add-on called ```Heroku Postgres```
    
      </p>
    </details>
    
    <details>
      <summary>Connect to the database with its credentials</summary>
      <p>
    
      - Go to your Heroku app then navigate to your database credentials
        <br/>
        
        ```Resources > Heroku Postgres add-on > Settings > Database Credentials > View Credentials```
      - If you're using the psql CLI to connect to the database, you can do so with its URI.

        ```
        psql <URI>
        ```
    
      </p>
    </details>
    
    <details>
      <summary>Create and populate tables in the database</summary>
      <p>
    
      - Use `src/Scripts/createTable.sql` to create and populate the tables.
      - If you're using psql, while connected to the database, you can simply copy and paste the script.
    
      </p>
    </details>
    
    <details>
      <summary>Add the user secrets for the database credentials</summary>
      <p>
      
      - Run these commands in your terminal with the correct credentials
    
        ```
        cd src
        ```
        ```
        dotnet user-secrets set "PGHOST" "<Host>"
        dotnet user-secrets set "PGDATABASE" "<Database>"
        dotnet user-secrets set "PGUSER" "<User>"
        dotnet user-secrets set "PGPORT" "<Port>"
        dotnet user-secrets set "PGPASSWORD" "<Password>"
        ```
    
      </p>
    </details>
  - Use ```dotnet run``` and you're good to go!

</details>