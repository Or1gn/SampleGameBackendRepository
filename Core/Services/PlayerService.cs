using Core.Core;
using Core.Extentions;
using Core.Interfaces;
using Core.Repositories.Interfaces;
using static Core.DTO.EntitiesDTO;

namespace Core.Services {
    public class PlayerService : IPlayerInfoService {
        public IUnitOfWork UnitOfWork { get; }
        public IPlayerRepository PlayerRepository { get; }
        public PlayerService(IUnitOfWork unitOfWork, IPlayerRepository playerRepository) { 
            UnitOfWork = unitOfWork;
            PlayerRepository = playerRepository;
        }

        public PlayerInfoDto? GetPlayerInfo(Guid playerId) {
            return PlayerRepository.Get(playerId)?.AsDto();       
        }

        public PlayerInfoDto SetPlayerInfo(string playerName, Guid userId) {
            PlayerInfo? existingPlayer = PlayerRepository.Find(x => x.PlayerName.Equals(playerName)).FirstOrDefault();

            if (existingPlayer != null) throw new Exception("Персонаж с таким именем уже существует!");

            var playerInfo = new PlayerInfo(Guid.NewGuid(), playerName, 10, 100, 10, 1000, userId) { PlayerLevel = 1 };
            PlayerRepository.Add(playerInfo);   

            UnitOfWork.Complete();

            return playerInfo.AsDto();
        }

        public PlayerInfoDto ChangePlayerName(Guid playerId, string newPlayerName) {
            var player = PlayerRepository.Get(playerId);

            if (player == null) throw new Exception("Персонаж не найден!");

            var playerWithUpdatedName = player with { PlayerName = newPlayerName };
            PlayerRepository.Update(playerWithUpdatedName);

            UnitOfWork.Complete();

            return playerWithUpdatedName.AsDto();
        }

        public IEnumerable<PlayerInfoDto> GetPlayers(Guid userId)
        {
            return PlayerRepository.Find(x => x.UserId == userId).Select(x => x.AsDto()).ToList();
        }
    }
}
