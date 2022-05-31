namespace QuickSQL.Tests.Logging
{
    public class Logger
    {
        private readonly ConsoleLog ConsoleLog;
        private readonly TestLog TestLog;
        public string TestName { get; set; }

        public Logger(string testName)
        {
            TestName = testName;

            ConsoleLog = new ConsoleLog(TestName);
            TestLog = new TestLog();
        }

        public void StartTest()
        {
            TestLog.StartTest();
            ConsoleLog.StartTest();
        }

        public void EndTest()
        {
            ConsoleLog.EndTest();
            TestLog.EndTest();
        }

        public void WriteConnection(bool isTravis, string connection)
        {
            ConsoleLog.WriteConnection(isTravis, connection);
            TestLog.WriteConnection(isTravis, connection);
        }

        public void WriteRequest(Request request)
        {
            ConsoleLog.WriteRequest(request);
            TestLog.WriteRequest(request);
        }

        public void WriteResult(string expected, string result)
        {
            ConsoleLog.WriteResult(expected, result);
            TestLog.WriteResult(expected, result);
        }
    }
}
