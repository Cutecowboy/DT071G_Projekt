using System.Text.Json;
using System.Text.Json.Serialization;

using static System.Console;

namespace Packt.Shared;

public class Games
{

    // Game object will store following variables
    // int id, string name, string developer, int/datetime year, int price
    public record Game(int Id, string Name, string Developer, DateTime Year, int Price);

    // declare empty game object as standard, will be used as read/write variable
    public List<Game> game = [];

    // admin variable which can be toggled
    public bool admin = false;

    // setup the initiation of the game JSON file. 

    public void Setup()
    {
        // check if file exists

        if (File.Exists("game.json"))
        {
            // read the JSON file
            string jsonData = File.ReadAllText("game.json");
            // check that its not empty
            if (!string.IsNullOrEmpty(jsonData))
            {
                // store the json data in the game variable for later usage. Is not null so ignore null warning
                game = JsonSerializer.Deserialize<List<Game>>(jsonData)!;
            }
        }
        else  // file is does not exist
        {
            // create an emtpy json file with no data.
            File.WriteAllText("game.json", "");
        }
    }

    // Functionality to login as an admin
    public bool LoginAdmin()
    {
        // hardcode username and password
        string username = "admin";
        string password = "password";
        // setup a dummy checker for the login and a counter 
        bool dummy = false;
        int counter = 0;
        // while the dummy is false
        while (dummy == false)
        {
            // prompt admin to enter username and password
            WriteLine("Enter your username: ");
            string inpName = ReadLine();
            WriteLine("Enter your password: ");
            string inpPass = ReadLine();

            // check input with the hardcoded username/password
            if (inpName.ToLower() == username && inpPass.ToLower() == password)
            {
                // if true, set the dummy to true, break the loop
                dummy = true;
                admin = true;
            }
            else // user admin prompt wrong information 
            {
                // increment counter 
                counter += 1;
                // break the loop if 3 attempts have been made
                if (counter == 3)
                {
                    break;
                }
                // clear terminal
                Clear();
                // write the error message
                WriteLine($"The information you have entered was invalid, you have {3 - counter} attempts left!");



            }
        }

        // return the dummy as the indicator if admin login was successful.
        return dummy;

    }

    // simple function that logs out the admin
    public bool LogoutAdmin()
    {
        // check if admin is logged in
        if (admin)
        {
            // logout the admin
            admin = false;
            // return true
            return true;
        } else {
            // return false, can prompt error message on another method 
            return false;
        }

    }


}
