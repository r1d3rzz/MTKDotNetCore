using MTKDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Console App is Start");

#region ADOExample
//ADONetExample adoNetExample = new ADONetExample();
//adoNetExample.Read();
//adoNetExample.Create();
//adoNetExample.Edit();
//adoNetExample.Delete();
#endregion

#region DapperExample

DapperExample dapperExample = new DapperExample();
//dapperExample.Read();
//dapperExample.Create();
//dapperExample.Edit();
//dapperExample.Delete();

#endregion

Console.ReadKey();