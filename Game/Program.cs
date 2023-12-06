using Packt.Shared;
using static System.Console;

Games games = new();

games.Setup();

// serve as a control variable when the admin is logged in.
games.LoginAdmin();
WriteLine(games.admin);

games.LogoutAdmin();
WriteLine(games.admin);