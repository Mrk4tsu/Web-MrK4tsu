using Model.DAO.Enums;
using Model.Entity;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.RegularExpressions;
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
        public IEnumerable<Account> GetAll() => db.Accounts;
        public IEnumerable<Account> GetAll(string searchString, int page, int pageSize, bool status)
        {
            IQueryable<Account> query = db.Accounts;
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(x => x.Username.Contains(searchString) || x.Name.Contains(searchString));
            }
            return query.OrderByDescending(x => x.CreatedAt).Where(u => u.Status == status).ToPagedList(page, pageSize);
        }
        public long Insert(Account entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            db.Accounts.Add(entity);
            db.SaveChanges();
            return entity.Id;
        }
        public bool Update(Account entity)
        {
            try
            {
                var user = db.Accounts.Find(entity.Id);

                var currentPass = user.Password;
                // Kiểm tra nếu newPassword được nhập và nếu nó khác với mật khẩu cũ
                if (entity.Password != currentPass)
                    user.Password = Hash.GetInstance().HashPassword(entity.Password);
                else
                    user.Password = currentPass;

                user.Name = entity.Name;
                user.Username = entity.Username;
                user.Email = entity.Email;
                user.Phone = entity.Phone;
                user.Status = entity.Status;
                user.ModifyAt = DateTime.UtcNow;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                var user = GetUserById(id);

                db.Accounts.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public Account GetUserByUserName(string userName)
        {
            return db.Accounts.SingleOrDefault(x => x.Username == userName);
        }
        public Account GetUserById(long id)
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
                    return Hash.GetInstance().VerifyPassword(password, result.Password)  
                    //return result.Password.Equals(password)
                        ? LoginResult.Success
                        : LoginResult.PasswordIncorrect;
                default:
                    return LoginResult.Error;
            }
        }
        public bool IsEmailExist(string email)
        {
            var v = db.Accounts.Where(e => e.Email == email).FirstOrDefault();
            return v != null;
        }
        public bool IsUsenameExist(string username)
        {
            var v = db.Accounts.Where(e => e.Username == username).FirstOrDefault();
            return v != null;
        }
        public bool IsValidUsername(string username)
        {
            // Regex pattern cho username: bắt đầu bằng chữ cái, chỉ chứa chữ cái, số, dấu gạch dưới, dấu gạch ngang, có độ dài từ 3 đến 16 ký tự
            string pattern = @"^[a-zA-Z][a-zA-Z0-9_-]{5,20}$";

            // Kiểm tra sự khớp của username với pattern
            return Regex.IsMatch(username, pattern);
        }
    }
}
