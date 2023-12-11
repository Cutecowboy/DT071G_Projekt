using System.Text.Json;
using MyMLApp;
namespace Packt.Shared
{
    public class Reviews : Games
    {

        // reviews will store following variables
        // id for the review, name of the reviewer, the review, and what game id the review is on
        public record Review(int Id, string Name, string Comment, bool Sentiment, int GameId);

        public List<Review> review = [];



        public new void Setup()
        {
            // check if file exists

            if (File.Exists("review.json"))
            {
                // read the JSON file
                string jsonData = File.ReadAllText("review.json");
                // check that its not empty
                if (!string.IsNullOrEmpty(jsonData))
                {
                    // store the json data in the game variable for later usage. Is not null so ignore null warning
                    review = JsonSerializer.Deserialize<List<Review>>(jsonData)!;
                }
            }
            else  // file is does not exist
            {
                // create an emtpy json file with no data.
                File.WriteAllText("review.json", "");
            }
        }

        public void AddComment(int gameId)
        {


            // declare dummies for the input values
            bool NameChecker = false;
            bool CommentChecker = false;

            // declare empty string values 
            string NameInp = "";
            string CommentInp = "";

            // while checker false
            while (NameChecker == false)
            {
                // clear and prompt user
                Clear();
                WriteLine("Enter your username: ");
                NameInp = ReadLine()!;

                // if if input is empty
                if (string.IsNullOrEmpty(NameInp))
                {
                    // clear and error message
                    Clear();
                    WriteLine("Please enter a valid username, press any key to continue!");
                    ReadKey();

                }
                // check if input has less than 3 characters
                else if (NameInp.Length < 3)
                {
                    // clear and error message
                    Clear();
                    WriteLine("Please enter an username that is atleast three characters long, press any key to continue!");
                    ReadKey();

                }
                else // all conditions ok
                {
                    // dummy variable true to break loop
                    NameChecker = true;
                }


                // while checker false
                while (CommentChecker == false)
                {
                    // clear and prompt user
                    Clear();
                    WriteLine("Please write your review: ");
                    CommentInp = ReadLine()!;

                    // check if input is empty
                    if (string.IsNullOrEmpty(CommentInp))
                    {
                        // clear and error message
                        Clear();
                        WriteLine("Please enter a valid review, press any key to continue!");
                        ReadKey();
                    }
                    // check input has less than 3 characters
                    else if (CommentInp.Length < 3)
                    {
                        // clear and error message
                        Clear();
                        WriteLine("Please write a comment that is atleast three characters long, press any key to continue!");
                        ReadKey();
                    }
                    else // all conditions ok
                    {
                        // dummy to true, break loop
                        CommentChecker = true;
                    }

                    // double check that comments and name conditions are ok
                    if (CommentChecker && NameChecker)
                    {

                        // code integrated with AI
                        // add inputted data
                        var sampleData = new SentimentModel.ModelInput()
                        {
                            Col0 = CommentInp
                        };
                        // make a prediction based on the submitted review
                        var result = SentimentModel.Predict(sampleData);
                        // if AI gives 1, then the prediction of the review is positive return true else false 
                        var sentiment = result.PredictedLabel == 1 ? true : false;

                        // create the new review
                        review.Add(new Review(Id: PostId(), Name: NameInp, Comment: CommentInp, Sentiment: sentiment, GameId: gameId));

                        // save
                        Save();
                        // clear
                        Clear();
                        // if the review was positive
                        if (sentiment)
                        {
                            // positive review message
                            WriteLine("Your positive review has been created, press any key to continue!");
                            ReadKey();
                        }
                        else // negative
                        {
                            // negative review message
                            WriteLine("Your negative review has been created, press any key to continue!");
                            ReadKey();
                        }
                    }

                }
            }
        }

        // return an unique id for when an review is made
        public new int PostId()
        {
            if (review.Count == 0)
            {
                return 0;
            }
            else
            {
                return review.Max(t => t.Id) + 1;
            }
        }


        // delete all reviews that is connected to gameid, triggered by DeleteGame 
        public void DeleteReview(int id, bool adm)
        {
            // check if logged in, should in theory be guarded by DeleteGame
            if (adm)
            {
                int count = 0;

                // try to find the count
                try
                {
                    // find the count of the instances where gameid is equals to the id that should be deleted

                    count = review.FindAll(rev => rev.GameId == id).Count;
                }
                catch (ArgumentNullException)
                {
                    // do nothing
                    throw;
                }

                // if count greater than 0
                if (count > 0)
                {
                    // try removing 
                    try
                    {
                        // remove all instances where gameid == the id you want to remove
                        review.RemoveAll(rev => rev.GameId == id);
                        // save new review list
                        Save();
                        // write success
                        WriteLine($"{count} reviews where removed on game id: {id}, press any key to continue!");
                        ReadKey();
                    }
                    // catch error
                    catch (ArgumentNullException)
                    {
                        // write error message
                        WriteLine("Error while trying to remove the reviews, press any key to continue!");
                        ReadKey();
                    }


                }
                else // no items where removed
                {
                    // write message
                    WriteLine($"No reviews where removed on game id: {id}, press any key to continue!");
                    ReadKey();
                }

            } // no need for else here, DeleteGame will prompt error

        }

        // save new reviews to the JSON file
        public new void Save()
        {
            // if no entries, reduce bugs by replacing empty array with empty string
            if (review.Count == 0)
            {
                // empty string instead of []
                File.WriteAllText("review.json", "");
            }
            else
            {
                // Serialize the entries
                string json = JsonSerializer.Serialize(review);
                // write the json data to the json file
                File.WriteAllText("review.json", json);
            }
        }

        // get the reviews based on sentiment, takes id and which Sentiment (bool), return string
        public string GetReviewsBySentiment(int id, bool sentiment)
        {

            // declare empty string
            string message = "";
            // check if gameid == id exist and that the sentiment that was sent exist on review
            if (review.FindAll(rev => rev.GameId == id && rev.Sentiment == sentiment).Count == 0)
            {
                // if positive reviews
                if (sentiment)
                {
                    // no positives
                    message = "There are no positive reviews on this product, press any key to go back!";
                    // else no negatives
                }
                else { message = "There are no negative reviews on this product, press any key to go back!"; }
            }
            else // if there exists reviews that is on that gameid and positive/negative
            {
                // if positive
                if (sentiment)
                {
                    // write positive reviews
                    message = "Displaying positive reviews: \n";
                }
                // else write negative reviews
                else { message = "Displaying negative reviews: \n"; }
                message += String.Format("{0, -5} {1, -20} {2, -30}\n\n", "ID", "Username", "Review");
                // check whether reviews exists based on condition on that game and positive/negative review
                foreach (var i in review.FindAll(rev => rev.GameId == id && rev.Sentiment == sentiment))
                {
                    // list those reviews
                    message += String.Format("{0, -5} {1, -20} {2, -30}\n", i.Id, i.Name, i.Comment);

                }
                message += "\nPress any key to go back!";

            }
            // return message string
            return message;


        }

        // get all reviews based on id
        public string GetReviewsById(int id)
        {
            // declare empty string
            string message = "";
            // if reviews that matches the games id does not exist
            if (review.FindAll(rev => rev.GameId == id).Count == 0)
            {
                // prompt that there are no reviews
                message = "There are no reviews on this product, press any key to go back!";
            }
            // else if there are reviews on product
            else
            {
                // display the products
                message = "Displaying all reviews:\n";
                message += String.Format("{0, -5} {1, -20} {2, -30}\n\n", "ID", "Username", "Review");
                // loop each iteration that has gameid == id
                foreach (var i in review.FindAll(rev => rev.GameId == id))
                {
                    // display the id, username and review
                    message += String.Format("{0, -5} {1, -20} {2, -30}\n", i.Id, i.Name, i.Comment);
                }
                message += "\nPress any key to go back!";

            }
            // return message string
            return message;
        }

    }
}




