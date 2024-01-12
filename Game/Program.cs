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
        // while loop for menu
        while (true)
        {
            // declare empty variables
            List<string> validInp = [];
            string userInp = "";
            // clear terminal
            Clear();
            // check the game count if > 0 give following options
            if (games.game.Count > 0)
            {
                // options
                validInp = ["1", "2", "X"];
                // welcome menu text and check user inp
                WriteLine("Welcome to Ted's gameshop!\n1. See all games\n2. Login as admin\nX. Exit shop");
                userInp = ReadLine()!;
            }
            // if game count 0 
            else
            {
                // only admin able to get in
                validInp = ["2", "X"];
                WriteLine("Welcome to Ted's gameshop!\nUnforunately the shop is currently closed!\n2. Login as admin\nX. Exit shop");
                userInp = ReadLine()!;
            }

            // check if userinp is empty
            if (String.IsNullOrEmpty(userInp))
            {
                // error message
                Clear();
                WriteLine("Please enter a valid input, press any key to continue!");
                ReadKey();
            }
            // if userinp input contains in array
            else if (validInp.Contains(userInp.ToUpper()))
            {
                // check which input it was with a switch
                switch (userInp.ToUpper())
                {
                    // if 1, go to menu for non admins
                    case "1":
                        if (validInp.Contains("1"))
                        {
                            menu2(1);
                        }
                        break;
                    // if 2, try to login admin
                    case "2":
                        if (validInp.Contains("2"))
                        {
                            // clear
                            Clear();
                            // prompt user to login
                            bool login = games.LoginAdmin();
                            // check if login was successful
                            if (!login)
                            {
                                // error message
                                Clear();
                                WriteLine("You have entered your username/password incorrectly too many times, shop will now close!");
                            }
                            // if logged in, go to menu for admins
                            else { menu2(2); }

                        }
                        // break case2
                        break;
                    // if case is X exit program
                    case "X":
                        WriteLine("Thank you for visiting the shop, please come again!");
                        break;
                    // if anything else error message (should not be possible in a theoretical level)
                    default:
                        Clear();
                        WriteLine("Please enter a valid input, press any key to continue!");
                        ReadKey();
                        break;
                }
                // break loop
                break;
            }
            else // error message if not contained in array
            {
                Clear();
                WriteLine("Please enter a valid input, press any key to continue!");
                ReadKey();
            }


        }
    }

    // menu once first selection is made in main menu
    void menu2(int section)
    {
        // while loop
        while (true)
        {
            //clear
            Clear();
            // declare empty array
            List<string> validInp = [];
            // if selection was made to go into userportal
            if (section == 1)
            {
                // declare valid selections and prompt user what choices it can make
                validInp = ["1", "2", "X", "A"];
                WriteLine($"Games on shop: \n{games.PrintGames()}");

                WriteLine("What do you want to do?\n1. Watch reviews\n2. Make a review\nX. Exit shop");

            }
            // if selection was made to go into adminportal
            else if (section == 2)
            {
                // declare valid admin selections and prompt admin the choices
                validInp = ["1", "2", "3", "4", "5", "X"];
                WriteLine($"Games on shop: \n{games.PrintGames()}");
                WriteLine("Ted's gameshop adminview!\n1. Add game\n2. Edit game\n3. Delete game\n4. Reviews\n5. Logout\nX. Exit shop");

            }

            // check users input
            string userInp = ReadLine()!;

            // check if input is inside the array
            if (validInp.Contains(userInp.ToUpper()))
            {
                // use switch to navigate the user to correct functions
                switch (userInp.ToUpper())
                {
                    // if option 1
                    case "1":
                        // if user
                        if (section == 1)
                        {
                            // clear terminal and go to review menu
                            Clear();
                            RevMenu(1);
                        }
                        else // if admin
                        {
                            // clear terminal and go to AddGame function to add games
                            Clear();
                            AddGame(games.admin);
                        }
                        break;
                    // if selection 2
                    case "2":
                        // if user
                        if (section == 1)
                        {
                            // go to reviewmenu option 2
                            Clear();
                            RevMenu(2);

                        }
                        // if admin
                        else
                        {
                            // edit game function
                            Clear();
                            EditGame(games.admin);
                        }
                        break;
                    // case 3 only admin
                    case "3":
                        // delete game function
                        Clear();
                        DeleteGame(games.admin);
                        break;
                    // case 4 only admin
                    case "4":
                        // review menu for admins
                        Clear();
                        RevMenu(3, games.admin);
                        break;
                    // case 5 only admins
                    case "5":
                        // logout the admin and go to main menu
                        Clear();
                        WriteLine("You have now been logged out, press any key to continue!");
                        ReadKey();
                        games.LogoutAdmin();
                        WelcomeMenu();
                        break;
                    // case X, exit the shop
                    case "X":
                        Clear();
                        WriteLine("Thank you for visiting the shop, please come again!");
                        break;

                    // case A, hidden on usermenu but option for admins
                    case "A":
                        Clear();
                        // prompt user to login
                        bool login = games.LoginAdmin();
                        // check if login was successful
                        if (!login)
                        {
                            // error message
                            Clear();
                            WriteLine("You have entered your username/password incorrectly too many times, shop will now close!");
                        }
                        // if logged in, go to menu for admins
                        else { menu2(2); }
                        break;

                    // if anything else give error (should be impossible on theoretical level)
                    default:
                        Clear();
                        WriteLine("Please enter a valid input, press any key to continue!");
                        ReadKey();
                        break;

                }
                // break loop
                break;
            }
            // else if not in array give error message
            else
            {
                Clear();
                WriteLine("Please enter a valid input, press any key to continue!");
                ReadKey();
            }

        }
    }

    // review menu for users and admins
    void RevMenu(int selection, bool loggedIn = false)
    {
        // not admin and selected to watch reviews
        if (selection == 1 && !loggedIn)
        {
            // setup dummy boolean to break loops 
            bool dummy = true;
            while (dummy)
            {
                Clear();

                // show all games
                WriteLine($"Displaying all games:\n{games.PrintGames()}");

                // prompt user to see positive, negative or all reviews on a product
                List<string> validInp = ["1", "2", "3", "X"];
                WriteLine("What reviews do you want to see?\n1. Positive reviews\n2. Negative reviews\n3. All reviews\nX. Go back");
                string rev = ReadLine()!;
                // if x go back to menu
                if (rev.ToUpper() == "X")
                {
                    // break loop
                    dummy = false;
                    menu2(1);
                }
                // if input which is on array
                else if (validInp.Contains(rev.ToUpper()))
                {
                    // prompt user which id they want to see review on
                    Clear();
                    WriteLine(games.PrintGames());
                    WriteLine("Select the id of the game you want to see the review on: ");
                    string gameid = ReadLine()!;
                    // check if integer was sent
                    if (Int32.TryParse(gameid, out int id))
                    {
                        // switch case based on sentiment
                        switch (rev.ToUpper())
                        {
                            // if positive reviews
                            case "1":
                                // send id and sentiment true
                                revOutput(id, true);
                                break;
                            // if negative reviews
                            case "2":
                                // send id and sentiment false
                                revOutput(id, false);
                                break;
                            // if all reviews
                            case "3":
                                // send id and that sentimentdummy is false 
                                revOutput(id, sentimentDummy: false);
                                break;
                            // anything else error msg
                            default:
                                Clear();
                                WriteLine("Please enter a valid input, press any key to continue!");
                                ReadKey();
                                break;
                        }
                        //break loop
                        dummy = false;

                    }
                    // if not in able to parse to int, error msg
                    else
                    {
                        Clear();
                        WriteLine("Please enter a valid input, press any key to continue!");
                        ReadKey();
                    }


                }
                // if not in array, error message
                else
                {
                    Clear();
                    WriteLine("Please enter a valid input, press any key to continue!");
                    ReadKey();
                }

            }



        }
        // if user wants to add review 
        else if (selection == 2 && !loggedIn)
        {
            bool dummy = true;
            // while loop
            while (dummy)
            {

                Clear();

                // show all games and prompt user to enter product id
                WriteLine($"Displaying all games:\n{games.PrintGames()}\n\nOptions:\nSelect the id on the product you want to make a review on!\nX to go back!");


                string userInp = ReadLine()!;
                // x to go back
                if (userInp.ToUpper() == "X")
                {
                    // break loop
                    dummy = false;
                    // go back to the user menu
                    menu2(1);
                }
                // try to parse the userinp id to int
                else if (Int32.TryParse(userInp, out int id))
                {
                    // call the method to add review
                    reviews.AddComment(id);

                    // check if user wants to make another review
                    WriteLine("Do you want to make another review? (Y/N)");
                    string userInp2 = ReadLine()!;

                    // if no go back to menu
                    if (userInp2.ToUpper() == "N")
                    {
                        // break loop
                        dummy = false;
                        menu2(1);
                    }
                    // if anythine else but yes, write error message and go back to menu 
                    else if (userInp2.ToUpper() != "Y")
                    {
                        Clear();
                        WriteLine("Entered an incorrect input, press any key to go back to the user menu!");
                        ReadKey();
                        // break loop
                        dummy = false;
                        // redirect
                        menu2(1);
                    }

                }
                else // invalid input, error msg
                {
                    Clear();
                    //error msg
                    WriteLine("Please enter a valid id, press any key to try again");
                    ReadKey();
                }


            }
        }
        // admin made selection to go to the review menu
        else if (selection == 3 && loggedIn)
        {
            bool dummy = true;
            while (dummy)
            {
                List<string> validInp = ["1", "2", "X"];

                Clear();
                WriteLine($"Displaying all games:\n{games.PrintGames()}\n\n1. Check reviews\n2. Delete review\nX. Go back");



                //check userinp
                string userInp = ReadLine()!;

                if (validInp.Contains(userInp.ToUpper()))
                {
                    switch (userInp.ToUpper())
                    {
                        case "1":
                            // setup dummy boolean to break loops 
                            bool dummy2 = true;
                            while (dummy2)
                            {
                                Clear();

                                // show all games
                                WriteLine($"Displaying all games:\n{games.PrintGames()}");

                                // prompt user to see positive, negative or all reviews on a product
                                List<string> validInp2 = ["1", "2", "3", "X"];
                                WriteLine("What reviews do you want to see?\n1. Positive reviews\n2. Negative reviews\n3. All reviews\nX. Go back");
                                string rev = ReadLine()!;
                                // if x go back to menu
                                if (rev.ToUpper() == "X")
                                {
                                    // break loop
                                    dummy2 = false;

                                }
                                // if input which is on array
                                else if (validInp2.Contains(rev.ToUpper()))
                                {
                                    // prompt user which id they want to see review on
                                    Clear();
                                    WriteLine(games.PrintGames());
                                    WriteLine("Select the id of the game you want to see the review on: ");
                                    string gameid = ReadLine()!;
                                    // check if integer was sent
                                    if (Int32.TryParse(gameid, out int id))
                                    {
                                        // switch case based on sentiment
                                        switch (rev.ToUpper())
                                        {
                                            // if positive reviews
                                            case "1":
                                                // send id and sentiment true
                                                revOutput(id, true, admin: loggedIn);
                                                break;
                                            // if negative reviews
                                            case "2":
                                                // send id and sentiment false
                                                revOutput(id, false, admin: loggedIn);
                                                break;
                                            // if all reviews
                                            case "3":
                                                // send id and that sentimentdummy is false 
                                                revOutput(id, sentimentDummy: false, admin: loggedIn);
                                                break;
                                            // anything else error msg
                                            default:
                                                Clear();
                                                WriteLine("Please enter a valid input, press any key to continue!");
                                                ReadKey();
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        Clear();
                                        WriteLine("Incorrect input value, press any key to try again!");
                                        ReadKey();
                                    }
                                }
                            }
                            break;
                        case "2":

                            bool dummy3 = true;
                            while (dummy3)
                            {
                                // select game id 
                                Clear();
                                WriteLine($"Displaying all games:\n{games.PrintGames()}");
                                WriteLine("Please enter what game id you want to delete the review on (X to exit)");
                                string userInp2 = ReadLine()!;
                                // try to parse the input to int
                                if (Int32.TryParse(userInp2, out int id))
                                {
                                    // check if that game has any reviews
                                    if (reviews.ReviewCounter(id, loggedIn))
                                    {
                                        // get a list of valid inputs otherwise able to remove a review outside the selected game
                                        List<int> list = reviews.GetReviewListById(id);
                                        // if it has games, print all of them out
                                        Clear();
                                        WriteLine(reviews.GetReviewsById(id, loggedIn) + "\n\nSelect the review id to remove\nX to go back");
                                        string userInp3 = ReadLine()!;
                                        if (userInp3.ToUpper() == "X")
                                        {
                                            dummy3 = false;
                                            dummy = false;
                                            RevMenu(3, loggedIn);
                                        }
                                        // if input can be parsed to int
                                        else if (Int32.TryParse(userInp3, out int id2))
                                        {
                                            // if the parsed input contains in the list of valid review id's
                                            if (list.Contains(id2))
                                            {
                                                // delete that review
                                                reviews.DeleteReviewById(id2, loggedIn);
                                                Clear();
                                                // ask if admin wants to remove another review
                                                WriteLine("Want to remove another review? (Y/N)");
                                                string userInp4 = ReadLine()!;
                                                // if no send to menu
                                                if (userInp4.ToUpper() == "N")
                                                {
                                                    dummy3 = false;
                                                    dummy = false;
                                                    RevMenu(3, true);

                                                }
                                                // if anything but yes send to menu
                                                else if (userInp4.ToUpper() != "Y")
                                                {
                                                    Clear();
                                                    WriteLine("Incorrect input, press any key to go back to admin menu!");
                                                    dummy3 = false;
                                                    dummy = false;

                                                    RevMenu(3, true);
                                                }
                                            }
                                            else
                                            {
                                                // if invalid input due not id not in list
                                                Clear();
                                                WriteLine("Invalid review id, press any key to try again!");
                                                ReadKey();
                                            }
                                        }
                                        else // if invalid input due to not able to parse to int
                                        {
                                            Clear();
                                            WriteLine("Incorrect input, press any key to try again!");
                                            ReadKey();
                                        }


                                    } // if no reviews
                                    else
                                    {
                                        Clear();
                                        WriteLine($"Game id: {id} has no reviews, press any key to try again!");
                                        ReadKey();
                                    }
                                }
                                else if (userInp2.ToUpper() == "X")
                                {
                                    dummy3 = false;
                                    dummy = false;
                                    RevMenu(3, loggedIn);
                                }

                                // if game id was not able to parse to int
                                else
                                {
                                    Clear();
                                    WriteLine("Incorrect input, press any key to try again!");
                                    ReadKey();
                                }
                            }
                            break;
                        case "X":
                            // case X send back to admin main menu
                            menu2(2);
                            break;
                        default:
                            // if something goes wrong  error msg send back to admin main menu
                            Clear();
                            WriteLine("The input was incorrect, press any key to try again!");
                            ReadKey();
                            menu2(2);
                            break;
                    }
                }
                break;
            }

        }
    }

    // revoutput, takes id, sentiment false/true (negative/positive), and sentimentDummy = false == all reviews, admin = false == not logged in
    void revOutput(int id, bool sentiment = false, bool sentimentDummy = true, bool admin = false)
    {
        Clear();


        // if sentimentdummy true, get reviews based on sentiment
        if (sentimentDummy)
        {

            WriteLine(reviews.GetReviewsBySentiment(id, sentiment, admin));
        }
        // if sentimentdummy false, get reviews based on id only I.E. all reviews on that id
        else
        {
            WriteLine(reviews.GetReviewsById(id, admin));
        }
        ReadKey();
        if (!admin)
        {
            // go back to review menu as user
            RevMenu(1);
        }
    }

    void AddGame(bool loggedIn)
    {
        // check adminstatus
        if (loggedIn)
        {
            // add game method
            games.AddGame();
            // go back to adminmenu
            menu2(2);
        }
    }

    void EditGame(bool loggedIn)
    {
        // check admin is logged in
        if (loggedIn)
        {
            bool dummy = true;
            // while loop
            while (dummy)
            {
                // clear terminal, show games and prompt user
                Clear();
                WriteLine(games.PrintGames());
                WriteLine("Select the id of the game you want to edit, press X to go back!");
                string userInp = ReadLine()!;
                // if x go back
                if (userInp.ToUpper() == "X")
                {
                    // back to menu for admins
                    dummy = false;
                    menu2(2);
                }
                // if integer inputted
                else if (Int32.TryParse(userInp, out int id))
                {
                    // try edit game
                    games.EditGame(id);
                    // clear after message and check if another game should be edited
                    Clear();
                    WriteLine(games.PrintGames());
                    WriteLine("Want to edit another game? (Y/N)");
                    string userInp2 = ReadLine()!;
                    // if user says no go back to menu for admins
                    if (userInp2.ToUpper() == "N")
                    {
                        dummy = false;
                        menu2(2);
                    }
                    else if (userInp2.ToUpper() != "Y")
                    {
                        Clear();
                        dummy = false;
                        WriteLine("You have entered an incorrect input, you will no go to the admin menu!");
                        ReadKey();
                        menu2(2);
                    }
                }
                // if input is incorrect 
                else
                {
                    // error message
                    Clear();
                    WriteLine("You have entered an incorrect input, press any key to try again!");
                    ReadKey();

                }
            }
        }
    }
    void DeleteGame(bool loggedIn)
    {
        // check admin status
        if (loggedIn)
        {
            bool dummy = true;

            // while loop
            while (dummy)
            {
                // clear terminal show all games and prompt user to input
                Clear();
                WriteLine(games.PrintGames());
                WriteLine("Enter the id of the game you want to delete, X to go back!");
                string userInp = ReadLine()!;
                // if x go back to adminmenu
                if (userInp.ToUpper() == "X")
                {
                    dummy = false;
                    menu2(2);

                }
                // else if integer try delete
                else if (Int32.TryParse(userInp, out int id))
                {
                    games.DeleteGame(id);
                    // clear, show all games and check if admin wants to remove anything else
                    Clear();
                    WriteLine(games.PrintGames());
                    WriteLine("Want to remove another game? (Y/N)");
                    string userInp2 = ReadLine()!;
                    // if no go back to admin menu
                    if (userInp2.ToUpper() == "N")
                    {
                        dummy = false;
                        menu2(2);
                    }
                    // else send user back to the admin menu
                    else if (userInp2.ToUpper() != "Y")
                    {
                        Clear();
                        dummy = false;
                        WriteLine("You have entered an incorrect input, you will now go to the admin menu!");
                        ReadKey();
                        menu2(2);
                    }
                }
                // if input is incorrect
                else
                {
                    // error message
                    Clear();
                    WriteLine("You have entered an incorrect input, press any key to try again!");
                    ReadKey();
                }
            }
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