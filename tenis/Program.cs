// See https://aka.ms/new-console-template for more information
using tennis;

PlayersManager.instance().registerTournamentPlayers();
Match match = new Match();
match.readConfig();
match.play();

