using System.Text.Json;
using MyMLApp;
namespace Packt.Shared
{
    public class Reviews : Games
    {

        // reviews will store following variables
        // id for the review, name of the reviewer, the review, and what game id the review is on
        public record Review(int Id, string Name, string Comment, bool Incitement, int GameId);

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

            while (NameChecker == false)
            {
                Clear();
                WriteLine("Enter your username: ");
                NameInp = ReadLine()!;

                if (string.IsNullOrEmpty(NameInp))
                {
                    Clear();
                    WriteLine("Please enter a valid username, press any key to continue!");
                    ReadKey();

                }
                else if (NameInp.Length < 3)
                {
                    Clear();
                    WriteLine("Please enter an username that is atleast three characters long, press any key to continue!");
                    ReadKey();

                }
                else
                {
                    NameChecker = true;
                }



                while (CommentChecker == false)
                {
                    Clear();
                    WriteLine("Please write your review: ");
                    CommentInp = ReadLine()!;

                    if (string.IsNullOrEmpty(CommentInp))
                    {
                        Clear();
                        WriteLine("Please enter a valid review, press any key to continue!");
                        ReadKey();
                    }
                    else if (CommentInp.Length < 3)
                    {
                        Clear();
                        WriteLine("Please write a comment that is atleast three characters long, press any key to continue!");
                        ReadKey();
                    }
                    else
                    {
                        CommentChecker = true;
                    }

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
                        // if AI gives 1, then the prediction of the review is positive return 1 (true) else (0) false 
                        var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";
                       
                        bool incitement = sentiment == "Positive" ? true : false;

                        review.Add(new Review(Id: PostId(), Name: NameInp, Comment: CommentInp, Incitement: incitement, GameId: gameId));

                        Save();
                        Clear();
                        if (incitement)
                        {
                            WriteLine("Your positive review has been created, press any key to continue!");
                            ReadKey();
                        }
                        else
                        {
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
    }
}




