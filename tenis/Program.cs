using System.Collections;
using tenisApp.Views;
using tennis;


Match match = new Match();
new MatchConfigurator(match).interact();
new ScoreBoard(match).interact();
