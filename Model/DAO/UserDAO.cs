using Model.DAO.Enums;
using Model.Entity;
using System;
using System.Linq;
using Utilities;


namespace Model.DAO
{
    public class UserDAO
    {
        public MrKatsuWebDbContext db = null;
        public UserDAO()
        {
            db = new MrKatsuWebDbContext();
        }
        public long Insert(Account entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public Account GetUserByUserName(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.Username == userName);
        }
        public Account GetUserById(int id)
        {
            return db.Accounts.Find(id);
        }
        public int IsLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return -3;
            var result = GetUserByUserName(userName: username);
            if (result == null) return 0;
            else
            {
                if (result.Status == false) return -2;
                else
                {
                    //if (result.Password.Equals(Hash.GetInstance().HashPassword(password))) return 1;
                    if (result.Password.Equals(password)) return 1;
                    else return -1;
                }
            }
        }
        public LoginResult IsSignIn(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return LoginResult.InvalidInput;

            var result = GetUserByUserName(userName: username);
            if (result == null) return LoginResult.UserNotFound;

            switch (result.Status)
            {
                case false:
                    return LoginResult.UserInactive;
                case true:
                    //return Hash.GetInstance().VerifyPassword(result.Password, password)  
                    return result.Password.Equals(password)
                        ? LoginResult.Success
                        : LoginResult.PasswordIncorrect;
                default:
                    return LoginResult.Error;
            }
        }
    }
}
