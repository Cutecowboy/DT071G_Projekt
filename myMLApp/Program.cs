using MyMLApp;

string review = "bad";
// add input data
var sampleData = new SentimentModel.ModelInput()
{
    Col0 = review
};
// load model and predict output of sample data
var result = SentimentModel.Predict(sampleData);

// If prediction is 1, sentiment is "Positive"; otherwise sentiment is "Negative"
var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";

Console.Clear();
// write the results of the prediction
Console.WriteLine($"Text: {sampleData.Col0}\nSentiment: {sentiment}\n");

/* Games games = new();
Reviews reviews = new();
games.Setup();
reviews.Setup();
// serve as a control variable when the admin is logged in.
games.LoginAdmin();
WriteLine(games.admin);
games.admin = true;

WriteLine(games.PrintGames());

reviews.AddComment(0); */


