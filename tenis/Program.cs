// See https://aka.ms/new-console-template for more information
using System.Collections;
using tenisApp.Views;
using tennis;

// PlayersManager.instance().registerTournamentPlayers();
//ScoreBoard scoreBoard = new ScoreBoard();
Match match = new Match();
Hashtable players = new Hashtable();
MatchConfigurator matchConfig = new MatchConfigurator(players, match);
matchConfig.interact();

scoreBoard.set(match);
match.readConfig();
match.play();

