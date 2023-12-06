using System.Text.Json;
using System.Text.Json.Serialization;

namespace Packt.Shared;

public class Games
{

    // Game object will store following variables
    // int id, string name, string developer, int/datetime year, int price
    public record Game(int Id, string Name, string Developer, DateTime Year, int Price);

    // declare empty game object as standard, will be used as read/write variable
    public List<Game> game = [];

    // setup the initiation of the game JSON file. 

    public void Setup(){
        // check if file exists

        if(File.Exists("game.json")){
            // read the JSON file
            string jsonData = File.ReadAllText("game.json");
            // check that its not empty
            if(!string.IsNullOrEmpty(jsonData))
            {
                // store the json data in the game variable for later usage. Is not null so ignore null warning
                game = JsonSerializer.Deserialize<List<Game>>(jsonData)!;
            }
        } else  // file is does not exist
        {
            // create an emtpy json file with no data.
            File.WriteAllText("game.json", "");
        }
    }


}
