using Packt.Shared;
using static System.Console;

Games games = new();

games.Setup();
bool test = games.LoginAdmin();
WriteLine(test);