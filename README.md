# [QuickSQL](https://www.nuget.org/packages/ArdenHide.Utils.QuickSQL/1.0.0)
[![Build Status](https://app.travis-ci.com/The-Poolz/APIs.svg?token=xusbS8YxMuyCLykrBixj&branch=master)](https://app.travis-ci.com/The-Poolz/APIs)
[![codecov](https://codecov.io/gh/The-Poolz/APIs/branch/master/graph/badge.svg?token=0nHvyp3cmC)](https://codecov.io/gh/The-Poolz/APIs)
[![CodeFactor](https://www.codefactor.io/repository/github/the-poolz/apis/badge?s=740ae1e3b7dbe3f939056f89e5d009f7544c75a2)](https://www.codefactor.io/repository/github/the-poolz/apis)



By default, Entity Framework does not support the ability to dynamically select by passing a string with the table name to get a DbSet.
This is how this library came about. This library allows you to perform a SELECT query by passing `Request` object.

## Install

## Example usage:

>Providers currently supported: [SupportedProviders](https://github.com/The-Poolz/APIs/blob/master/QuickSQL/Providers.cs)
>>Also you can add your provider, see below.

***The first step*** is to create the desired request.

```c#
Request tokenBalances = new Request
{
    TableName = "TokenBalances",
    SelectedColumns = "Token, Owner, Amount",
    WhereCondition = "Id = 1, Token = 'ARD'"
};
```
**Request fields**

* `TableName` - This is a required parameter. Pass a table name from which to take data.
* `SelectedColumns` - This is a required parameter. Pass columns from which to take data.
* `WhereCondition` - Not required parameter. Enter condition for search tables. If it is a string parameter, you need to pass the condition parameter in single quotes, like `TableName.Username = 'Alex'`.

***The second step***, invoke request.
```c#
Request tokenBalances = new Request
{
    TableName = "TokenBalances",
    SelectedColumns = "Token, Owner, Amount",
    WhereCondition = "Id = 1, Token = 'ARD'"
};
string result = QuickSql.InvokeRequest(
    tokenBalances,
    connectionString,
    new MySqlDataReader(),
    new MySqlQueryCreator()
);
```

## I didn't find my provider. Instructions for adding your provider

***The first step*** is to create a DataReader for your SQL provider. It is easier than it might seem, to implement your DateReader inherit the abstract class `BaseDataReader`. This abstract class have core logic for read SQL data. You need to define `CreateConnection()` and `CreateReader()` for your provider.

**Example for MySql provider**
```c#
public class MySqlDataReader : BaseDataReader
{
    public override Providers Provider => Providers.MySql;

    public override DbConnection CreateConnection(string connectionString)
        => new MySqlConnection(connectionString);

    public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
        => new MySqlCommand(commandQuery, (MySqlConnection)connection).ExecuteReader();
}
```

**Example for MicrosoftSqlServer provider**
```c#
public class SqlDataReader : BaseDataReader
{
    public override Providers Provider => Providers.MicrosoftSqlServer;

    public override DbConnection CreateConnection(string connectionString)
        => new SqlConnection(connectionString);

    public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
        => new SqlCommand(commandQuery, (SqlConnection)connection).ExecuteReader();
}
```
Now this line does not contain logic. You just need to define Provider to match the abstract class. The selected provider is not important.
```c#
public override Providers Provider => Providers.MicrosoftSqlServer;
```


***The second step*** is to create a QueryCreator for your SQL provider. You need to define `OnCreateCommandQuery()` for your provider. This function should create a SQL query string returning data in JSON format.

**Example for MicrosoftSqlServer provider**
```c#
public class SqlQueryCreator : BaseQueryCreator
{
    public override Providers Provider => Providers.MicrosoftSqlServer;

    protected override string OnCreateCommandQuery(Request request)
    {
        string commandQuery = $"SELECT {request.SelectedColumns} FROM {request.TableName}";
        if (!string.IsNullOrEmpty(request.WhereCondition))
        {
            string condition = string.Join(" AND ", request.WhereCondition.Split(",").ToList());
            commandQuery += ($" WHERE {condition}");
        }
        commandQuery += " FOR JSON PATH";
        return commandQuery;
    }
}
```

Now this line does not contain logic. You just need to define Provider to match the abstract class. The selected provider is not important.
```c#
public override Providers Provider => Providers.MicrosoftSqlServer;
```
