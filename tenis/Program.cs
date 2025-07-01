// See https://aka.ms/new-console-template for more information
using tennis;

Console.WriteLine("Hello, World!");
Match match = new Match();
ScoreBoard.instance().set(match);
match.play();

