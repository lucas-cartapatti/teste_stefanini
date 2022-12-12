// See https://aka.ms/new-console-template for more information

using RestSharp;

var client = new RestClient("https://localhost:58837/api/Products");
client.Timeout = -1;
var request = new RestRequest(Method.POST);
request.AddHeader("Content-Type", "application/json");
var body = @"[
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Pão de Queijo"",
" + "\n" +
@"        ""valor"": 3.99
" + "\n" +
@"    },
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Pão na Chapa"",
" + "\n" +
@"        ""valor"": 2.99
" + "\n" +
@"    },
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Salgado"",
" + "\n" +
@"        ""valor"": 5.99
" + "\n" +
@"    },
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Churros"",
" + "\n" +
@"        ""valor"": 4.99
" + "\n" +
@"    },
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Bolo no Pote"",
" + "\n" +
@"        ""valor"": 7.99
" + "\n" +
@"    },    
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Café"",
" + "\n" +
@"        ""valor"": 4.99
" + "\n" +
@"    },
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Cappuccino"",
" + "\n" +
@"        ""valor"": 7.99
" + "\n" +
@"    },
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Suco Natural"",
" + "\n" +
@"        ""valor"": 5.99
" + "\n" +
@"    },    
" + "\n" +
@"    {
" + "\n" +
@"        ""nomeProduto"": ""Coca Cola Lata"",
" + "\n" +
@"        ""valor"": 4.99
" + "\n" +
@"    }
" + "\n" +
@"]";
request.AddParameter("application/json", body,  ParameterType.RequestBody);
IRestResponse response = client.Execute(request);
Console.WriteLine(response.Content);