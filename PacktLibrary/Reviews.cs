using System.Text.Json;
using MyMLApp;
namespace Packt.Shared
{
    public class Reviews
    {

        // reviews will store following variables
        // id for the review, name of the reviewer, the review, and what game id the review is on
        public record Review(int Id, string Name, string Comment, bool Sentiment, int GameId);

        public List<Review> review = [];



        public void Setup()
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

            bool ExitMenu = false;
            // while checker false
            while (NameChecker == false)
            {
                // clear and prompt user
                Clear();
                WriteLine("Enter your username (X to exit): ");
                NameInp = ReadLine()!;

                // if if input is empty
                if (string.IsNullOrEmpty(NameInp))
                {
                    // clear and error message
                    Clear();
                    WriteLine("Please enter a valid username, press any key to continue!");
                    ReadKey();

                }
                else if (NameInp.ToUpper() == "X")
                {
                    ExitMenu = true;
                    break;
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
            }


            // while checker false
            while (CommentChecker == false)
            {
                if (ExitMenu) { break; }
                // clear and prompt user
                Clear();
                WriteLine("Please write your review (X to exit): ");
                CommentInp = ReadLine()!;

                // check if input is empty
                if (string.IsNullOrEmpty(CommentInp))
                {
                    // clear and error message
                    Clear();
                    WriteLine("Please enter a valid review, press any key to continue!");
                    ReadKey();
                }
                else if (CommentInp.ToUpper() == "X")
                {
                    ExitMenu = true;
                    break;
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
            else if (ExitMenu)
            {
                WriteLine("You have chosen not to create your review, press any key to continue!");
                ReadKey();
            }

        }

        // return an unique id for when an review is made
        public int PostId()
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


        // count the number of reviews in a gameid, return boolean
        public bool ReviewCounter(int id, bool adm)
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
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
        // remove review based on review id
        public void DeleteReviewById(int id, bool adm)
        {
            // check if logged in, should in theory be guarded by DeleteGame
            if (adm)
            {
                int count = 0;

                // try to find the count
                try
                {
                    // find the count of the instances where gameid is equals to the id that should be deleted

                    count = review.FindAll(rev => rev.Id == id).Count;
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
                        review.RemoveAll(rev => rev.Id == id);
                        // save new review list
                        Save();
                        // write success
                        WriteLine($"Review {id} was successfully removed, press any key to continue!");
                        ReadKey();
                    }
                    // catch error
                    catch (ArgumentNullException)
                    {
                        // write error message
                        WriteLine("Error while trying to remove the review, press any key to continue!");
                        ReadKey();
                    }


                }
                else // no items where removed
                {
                    // write message
                    WriteLine($"No reviews where found on id: {id}, press any key to continue!");
                    ReadKey();
                }

            }
            else
            {
                WriteLine("Unable to delete reviews without logging in as admin, press any key to continue!");
                ReadKey();
            }

        }

        // save new reviews to the JSON file
        public void Save()
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
        public string GetReviewsBySentiment(int id, bool sentiment, bool adm)
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
                if (!adm)
                {
                    message += "\nPress any key to go back!";
                }
            }
            // return message string
            return message;


        }

        // get list of valid id's on reviews based on id
        public List<int> GetReviewListById(int id)
        {
            List<int> list = [];
            if (review.FindAll(rev => rev.GameId == id).Count > 0)
            {
                foreach (var i in review.FindAll(rev => rev.GameId == id))
                {
                    list.Add(i.Id);
                }
            }
            return list;
        }

        // get all reviews based on gameid
        public string GetReviewsById(int id, bool adm)
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
                message += String.Format("{0, -5} {1, -20} {2, -50} {3, -30}\n\n", "ID", "Username", "Review", "Sentiment");
                // loop each iteration that has gameid == id
                foreach (var i in review.FindAll(rev => rev.GameId == id))
                {
                    // display the id, username and review
                    message += String.Format("{0, -5} {1, -20} {2, -50} {3, -30}\n", i.Id, i.Name, i.Comment, i.Sentiment == true ? "Positive" : "Negative");
                }
                if (!adm)
                {
                    message += "\nPress any key to go back!";
                }
            }
            // return message string
            return message;
        }

    }
}




