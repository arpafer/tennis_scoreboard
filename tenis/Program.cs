// See https://aka.ms/new-console-template for more information
using System.Collections;
using tenisApp.Views;
using tennis;

// PlayersManager.instance().registerTournamentPlayers();
//ScoreBoard scoreBoard = new ScoreBoard();
Match match = new Match();
Hashtable players = new Hashtable();
MatchConfigurator matchConfigView = new MatchConfigurator(players, match);
matchConfigView.interact();

ScoreBoard scoreBoard = new ScoreBoard(match);



