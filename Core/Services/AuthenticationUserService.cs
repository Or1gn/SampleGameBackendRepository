using Core.Core;
using Core.Extentions;
using Core.Interfaces;
using Core.Repositories.Interfaces;
using System.Security.Cryptography;

namespace Core.Services {
    public class AuthenticationUserService : IAuthenticationUserService {   
        public IUnitOfWork UnitOfWork { get; }
        public IUserRepository UserRepository { get; }

        private const int saltSize = 16;    
        private const int keySize = 32;
        private const int iterations = 10000;

        public AuthenticationUserService(IUnitOfWork unitOfWork, IUserRepository userRepository) {
            UnitOfWork = unitOfWork;
            UserRepository = userRepository;
        }

        public User? LoginUser(string name, string password) {
            var user = UserRepository.Find(x => x.name.Equals(name)).FirstOrDefault();

            if (user == null) throw new Exception("Пользователь не найден!");
            if (!VerifyPassword(password, user.password)) throw new Exception("Неверный пароль!");

            return user;
        }

        public User RegisterUser(string name, string password) {
            var user = UserRepository.Find(x => x.name.Equals(name)).FirstOrDefault();

            if (user != null) throw new Exception("Пользователь с таким именем уже существует!");

            User createdUser = new User(Guid.NewGuid(), name, HashPassword(password));

            UserRepository.Add(createdUser);
            UnitOfWork.Complete();

            return createdUser;
        }

        /// <summary>
        /// Хэширует пароль пользователя
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string HashPassword(string password) {
            using (var rng = new RNGCryptoServiceProvider()) {
                byte[] salt = new byte[saltSize];
                rng.GetBytes(salt);

                var pdkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
                byte[] hash = pdkdf2.GetBytes(keySize);

                byte[] hashBytes = new byte[saltSize + keySize];
                Buffer.BlockCopy(salt, 0, hashBytes, 0, saltSize);
                Buffer.BlockCopy(hash, 0, hashBytes, saltSize, keySize);

                return Convert.ToBase64String(hashBytes);
            }
        }

        /// <summary>
        /// Проверяет пароль 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        private static bool VerifyPassword(string password, string hashedPassword) {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[saltSize];
            Buffer.BlockCopy(hashBytes, 0, salt, 0, saltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(keySize);

            for (int i = 0; i < keySize; i++) {
                if (hashBytes[i + saltSize] != hash[i]) {
                    return false;
                }
            }

            return true;
        }
    }
}
