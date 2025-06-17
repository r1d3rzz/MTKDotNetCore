using MTKDotNetCore.ConsoleHttpClient;
using Refit;

Console.WriteLine("App is Start");

#region buildIn_HttpClient
//HttpClientExample httpClientExample = new HttpClientExample();
//await httpClientExample.ReadAsync(1111);
//await httpClientExample.ReadAsync(1);
//await httpClientExample.CreateAsync("test", false, 562);
//await httpClientExample.UpdateAsync(1,"test update 2", false, 562);
//await httpClientExample.DeleteAsync(1);
#endregion

#region HttpRestClient
//HttpRestClientExample httpRestClientExample = new HttpRestClientExample();
//await httpRestClientExample.ReadAsync(2);
//await httpRestClientExample.CreateAsync("test", false, 562);
//await httpRestClientExample.UpdateAsync(2, "test update 2", false, 562);
//await httpRestClientExample.DeleteAsync(2);
#endregion

#region Refit
RefitExample refitExample = new RefitExample();
//await refitExample.Todos("10");
//await refitExample.Todos("11");
//await refitExample.CreateTodo("12", "Test Test", false, 562);
//await refitExample.UpdateTodo("12" ,"Test Update", true, 562);
//await refitExample.DeleteTodo("12");
#endregion
Console.ReadKey();