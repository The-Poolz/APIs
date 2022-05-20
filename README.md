# [QuickSQL](https://www.nuget.org/packages/ArdenHide.Utils.QuickSQL/1.0.0)
[![Build Status](https://app.travis-ci.com/The-Poolz/APIs.svg?token=xusbS8YxMuyCLykrBixj&branch=master)](https://app.travis-ci.com/The-Poolz/APIs)
[![codecov](https://codecov.io/gh/The-Poolz/APIs/branch/master/graph/badge.svg?token=0nHvyp3cmC)](https://codecov.io/gh/The-Poolz/APIs)
[![CodeFactor](https://www.codefactor.io/repository/github/the-poolz/apis/badge?s=740ae1e3b7dbe3f939056f89e5d009f7544c75a2)](https://www.codefactor.io/repository/github/the-poolz/apis)


By default, Entity Framework does not support the ability to dynamically select by passing a string with the table name to get a DbSet.
This is how this library came about. This library allows you to perform a SELECT query by passing `Request` object.

## Install
**Package Manager**
```
Install-Package ArdenHide.Utils.QuickSQL -Version 1.0.0
```
**.NET CLI**
```
dotnet add package ArdenHide.Utils.QuickSQL --version 1.0.0
```

You also need to install a package with your provider or implement your provider. Example for Microsoft Sql Server provider:

**Package Manager**
```
Install-Package ArdenHide.Utils.QuickSQL.MicrosoftSqlServer -Version 1.0.0
```
**.NET CLI**
```
dotnet add package ArdenHide.Utils.QuickSQL.MicrosoftSqlServer --version 1.0.0
```

## Example usage:

>Providers currently supported: [SupportedProviders](https://github.com/The-Poolz/APIs/blob/master/QuickSQL/Providers.cs)
>>Also you can add your provider, see below.

***The first step*** is to create the desired request.

```c#
using QuickSQL;

Request tokenBalances = new Request(
    "TokenBalances",
    "Token, Owner, Amount",
    new Collection<Condition>
    {
        new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" },
        new Condition { ParamName = "Name", Operator = OperatorName.Equals, ParamValue = "'Alex'" },
    });
```
**Request fields**

* `TableName` - This is a required parameter. Pass a table name from which to take data.
* `SelectedColumns` - This is a required parameter. Pass columns from which to take data.
* `WhereConditions` - Not required parameter. Enter condition for search tables. If it is a string parameter, you need to pass the condition parameter in single quotes, like `ParamValue = "'Alex'`.

***The second step***, invoke request.
```c#
using QuickSQL;
using QuickSQL.MicrosoftSqlServer;

Request tokenBalances = new Request(
    "TokenBalances",
    "Token, Owner, Amount",
    new Collection<Condition>
    {
        new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" },
        new Condition { ParamName = "Name", Operator = OperatorName.Equals, ParamValue = "'Alex'" },
    });
    
string result = QuickSql.InvokeRequest(
    tokenBalances,
    connectionString,
    new SqlDataReader(),
    new SqlQueryCreator()
);
```

## I didn't find my provider. Instructions for adding your provider

***The first step*** is to create a DataReader for your SQL provider. It is easier than it might seem, to implement your DateReader inherit the abstract class `BaseDataReader`. This abstract class have core logic for read SQL data. You need to define `CreateConnection()` and `CreateReader()` for your provider.

**Example for MySql provider**
```c#
using QuickSQL.Datareader;

public class MySqlDataReader : BaseDataReader
{
    public override DbConnection CreateConnection(string connectionString)
        => new MySqlConnection(connectionString);

    public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
        => new MySqlCommand(commandQuery, (MySqlConnection)connection).ExecuteReader();
}
```

**Example for MicrosoftSqlServer provider**
```c#
using QuickSQL.Datareader;

public class SqlDataReader : BaseDataReader
{
    public override DbConnection CreateConnection(string connectionString)
        => new SqlConnection(connectionString);

    public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
        => new SqlCommand(commandQuery, (SqlConnection)connection).ExecuteReader();
}
```

***The second step*** is to create a QueryCreator for your SQL provider. You need to define `OnCreateCommandQuery()` for your provider. This function should create a SQL query string returning data in JSON format. You can use the `CreateWhereCondition()` internal function to create the condition string.

**Example for MicrosoftSqlServer provider**
```c#
using QuickSQL.QueryCreator;

public class SqlQueryCreator : BaseQueryCreator
{
    protected override string OnCreateCommandQuery(Request request)
    {
        string commandQuery = $"SELECT {request.SelectedColumns} FROM {request.TableName}";

        if (request.WhereConditions != null)
        {
            commandQuery += $" {CreateWhereCondition(request.WhereConditions)}";
        }

        commandQuery += " FOR JSON PATH";
        return commandQuery;
    }
}
```
