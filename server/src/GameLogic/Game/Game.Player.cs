
using Thuai.Server.GameController;

namespace Thuai.Server.GameLogic;

public partial class Game 
{

    #region Fields and properties
    public List<Player> AllPlayers { get; private set; } = [];

    public int PlayerCount => AllPlayers.Count;

    /// <summary>
    /// Record the scores of the players.
    /// </summary>
    /// <remarks>
    /// The i-th player's score is <see cref="Scoreboard"/>[i]
    /// </remarks>
    public List<int> Scoreboard { get; private set; } = [];

    #endregion

    #region Methods

    /// <summary>
    /// Add player in the game.
    /// </summary>
    /// <param name="player">The player to be added.</param>
    /// <returns>If the adding succeeds.</returns>
    public bool AddPlayer(Player player)
    {
        if (Stage != GameStage.Waiting) 
        {
            _logger.Error("Cannot add player: The game is already started.");
            return false;
        }

        try
        {
            lock(_lock)
            {
                AllPlayers.Add(player);
                Scoreboard.Add(0);
                // SubscribePlayerEvents(player);
                return true;
            }
        }
        catch (Exception e)
        {
            _logger.Error($"Cannot add player: {e.Message}");
            _logger.Debug($"{e}");
            return false;
        }
    }

    /// <summary>
    /// Remove a player.
    /// </summary>
    /// <param name="player">The player to be removed.</param>
    public void RemovePlayer(Player player)
    {
        try
        {
            lock(_lock)
            {
                // Is there a fancier way?
                int index = AllPlayers.FindIndex((Player _player) => {
                    return _player == player;
                });
                if (index != -1)
                {
                    Scoreboard.RemoveAt(index);
                }
                AllPlayers.Remove(player);
            }
        }
        catch (Exception e)
        {
            _logger.Error($"Cannot remove player: {e.Message}");
            _logger.Debug($"{e}");
        }
    }

    public void UpdatePlayers()
    {
        foreach (Player player in AllPlayers)
        {
            // Update the player status.
        }
    }


    #endregion

}