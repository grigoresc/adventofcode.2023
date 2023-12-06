
var x = @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";

var input = File.ReadAllText("input.txt");
x = input;

var lines = x.Split("\r\n");

foreach (var line in lines)
{
    var split = line.Split(':');
    var game = int.Parse(split[0].Replace("Game ", ""));
    Console.WriteLine(game);
    var splitg = split[1].Split(";");
}