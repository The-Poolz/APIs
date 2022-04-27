# QuickSQL
[![Build Status](https://app.travis-ci.com/The-Poolz/APIs.svg?token=xusbS8YxMuyCLykrBixj&branch=master)](https://app.travis-ci.com/The-Poolz/APIs)
[![codecov](https://codecov.io/gh/The-Poolz/APIs/branch/master/graph/badge.svg?token=0nHvyp3cmC)](https://codecov.io/gh/The-Poolz/APIs)
[![CodeFactor](https://www.codefactor.io/repository/github/the-poolz/apis/badge?s=740ae1e3b7dbe3f939056f89e5d009f7544c75a2)](https://www.codefactor.io/repository/github/the-poolz/apis)

By default, Entity Framework does not support the ability to dynamically select by passing a string with the table name to get a DbSet.
This is how this library came about. This library allows you to perform a SELECT query by passing `Request` object.

## Install

## Example usage:

The first step is to create a DataReader for your SQL provider. It is easier than it might seem, to implement your DateReader inherit the abstract class `BaseDataReader`. This abstract class have core logic for read SQL data. You first need to specify your database provider. Next, you need to define `CreateConnection()` and `CreateReader()` for your provider.

**Example for MySql provider**
```c#
using QuickSQL.DataReader;

public class MySqlDataReader : BaseDataReader
{
    public override Providers Provider => Providers.MySql;

    public override DbConnection CreateConnection(string connectionString)
        => new MySqlConnection(connectionString);

    public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
        => new MySqlCommand(commandQuery, (MySqlConnection)connection).ExecuteReader();
}
```
**Example for Microsoft SQL Server provider**
```c#
using QuickSQL.DataReader;

public class MicrosoftSqlServerDataReader : BaseDataReader
{
    public override Providers Provider => Providers.MicrosoftSqlServer;

    public override DbConnection CreateConnection(string connectionString)
        => new SqlConnection(connectionString);

    public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
        => new SqlCommand(commandQuery, (SqlConnection)connection).ExecuteReader();
}
```

In this line you select your database provider.
```c#
public override Providers Provider => Providers.MySql;
```

>Providers currently supported: [SupportedProviders](https://github.com/The-Poolz/APIs/blob/master/QuickSQL/Providers.cs)


The second step is to create the desired request.

**Request fields**

* `TableName` - This is a required parameter. Pass a table from which to take data.
* `SelectedColumns` - This is a required parameter. Pass columns from which to take data.
* `WhereCondition` - Not required parameter. Enter condition for search tables. If it is a string parameter, you need to pass the condition parameter in single quotes, like `TableName.Username = 'Alex'`.
```c#
Request tokenBalances = new Request
{
    TableName = "TokenBalances",
    SelectedColumns = "Token, Owner, Amount",
    WhereCondition = "Id = 1"
};
```

The third step<, invoke request.
```c#
MySqlDataReader reader = new MySqlDataReader();     // Your DataReader
Request tokenBalances = new Request
{
    TableName = "TokenBalances",
    SelectedColumns = "Token, Owner, Amount",
    WhereCondition = "Id = 1"
};
QuickSql.InvokeRequest(tokenBalances, connectionString, reader);
```
