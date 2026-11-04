## SQLite Browser
For this we would like you to create a small app to view and search the contents of an SQLite
database. The app should be written using C# and WPF.
The following requirements should be met:
- The app should present a data grid or list view and allow the user to page through the
`safe_hashes` table of the hashes.sqlite database. Pagination should be done using the LIMIT
OFFSET SQL syntax and display 10 records at a time. Bonus if you can allow the user to type
in the page number to view.
- The app should provide a text input for a hash and have an option to search the
`safe_hashes` table for any matching hash. A confirmation should be shown if that hash
exists. Bonus if you can allow the user to enter multiple hashes to search at once.

# References

https://aspire.dev/integrations/databases/sqlite/sqlite-host/?aspire-lang=csharp

Typed Results
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/responses?view=aspnetcore-10.0

Fluent Validation
https://docs.fluentvalidation.net/en/latest/index.html