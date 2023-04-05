namespace GoodToKnow;

public class Player
{
    public string Name { get; set; }
    public int Number { get; set; }
    public bool Starter { get; set; }
}

public class Roaster
{
    public List<Player> Players { get; set; }
}

public class Examples
{
    private Roaster _roaster;
    private Func<Player, bool> _starterSelector = player => player.Starter;
    private Func<int, int, int> _sum = ((i1, i2) => i1 + i2);
    public List<PlayerviewModel> GetStartersLinq()
    {
        var startingPlayers = _roaster.Players
            .Where(player => player.Starter)
            .Select(player => new PlayerviewModel(player.Name, player.Number));
        return startingPlayers.ToList();
    }

    public List<PlayerviewModel> GetStarters()
    {
        List<PlayerviewModel> playerviewModels = new List<PlayerviewModel>();
        foreach (var player in _roaster.Players)
        {
            if (player.Starter)
            {
                playerviewModels.Add(new PlayerviewModel(player.Name, player.Number));
            }
        }

        return playerviewModels;
    }

    public void LinqExamples()
    {
        List<int> ints = new List<int>() {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
        var even = ints.Where(i => i % 2 == 0);

        var timesTwo = ints.Select(i => i * 2);
    }

}

public class PlayerviewModel
{
    public PlayerviewModel(string playerName, int playerNumber)
    {
        throw new NotImplementedException();
    }
}

