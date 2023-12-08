using System.Text.Json;

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

            if (File.Exists("reviews.json"))
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
                        bool incitement = true;

                        review.Add(new Review(Id: 0, Name: NameInp, Comment: CommentInp, Incitement: true, GameId: gameId));

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




