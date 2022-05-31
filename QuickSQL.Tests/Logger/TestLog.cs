using System;
using Serilog;

namespace QuickSQL.Tests.Logging
{
    public class TestLog
    {
        private readonly DateTime StartTime;

        public TestLog() { StartTime = DateTime.UtcNow; }

        public void StartTest()
        {
            Log.Information("Test has been run...");
        }

        public void EndTest()
        {
            Log.Information("Test completed.");
            Log.Information($"Test execution time {DateTime.UtcNow - StartTime}.");
        }

        public void WriteConnection(bool isTravis, string connection)
        {
            if (isTravis)
            {
                Log.Information("Used TravisCI connection now.");
                Log.Information($"Connection string: {connection}");
            }
            else
            {
                Log.Information("Used Local connection now.");
                Log.Information($"Connection string: {connection}");
            }
        }

        public void WriteRequest(Request request)
        {
            Log.Information($"Table name: {request.TableName}");

            if (request.SelectedColumns == null)
            {
                Log.Information($"Selected columns: null");
            }
            else if (request.SelectedColumns.Count == 0)
            {
                Log.Information($"Selected columns: Count = 0");
            }
            else
            {
                Log.Information($"Selected columns: {string.Join(", ", request.SelectedColumns)}");
            }

            if (request.WhereConditions == null)
            {
                Log.Information($"Where conditions: null");
            }
            else if (request.WhereConditions.Count == 0)
            {
                Log.Information($"Where conditions: Count = 0");
            }
            else
            {
                foreach (var cond in request.WhereConditions)
                {
                    Log.Information($"condition: {cond.ParamName} {cond.Operator} {cond.ParamValue}");
                }
            }
        }

        public void WriteResult(string expected, string result)
        {
            if (result.Equals(expected))
            {
                Log.Information($"Expected value: {expected}");
                Log.Information($"Result value: {result}");
            }
            else
            {
                Log.Fatal($"Expected value: {expected}");
                Log.Fatal($"Result value: {result}");
            }
        }
    }
}
