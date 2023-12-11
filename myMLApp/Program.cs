using MyMLApp;

bool dummy = true;

while (dummy == true)
{
    // prompt user to input a review

    Console.WriteLine("Please write your review of the product: ");
    string review = Console.ReadLine()!;

    // check that the string is not empty or null
    if (string.IsNullOrEmpty(review))
    {
        // error message
        Console.WriteLine("You have to enter a review, press any key to try again!");
        Console.ReadKey();
        Console.Clear();
    } else if(review.Length < 3) // check that review is greater than 2 characters
    {
        // error message 
        Console.WriteLine("You have to enter a review with more than 2 characters, press any key to try again!");
        Console.ReadKey();
        Console.Clear();
    } else 
    {
        // add input data
        var sampleData = new SentimentModel.ModelInput()
        {
            Col0 = review
        };
        // load model and predict output of sample data
        var result = SentimentModel.Predict(sampleData);

        // If prediction is 1, sentiment is "Positive"; otherwise sentiment is "Negative"
        var sentiment = result.PredictedLabel == 1 ? "Positive" : "Negative";

        Console.Clear();
        // write the results of the prediction
        Console.WriteLine($"Text: {sampleData.Col0}\nSentiment: {sentiment}\n");
        // while loop that presents user another option
        while (true)
        {
            // prompt user if it wants to predict once more
            Console.WriteLine("Do you want to make another review (y/n)?");
            string userInp = Console.ReadLine()!;
            // if user enters no
            if (userInp.ToUpper() == "N")
            {
                // console and thank user for using the application
                Console.Clear();

                Console.WriteLine("Thank you for using this application, hope you had fun!");
                // set dummy variable to false to break first loop
                dummy = false;
                // break to break second loop
                break;
            }
            // if user enters yes
            else if (userInp.ToUpper() == "Y")
            {
                // clear console 
                Console.Clear();
                // break second loop, first loop still runs and program restarts
                break;
            }
            // if user enters something else
            else 
            {
                // clear console
                Console.Clear();
                // error message
                Console.WriteLine("You have entered an incorrect input, press any key to try again!");
                // read any key
                Console.ReadKey();
                // clear console and rerun the second loop
                Console.Clear();
            }
        }



    }

}



