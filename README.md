# Query Strings

## Task 0

Play around with the search functionality of the [star wars api](https://swapi.dev/api/people/?search=sky).

## Task 1

To your books api, add the functionality so that when a request is received to `/books?search=mySearch`, all books that have `"mySearch"` in the title are returned. The search should be case insensitive and mach the middle of the words not just the beginning e.g. search=en => ben, den, end.

## Task 2

To your books api, add the functionality so that when a request is received to `/books?search=mySearch`, all books that have `"mySearch"` in either the title, or the author are returned. The search should be case insensitive and mach the middle of the words not just the beginning e.g. search=en => ben, den, end.

## Task 3

To your books api, add the functionality so that when a request is received to `/books?limit=5`, it will return the first five books. This should be able to be used in combination with task 1.

## Task 4

To your books api, add the functionality so that when a request is received to `/books?limit=5&page=2`, it will return the second five books. This should be able to be used in combination with task 1 and 2.
