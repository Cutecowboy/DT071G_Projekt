using Packt.Shared;
using static System.Console;



Games games = new();
Reviews reviews = new();
games.Setup();
reviews.Setup();
// serve as a control variable when the admin is logged in.
/* games.LoginAdmin();
WriteLine(games.admin);
games.admin = true; */

WriteLine(games.PrintGames());

WriteLine(reviews.GetReviewsBySentiment(3, true));
WriteLine(reviews.GetReviewsById(2));