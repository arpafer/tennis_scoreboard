// See https://aka.ms/new-console-template for more information
using tennis;

PlayersManager.instance().registerTournamentPlayers();
ScoreBoard scoreBoard = new ScoreBoard();
Match match = new Match(scoreBoard);
scoreBoard.set(match);
match.readConfig();
match.play();

