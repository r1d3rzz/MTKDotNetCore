using MTKDotNetCore.ConsoleHttpClient;

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

Console.ReadKey();