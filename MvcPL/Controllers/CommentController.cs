using System;
using System.Web.Mvc;
using Bll.Interface;
using Bll.Interface.Entities;
using MvcPL.ViewModels;

namespace MvcPL.Controllers
{
    public class CommentController : Controller
    {
        //
        // GET: /Comment/

        private IUserService userService;
        private ITopicService topicService;
        private ICommentService commentService;
        private ICategoryService categoryService;
        private IRoleService roleService;
        public CommentController(IUserService userService, ITopicService topicService,
            ICommentService commentService, ICategoryService categoryService, IRoleService roleService)
        {
            this.userService = userService;
            this.topicService = topicService;
            this.commentService = commentService;
            this.categoryService = categoryService;
            this.roleService = roleService;
        }

        public ActionResult Id(int id)
        {
            BllComment bllComment = commentService.GetById(id);
            BllUser bllUser = userService.GetById(bllComment.UserId);
            BllTopic blltopic = topicService.GetById(bllComment.TopicId);

            var commentViewModel = new CommentFullViewModel
            {
                Id = bllComment.Id,
                Body = bllComment.Body,
                Time = bllComment.Time,
                Topic = new TopicHeadViewModel { Id = blltopic.Id, Name = blltopic.Name },
                User = new UserViewModel
                {
                    Id = bllUser.Id,
                    Login = bllUser.Login,
                    RegisteredTime = bllUser.RegistrationDateTime,
                    Role = roleService.GetById(bllUser.RoleId).Name
                }
            };
            return View(commentViewModel);
        }

        [ChildActionOnly]
        public ActionResult Create(int topicId)
        {
            ViewBag.topicId = topicId;
            return PartialView("_CreateComment");
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentCreateViewModel comment)
        {
            if (ModelState.IsValid)
            {
                var bllComment = new BllComment
                {
                    Body = comment.CommentBody,
                    Time = DateTime.Now,
                    TopicId = comment.TopicId,
                    UserId = userService.GetByLogin(User.Identity.Name).Id
                };
                commentService.Create(bllComment);
                return RedirectToAction("Index", "Topic", new { id = comment.TopicId });
            }
            return PartialView("_CreateComment", comment);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            BllComment comment = commentService.GetById(id);
            commentService.Delete(comment);
            return RedirectToAction("Index", "Topic", new { id = comment.TopicId });
        }
    }
}
