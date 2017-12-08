using Friends.Data;
using Friends.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Friends.Services
{
    public class FriendRepository : IFriendRepository
    {
        private FriendDbContext _dbContext;

        public FriendRepository(FriendDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddFriend(Friend friend)
        {
            friend.Id = Guid.NewGuid();
            _dbContext.Friends.Add(friend);
        }

        public Friend GetFriend(Guid friendId)
        {
            return _dbContext.Friends.FirstOrDefault(f => f.Id == friendId);
        }

        public IEnumerable<Friend> GetFriends()
        {
            return _dbContext.Friends.ToList();
        }

        public bool IsFriendExist(Guid friendId)
        {
            return _dbContext.Friends.Any(f => f.Id == friendId);
        }

        public bool Save()
        {
            return _dbContext.SaveChanges() > 0;
        }
    }
}