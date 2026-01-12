using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerSetupModel
{
    private readonly IStorePlayersCountInfoProvider _storePlayersCountInfoProvider;

    private readonly PlayerPeople _playerPeople;
    private readonly List<PlayerBot> _playerBots = new();

    public PlayerSetupModel(IStorePlayersCountInfoProvider storePlayersCountInfoProvider, PlayerPeople playerPeople, List<PlayerBot> playerBots)
    {
        _storePlayersCountInfoProvider = storePlayersCountInfoProvider;
        _playerPeople = playerPeople;
        _playerBots = playerBots;
    }

    public void Setup()
    {
        if (_playerBots.Count > _nicknames.Length)
        {
            Debug.LogWarning("Ботов больше, чем доступных никнеймов!");
        }

        // Скопировать и перемешать никнеймы
        var availableNicknames = new List<string>(_nicknames);

        availableNicknames.Shuffle();

        foreach (var bot in _playerBots)
        {
            // Никнейм
            var nickname = availableNicknames[0];
            availableNicknames.RemoveAt(0);
            bot.SetNickname(nickname);

            // Аватар (индекс от 0 до 7)
            var avatarIndex = Random.Range(0, 8);
            bot.SetAvatarIndex(avatarIndex);
        }
    }

    public List<IPlayer> GetPlayers()
    {
        var list = new List<IPlayer>
        {
            _playerPeople
        };

        list.AddRange(_playerBots.Take(_storePlayersCountInfoProvider.PlayersCount - 1));

        return list;
    }

    private readonly string[] _nicknames = new string[]
{
    "AceMaster", "LuckyDraw", "CardShark", "KingPin", "QueenBee",
    "JackAttack", "RoyalFlush", "PocketRocket", "HighRoller", "ChipsChamp",
    "FastHand", "BluffWizard", "DealMaker", "StackLord", "ShuffleKing",
    "CardNinja", "ChipMagnet", "PocketAce", "RiverRunner", "DeckHero",
    "CardJuggler", "HandRaiser", "FlushFan", "StraightShooter", "PairUp",
    "BetBuddy", "RaiseRuler", "FoldMaster", "AllInAce", "ChipStacker",
    "DeckSlayer", "CardCrafter", "LuckyHand", "ShuffleMaster", "BluffKing",
    "RiverQueen", "PocketPrince", "HighCardHero", "AceHunter", "CardCaptain",
    "StackWizard", "RoyalGambler", "ChipCommander", "DeckChampion", "BluffNinja",
    "AllInMaster", "HandHero", "CardTiger", "FlushLord", "RaiseRanger"
};
}
