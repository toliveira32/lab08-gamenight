/*
* Name: Tiago Oliveira
* Course: CSCI 1250, Section 001
* Assignment: Lab 08, Game Night
* Date: November 18, 2026
* Description: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.
*/

//Constants of our program, in this case contants that refere to the 3 files we created (players, points and history)
const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

//The menu that displays to the user
Console.WriteLine("PLAYER".PadRight(11) + "POINTS");

//Part 1: The standings, read both files into arrays and display to the user how is the season going so far

string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

//Method to get the leader with the highest score out of the array
static int LeaderIndex(string[] points)
{
    int best = 0;

    //Reason for the for, I need the position of the best score not just the score, so I need the index and a for each does all of them I dont have the index
    for (int i = 1; i < points.Length; i++)
    {
        if (Convert.ToInt32(points[i]) > Convert.ToInt32(points[best]))
        {
            best = i;
        }
    }
    return best;
}

//Method getting the total points of every player out of the array
static int TotalPoints(string[] points)
{
    int total = 0;

    //Reason for the foreach, every value gets added so I want to run throught all of them, and the index doesnt matter
    foreach (string p in points)
    {
        total += Convert.ToInt32(p);
    }
    return total;
}

//Reason for the for, I need to have some sort of index for the two arrays, so I can display the exact position I want
for (int i = 0; i < players.Length; i++)
{
    Console.WriteLine(players[i].PadRight(11) + points[i]);
}

//More information to dislpay at the menu to the user
int leader = LeaderIndex(points);
Console.WriteLine($"Leader: {players[leader]} with {points[leader]}");
Console.WriteLine($"Season total: {TotalPoints(points)}");

//Part 2: Game Night
//Asking the players for the points of the night and doing the calculations for the session, so adding the points and saving them and saying whos the winner

string winnerTonight = "";
int bestTonight = -1;

//The reason for the for, each players name and season total needs to share an index, and I need that index to put the new total on the right place
for (int i = 0; i < players.Length; i++)
{
    Console.Write($"Points tonight for {players[i]}: ");
    int tonight = Convert.ToInt32(Console.ReadLine());

    //The reason for the while, I need to use a while loop like the professor asked because we need to deny attempts outside of 0-20
    while (tonight < 0 || tonight > MAX_POINTS)
    {
        Console.WriteLine($"Points must be from 0 to {MAX_POINTS}.");
        Console.Write($"Points tonight for {players[i]}: ");
        tonight = Convert.ToInt32(Console.ReadLine());
    }

    //Like the professor said we need to Convert on the way in and ToString on the way out
    int season = Convert.ToInt32(points[i]) + tonight;
    points[i] = season.ToString();

    //Tracking tonight best score and who won tonight
    if (tonight > bestTonight)
    {
        bestTonight = tonight;
        winnerTonight = players[i];
    }
}

//Displaying the new information on the menu and saving the new saved totals and replcing the previous ones
Console.WriteLine($"Tonight's winner: {winnerTonight} with {bestTonight}\n");
File.WriteAllLines(POINTS_FILE, points);
Console.WriteLine("Saved points.txt.");

//the 3 methods we will need
/* static int LeaderIndex(string[] points)
static int TotalPoints(string[] points)
static string HistoryEntry(int night, string winner, int points) */