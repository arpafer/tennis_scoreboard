using System.Collections;
using tenisApp.Views;
using tennis;


Match match = new Match();
MatchConfigurator matchConfigView = new MatchConfigurator(match);
matchConfigView.interact();
ScoreBoard scoreBoard = new ScoreBoard(match);
scoreBoard.interact();



