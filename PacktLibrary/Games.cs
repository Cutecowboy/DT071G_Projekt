using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
namespace Packt.Shared;

public class Games
{

    // Game object will store following variables
    // int id, string name, string developer, int/datetime year, int price  
    /*     public record struct Game(int Id, string Name, string Developer, int Year, int Price);
     */
    public record Game
    {
        public Game(int Id, string Name, string Developer, int Year, int Price)
        {
            this.Id = Id;
            this.Name = Name;
            this.Developer = Developer;
            this.Year = Year;
            this.Price = Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public int Year { get; set; }
        public int Price { get; set; }
    }
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
            string inpName = ReadLine()!;
            WriteLine("Enter your password: ");
            string inpPass = ReadLine()!;

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
        }
        else
        {
            // return false, can prompt error message on another method 
            return false;
        }

    }

    public void AddGame()
    {
        // Ensure no brute-forcing, admin has to be logged in
        if (admin)
        {
            // declare dummies for input values
            bool NameChecker = false;
            bool DevChecker = false;
            bool PriceChecker = false;
            bool YearChecker = false;

            // declare empty values for the input params
            string inpName = "";
            string inpDev = "";
            string inpPrice = "";
            string inpYear = "";


            // first loop
            while (NameChecker == false)
            {
                // clear terminal
                Clear();
                // prompt user to enter game name
                WriteLine("Enter the games name: ");
                inpName = ReadLine()!;
                // check whether input is empty
                if (string.IsNullOrEmpty(inpName))
                {
                    // clear
                    Clear();
                    // prompt user to re-enter valid name
                    WriteLine("Please enter a valid name, press any key to continue!");
                    ReadKey();
                }
                // check if input is greater than 1 character long
                else if (inpName.Length < 2)
                {
                    // clear
                    Clear();
                    // prompt user to re-enter valid name
                    WriteLine("Please enter a name that includes atleast two characters, press any key to continue!");
                    ReadKey();
                }
                else // success
                {
                    // set dummy to true, break loop
                    NameChecker = true;
                }
            }


            // second loop
            while (DevChecker == false)
            {
                // clear
                Clear();
                // prompt user to add dev name
                WriteLine("Enter the developers name: ");
                inpDev = ReadLine()!;
                // check if empty input
                if (string.IsNullOrEmpty(inpDev))
                {
                    Clear();
                    // prompt user to re-enter valid name
                    WriteLine("Please enter a valid name, press any key to continue!");
                    ReadKey();
                }

                // check that characters is greater than 1 character long
                else if (inpDev.Length < 2)
                {
                    Clear();
                    // prompt user to re-enter valid name
                    WriteLine("Please enter a name that includes atleast two characters, press any key to continue!");
                    ReadKey();
                }
                else // success
                {
                    // set dummy to true, break loop
                    DevChecker = true;
                }
            }

            // third loop 
            while (YearChecker == false)
            {
                //clear
                Clear();
                // prompt user to enter a year
                WriteLine("Enter the published game date in years (YYYY): ");
                inpYear = ReadLine()!;
                // try to parse input value to an int
                if (Int32.TryParse(inpYear, out int year))
                {
                    // if able to parse, check that the inputted year is between 1970 and the current year (2023)
                    if (year >= 1970 && year <= DateTime.Now.Year)
                    {
                        // set dummy to true, break loop
                        YearChecker = true;
                    }
                    else // wrong date
                    {
                        Clear();
                        // explain to user to enter a year between the condition above
                        WriteLine($"Please enter a year between 1970 and {DateTime.Now.Year}, press any key to try again!");
                        ReadKey();
                    }

                }
                else // failed format
                {
                    Clear();
                    // promt user to re-enter valid year format
                    WriteLine("Please enter a valid year, format YYYY, press any key to continue!");
                    ReadKey();
                }
            }

            // fourth loop
            while (PriceChecker == false)
            {
                Clear();
                // prompt user to enter the games price
                WriteLine("Enter the games price: ");
                inpPrice = ReadLine()!;
                // try to parse the input string to an int
                if (Int32.TryParse(inpPrice, out int price))
                {
                    // if parsed, check that price is greater than 0
                    if (price > 0)
                    {
                        // set dummy to true, break loop
                        PriceChecker = true;
                    }
                    else // zero or negative
                    {
                        Clear();
                        // prompt user to re-enter a valid price
                        WriteLine("Please enter a price greater than 0, press any key to try again!");
                        ReadKey();
                    }

                }
                else // invalid input
                {
                    Clear();
                    // prompt user to re-enter valid input
                    WriteLine("Please enter a valid price, press any key to continue!");
                    ReadKey();
                }
            }

            // double-check whether all loops are correct before storing on JSON file. 
            if (DevChecker && NameChecker && YearChecker && PriceChecker)
            {
                // append the new game to the game list
                game.Add(new Game(Id: PostId(), Name: inpName, Developer: inpDev, Year: Int32.Parse(inpYear), Price: Int32.Parse(inpPrice)));

                // save the new game
                Save();
                // clear terminal
                Clear();
                // prompt user that the game has been stored
                WriteLine($"Name: {inpName}\nDeveloper: {inpDev}\nYear: {inpYear}\nPrice: {inpPrice}\nHas successfully been created, press any key to continue!");
                ReadKey();

            }
            else // all conditions are not good
            {
                WriteLine("Something went wrong, please try again, press any key to continue!");
                ReadKey();
            }
        }
        else
        {
            Clear();
            // prompt user that they have to be logged in to access this functionality, guard for brute forcing. 
            WriteLine("Must be logged in to add a new game to the file, please log in and try again, press any key to continue!");
            ReadKey();
        }
    }
    /// <summary>
    /// Save the content on the entries to the JSON file
    /// </summary>
    public void Save()
    {
        // if no entries, reduce bugs by replacing empty array with empty string
        if (game.Count == 0)
        {
            // empty string instead of []
            File.WriteAllText("game.json", "");
        }
        else
        {
            // Serialize the entries
            string json = JsonSerializer.Serialize(game);
            // write the json data to the json file
            File.WriteAllText("game.json", json);
        }

    }

    // delete a game with id as param
    public void DeleteGame(int id)
    {
        // check that you're logged in as admin
        if (admin)
        {
            // if the inputted id is >= 0 and the id <= game count 
            if (game.FindAll(g => g.Id == id).Count == 1)
            {
                int index = game.FindIndex(g => g.Id == id);
                // try to 
                try
                {
                    // remove the game at said index
                    game.RemoveAt(index);
                    // save
                    Save();
                    // clear
                    Clear();
                    // write success message
                    WriteLine($"Post id: {id} is now removed!");


                }
                // catch exceptionerror in case something goes wrong
                catch (ArgumentException)
                {
                    // clear
                    Clear();
                    // write error message
                    WriteLine($"Post id: {id} was not found, press any key to continue!");
                    ReadKey();
                }
            }
            // if you write an id below zero or above the count
            else
            {
                Clear();
                // error message
                WriteLine($"Post id: {id} was not found, press any key to continue!");
                ReadKey();
            }
        }
        // if you're not logged in as admin
        else
        {
            Clear();
            // prompt user that they have to be logged in to access this functionality, guard for brute forcing. 
            WriteLine("Must be logged in to add a new game to the file, please log in and try again, press any key to continue!");
            ReadKey();
        }



    }

    // check index and return a index
    public int PostId()
    {
        // if there are not games 
        if (game.Count == 0)
        {
            // return Id 0
            return 0;
        }
        else
        {
            // return the max value of the Id + 1, this to ensure correct "database" Id value
            return game.Max(t => t.Id) + 1;
        }
    }

    public string PrintGames()
    {
        // declare empty string
        string message = "";
        if (game.Count == 0)
        {
            // inform that there are no games
            message = "There are no games in the store!";

        }
        else
        {
            message = String.Format("{0,-5} {1,-30} {2,-30} {3, -10} {4, -10}\n\n", "ID", "Title", "Developer", "Published", "Price");
            for (int i = 0; i < game.Count; i++)
            {
                // add information to the string
                message += String.Format("{0,-5} {1,-30} {2,-30} {3, -10} {4, -10}\n", game[i].Id, game[i].Name, game[i].Developer, game[i].Year, game[i].Price);
            }
        }
        return message;
    }

    // edit game by id
    public void EditGame(int id)
    {
        // ensure no brute-forcing, admin has to be logged in
        if (admin)
        {
            // if game with that id exists, should always be one as id is unique
            if (game.FindAll(g => g.Id == id).Count == 1)
            {
                int index = game.FindIndex(g => g.Id == id);
                // declare dummies for input values
                bool NameChecker = false;
                bool DevChecker = false;
                bool PriceChecker = false;
                bool YearChecker = false;

                // declare empty values for the input params
                string inpName = "";
                string inpDev = "";
                string inpPrice = "";
                string inpYear = "";

                // first loop
                while (NameChecker == false)
                {
                    // clear terminal
                    Clear();
                    // prompt user to enter game name
                    WriteLine("Enter the games name: ");
                    inpName = ReadLine()!;
                    // check whether input is empty
                    if (string.IsNullOrEmpty(inpName))
                    {
                        // clear
                        Clear();
                        // prompt user to re-enter valid name
                        WriteLine("Please enter a valid name, press any key to continue!");
                        ReadKey();
                    }
                    // check if input is greater than 1 character long
                    else if (inpName.Length < 2)
                    {
                        // clear
                        Clear();
                        // prompt user to re-enter valid name
                        WriteLine("Please enter a name that includes atleast two characters, press any key to continue!");
                        ReadKey();
                    }
                    else // success
                    {
                        // set dummy to true, break loop
                        NameChecker = true;
                    }
                }


                // second loop
                while (DevChecker == false)
                {
                    // clear
                    Clear();
                    // prompt user to add dev name
                    WriteLine("Enter the developers name: ");
                    inpDev = ReadLine()!;
                    // check if empty input
                    if (string.IsNullOrEmpty(inpDev))
                    {
                        Clear();
                        // prompt user to re-enter valid name
                        WriteLine("Please enter a valid name, press any key to continue!");
                        ReadKey();
                    }

                    // check that characters is greater than 1 character long
                    else if (inpDev.Length < 2)
                    {
                        Clear();
                        // prompt user to re-enter valid name
                        WriteLine("Please enter a name that includes atleast two characters, press any key to continue!");
                        ReadKey();
                    }
                    else // success
                    {
                        // set dummy to true, break loop
                        DevChecker = true;
                    }
                }

                // third loop 
                while (YearChecker == false)
                {
                    //clear
                    Clear();
                    // prompt user to enter a year
                    WriteLine("Enter the published game date in years (YYYY): ");
                    inpYear = ReadLine()!;
                    // try to parse input value to an int
                    if (Int32.TryParse(inpYear, out int year))
                    {
                        // if able to parse, check that the inputted year is between 1970 and the current year (2023)
                        if (year >= 1970 && year <= DateTime.Now.Year)
                        {
                            // set dummy to true, break loop
                            YearChecker = true;
                        }
                        else // wrong date
                        {
                            Clear();
                            // explain to user to enter a year between the condition above
                            WriteLine($"Please enter a year between 1970 and {DateTime.Now.Year}, press any key to try again!");
                            ReadKey();
                        }

                    }
                    else // failed format
                    {
                        Clear();
                        // promt user to re-enter valid year format
                        WriteLine("Please enter a valid year, format YYYY, press any key to continue!");
                        ReadKey();
                    }
                }

                // fourth loop
                while (PriceChecker == false)
                {
                    Clear();
                    // prompt user to enter the games price
                    WriteLine("Enter the games price: ");
                    inpPrice = ReadLine()!;
                    // try to parse the input string to an int
                    if (Int32.TryParse(inpPrice, out int price))
                    {
                        // if parsed, check that price is greater than 0
                        if (price > 0)
                        {
                            // set dummy to true, break loop
                            PriceChecker = true;
                        }
                        else // zero or negative
                        {
                            Clear();
                            // prompt user to re-enter a valid price
                            WriteLine("Please enter a price greater than 0, press any key to try again!");
                            ReadKey();
                        }

                    }
                    else // invalid input
                    {
                        Clear();
                        // prompt user to re-enter valid input
                        WriteLine("Please enter a valid price, press any key to continue!");
                        ReadKey();
                    }
                }
                // double-check whether all loops are correct before storing on JSON file. 
                if (DevChecker && NameChecker && YearChecker && PriceChecker)
                {
                    game[index].Name = inpName;
                    game[index].Developer = inpDev;
                    game[index].Year = Int32.Parse(inpYear);
                    game[index].Price = Int32.Parse(inpPrice);
                    Save();
                }
                else // all conditions are not good
                {
                    WriteLine("Something went wrong, please try again, press any key to continue!");
                    ReadKey();
                }


            }
            else // if id does not exist
            {
                // error message
                WriteLine("That id does not exist, press any key to continue!");
                ReadKey();
            }

        }
        else // if not logged in
        {
            // error message
            WriteLine("Must be logged in to edit a game, please log in and try again, press any key to continue!");
            ReadKey();
        }

    }
}
