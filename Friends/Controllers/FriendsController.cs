using Friends.Services;
using Microsoft.AspNetCore.Mvc;

namespace Friends.Controllers
{
    [Route("api/Friends")]
    public class FriendsController : Controller
    {
        private IFriendRepository _friendRepository;

        public FriendsController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        //public IActionResult AddFriend([FromBody] Friend friend)
        //{
        //}

        [HttpGet]
        public IActionResult GetFriends()
        {
            return Ok(_friendRepository.GetFriends());
        }
    }
}