using System.ComponentModel.Design;
using System.Data.Common;
using Packt.Shared;
using static System.Console;

static void Program()
{
    // instantizise the objects
    Games games = new();
    Reviews reviews = new();

    // setup neccessary files
    games.Setup();
    reviews.Setup();

    WelcomeMenu();

    // function to start the menu
    void WelcomeMenu()
    {
        while (true)
        {
            List<string> validInp = [];
            string userInp = "";
            Clear();
            if (games.game.Count > 0)
            {
                validInp = ["1", "2", "X"];
                // welcome menu
                WriteLine("Welcome to Ted's gameshop!\n1. See all games\n2. Login as admin\nX. Exit shop");
                userInp = ReadLine()!;
            }
            else
            {
                validInp = ["2", "X"];
                WriteLine("Welcome to Ted's gameshop!\nUnforunately the shop is currently closed!\n2. Login as admin\nX. Exit shop");
                userInp = ReadLine()!;
            }


            if (String.IsNullOrEmpty(userInp))
            {
                Clear();
                WriteLine("Please enter a valid input, press any key to continue!");
                ReadKey();
            }
            else if (validInp.Contains(userInp.ToUpper()))
            {
                switch (userInp.ToUpper())
                {
                    case "1":
                        if (validInp.Contains("1"))
                        {
                            menu2(1);
                        }
                        break;

                    case "2":
                        if (validInp.Contains("2"))
                        {
                            Clear();
                            bool login = games.LoginAdmin();
                            if (!login)
                            {
                                Clear();
                                WriteLine("You have entered your username/password incorrectly too many times, shop will now close!");
                            }
                            else { menu2(2); }

                        }
                        break;
                    case "X":
                        WriteLine("Thank you for visiting the shop, please come again!");
                        break;
                    default:
                        Clear();
                        WriteLine("Please enter a valid input, press any key to continue!");
                        ReadKey();
                        break;
                }
                break;
            }
            else
            {
                Clear();
                WriteLine("Please enter a valid input, press any key to continue!");
                ReadKey();
            }


        }
    }

    void menu2(int section)
    {
        while (true)
        {
            Clear();
            List<string> validInp = [];
            if (section == 1)
            {
                validInp = ["1", "2", "X"];
                WriteLine($"Games on shop: \n{games.PrintGames()}");

                WriteLine("What do you want to do?\n1. Watch reviews\n2. Make a review\nX. Exit shop");

            }
            else if (section == 2)
            {
                validInp = ["1", "2", "3", "4", "5", "X"];
                WriteLine($"Games on shop: \n{games.PrintGames()}");
                WriteLine("Ted's gameshop adminview!\n1. Add game\n2. Edit game\n3. Delete game\n4. Reviews\n5. Logout\nX. Exit shop");

            }

            string userInp = ReadLine()!;

            if (validInp.Contains(userInp.ToUpper()))
            {
                switch (userInp.ToUpper())
                {
                    case "1":
                        if (section == 1)
                        {
                            Clear();
                            RevMenu(1);
                        }
                        else
                        {
                            Clear();
                            AddGame(games.admin);
                        }
                        break;
                    case "2":
                        if (section == 2)
                        {
                            Clear();
                            RevMenu(2);

                        }
                        else
                        {
                            Clear();
                            EditGame(games.admin);
                        }
                        break;
                    case "3":
                        Clear();
                        DeleteGame(games.admin);
                        break;
                    case "4":
                        Clear();
                        RevMenu(4, games.admin);
                        break;
                    case "5":
                        Clear();
                        WriteLine("You have now been logged out, press any key to continue!");
                        ReadKey();
                        games.LogoutAdmin();
                        menu2(1);
                        break;
                    case "X":
                        Clear();
                        WriteLine("Thank you for visiting the shop, please come again!");
                        break;
                    default:
                        Clear();
                        WriteLine("Please enter a valid input, press any key to continue!");
                        ReadKey();
                        break;

                }
                break;
            }
            else
            {
                Clear();
                WriteLine("Please enter a valid input, press any key to continue!");
                ReadKey();
            }

        }
    }

    void RevMenu(int selection, bool loggedIn = false)
    {
        // not admin and selected to watch reviews
        if (selection == 1 && loggedIn == false)
        {
            while (true)
            {
                Clear();

                
                WriteLine($"Displaying all games:\n{games.PrintGames()}");
                // user should now select a game id 

                List<string> validInp = ["1", "2", "3", "X"];
                WriteLine("What reviews do you want to see?\n1. Positive reviews\n2. Negative reviews\n3. All reviews\nX. Go back");
                string rev = ReadLine()!;
                if (rev.ToUpper() == "X") {
                    menu2(1);
                }
                else if (validInp.Contains(rev.ToUpper()))
                {
                    WriteLine("Select the id of the game you want to see the review on: ");
                    // behövs en metod som kollar gameid 
                    string gameid = ReadLine()!;
                    if (Int32.TryParse(gameid, out int id))
                    {
                        switch (rev.ToUpper())
                        {
                            case "1":
                                revOutput(id, true);
                                break;
                            case "2":
                                revOutput(id, false);
                                break;
                            case "3":
                                revOutput(id, sentimentDummy: false);
                                break;
                            default:
                                Clear();
                                WriteLine("Please enter a valid input, press any key to continue!");
                                ReadKey();
                                break;
                        }
                        break;
                    }
                    else
                    {
                        Clear();
                        WriteLine("Please enter a valid input, press any key to continue!");
                        ReadKey();
                    }


                }
                else
                {
                    Clear();
                    WriteLine("Please enter a valid input, press any key to continue!");
                    ReadKey();
                }

            }



        }
    }

    void revOutput(int id, bool sentiment = false, bool sentimentDummy = true)
    {
        Clear();
        if (sentimentDummy)
        {
            WriteLine(reviews.GetReviewsBySentiment(id, sentiment));
        }
        else
        {
            WriteLine(reviews.GetReviewsById(id));
        }
        ReadKey();
        RevMenu(1);
    }

    void AddGame(bool loggedIn)
    {
        if (loggedIn)
        {
            WriteLine("add game");
        }
    }

    void EditGame(bool loggedIn)
    {

        if (loggedIn)
        {

            WriteLine("edit game");
        }
    }
    void DeleteGame(bool loggedIn)
    {
        if (loggedIn)
        {
            WriteLine("del game");
        }
    }

}

// serve as a control variable when the admin is logged in.
/* games.LoginAdmin();
WriteLine(games.admin);
games.admin = true; */


Program();


/* Games games = new();
Reviews reviews = new();

games.Setup();
reviews.Setup();

WriteLine(reviews.GetReviewsById(1)); */