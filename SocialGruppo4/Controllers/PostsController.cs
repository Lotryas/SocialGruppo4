﻿using Microsoft.AspNetCore.Mvc;
using SocialGruppo4.Models.Post;
using Utility;

namespace SocialGruppo4.Controllers
{
    public class PostsController : Controller
    {
        public IActionResult Details(int id) => View(DAOPost.GetInstance().FullPost(id));
    }
}
