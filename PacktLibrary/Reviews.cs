using System.Text.Json;

namespace Packt.Shared
{
    public class Reviews : Games
    {

        // reviews will store following variables
        // id for the review, name of the reviewer, the review, and what game id the review is on
        public record Review(int Id, string Name, string Comment, int GameId);

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


    }


}