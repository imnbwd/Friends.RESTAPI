using Friends.Models;
using System;
using System.Collections.Generic;

namespace Friends.Services
{
    public interface IFriendRepository
    {
        IEnumerable<Friend> GetFriends();
        Friend GetFriend(Guid friendId);

        void AddFriend(Friend friend);

        bool IsFriendExist(Guid friendId);

        bool Save();

    }
}