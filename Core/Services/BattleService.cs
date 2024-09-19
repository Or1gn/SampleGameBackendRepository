using Core.Core;
using Core.Interfaces;
using Core.Repositories.Interfaces;

namespace Core.Services {
    public class BattleService : IBattleService {
        public IUnitOfWork UnitOfWork { get; }
        public IEnemyRepository EnemyRepository { get; }
        public IPlayerRepository PlayerRepository { get; }
        public BattleService(IUnitOfWork unitOfWork, IPlayerRepository playerRepository, IEnemyRepository enemyRepository) {
            UnitOfWork = unitOfWork;
            PlayerRepository = playerRepository;
            EnemyRepository = enemyRepository;
        }

        public List<Enemy> GetEnemies() {
            return EnemyRepository.GetAll().ToList();
        }

        public Enemy? GetEnemy(Guid enemyId) {
            return EnemyRepository.Get(enemyId);
        }

        /// <summary>
        /// Возвращает true если победа и false если поражение или ничья
        /// </summary>
        /// <param name="enemyId"></param>
        /// <returns></returns>
        public bool StartFigth(Guid enemyId, Guid playerId) {
            bool fightResult = false;

            var enemy = GetEnemy(enemyId);

            if (enemy == null) return fightResult;

            var player = PlayerRepository.Get(playerId);

            if (player == null) return fightResult;
  
            int playerHP = (int) player.Health;
            int enemyHP = (int) enemy.Health;

            while (playerHP >= 0) {
                int currentPlayerDamage = Math.Max((int) (player.Damage - enemy.Armor), 0);
                enemyHP -= currentPlayerDamage;

                if (enemyHP <= 0) {
                    fightResult = true;
                }

                int currentEnemyDamage = Math.Max((int)(enemy.Damage - player.Armor), 0);
                playerHP -= currentEnemyDamage;

                if (currentEnemyDamage == 0 && currentPlayerDamage == 0) return false;
            }

            PlayerInfo updatedPlayerHP = player;

            if (playerHP <= 0) {
                updatedPlayerHP = player with { Health = 0 };
                fightResult = false;
            }
            if (enemyHP <= 0) { 
                updatedPlayerHP = player with { Health = playerHP, Gold = player.Gold + enemy.Reward };
                fightResult = true;
            }

            PlayerRepository.Update(updatedPlayerHP);
            UnitOfWork.Complete();

            return fightResult;
        }
    }
}
