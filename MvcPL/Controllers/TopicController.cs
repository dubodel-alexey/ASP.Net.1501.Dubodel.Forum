using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bll.Interface;
using Bll.Interface.Entities;
using MvcPL.ViewModels;

namespace MvcPL.Controllers
{// check if user is creator or admin
    public class TopicController : Controller
    {
        //
        // GET: /Topic/
        private IUserService userService;
        private ITopicService topicService;
        private ICommentService commentService;
        private ICategoryService categoryService;
        private IRoleService roleService;
        public TopicController(IUserService userService, ITopicService topicService,
            ICommentService commentService, ICategoryService categoryService, IRoleService roleService)
        {
            this.userService = userService;
            this.topicService = topicService;
            this.commentService = commentService;
            this.categoryService = categoryService;
            this.roleService = roleService;
        }

        public ActionResult Index(int id)
        {//check forRegistered topic
            BllTopic topic = topicService.GetById(id);
            BllCategory parentCategory = categoryService.GetById(topic.CategoryId);
            var parentViewModel = new CategoryHeadViewModel
            {
                Id = parentCategory.Id,
                Name = parentCategory.Name,
                ForRegistered = parentCategory.ForRegistered
            };
            var topicViewModel = new TopicViewModel { Id = topic.Id, Name = topic.Name, ParentCategory = parentViewModel };

            return View(topicViewModel);
        }

        [Authorize]
        public ActionResult Create(int id)
        {
            ViewBag.ParentCategoryId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(TopicCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newTopic = new BllTopic
                {
                    Name = viewModel.Name,
                    CategoryId = viewModel.ParentCategoryId,
                    ForRegistered = categoryService.GetById(viewModel.ParentCategoryId).ForRegistered
                };

                var newComment = new BllComment
                {
                    Body = viewModel.CommentBody,
                    Time = DateTime.Now,
                    UserId = userService.GetByLogin(User.Identity.Name).Id
                };

                topicService.Create(newTopic, newComment); // return id
                return RedirectToAction("Index", "Category"); // redirect to topic controller
            }

            return View(viewModel);
        }


        [Authorize]
        public ActionResult Edit(int id)
        {//check if creator is this users
            BllTopic topic = topicService.GetById(id);
            var viewModel = new TopicUpdateViewModel
            {
                Name = topic.Name,
                Id = id
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(TopicUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var topic = topicService.GetById(viewModel.Id);
                topicService.Update(new BllTopic
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    ForRegistered = topic.ForRegistered,
                    CategoryId = topic.CategoryId
                });
                return RedirectToAction("Index", new { id = viewModel.Id });
            }
            return View();
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            BllTopic topic = topicService.GetById(id);
            topicService.Delete(id);
            return RedirectToAction("Index", "Category", new { id = topic.CategoryId });
        }

        public ActionResult CommentsList(int id)
        {   // add getbytopicId in commentService
            // paged. change skip-take, to pageNumber-countPerPage
            var bllTopic = topicService.GetById(id);
            var bllComments = commentService.GetAllByTopic(bllTopic);
            var viewModel = (from bllComment in bllComments
                             let user = userService.GetById(bllComment.UserId)
                             let userViewModel = new UserViewModel
                             {
                                 Id = user.Id,
                                 Login = user.Login,
                                 RegisteredTime = user.RegistrationDateTime,
                                 Role = roleService.GetById(user.RoleId).Name
                             }
                             select new CommentViewModel
                             {
                                 Id = bllComment.Id,
                                 Body = bllComment.Body,
                                 Time = bllComment.Time,
                                 User = userViewModel
                             }).ToList();

            return PartialView(viewModel);
        }
    }
}
