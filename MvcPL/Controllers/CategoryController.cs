using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bll.Interface;
using Bll.Interface.Entities;
using MvcPL.ViewModels;

namespace MvcPL.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        private IUserService userService;
        private ICategoryService categoryService;
        private ITopicService topicService;
        private ICommentService commentService;
        public CategoryController(IUserService userService, ICategoryService categoryService,
            ITopicService topicService, ICommentService commentService)
        {
            this.userService = userService;
            this.categoryService = categoryService;
            this.topicService = topicService;
            this.commentService = commentService;
        }

        public ActionResult Index(int? id)
        {//check for registered 
            var category = new CategoryViewModel();
            if (id.HasValue)
            {
                var bllCategory = categoryService.GetById(id.Value);
                if (bllCategory == null)
                {
                    return HttpNotFound();
                }
                if (bllCategory.ForRegistered && !User.Identity.IsAuthenticated)
                {
                    return RedirectToAction("Index", "Category");
                }
                CategoryHeadViewModel parentCategory = null;
                if (bllCategory.ParentCategoryId.HasValue)
                {
                    var bllParentCategory = categoryService.GetById(bllCategory.ParentCategoryId.Value);
                    parentCategory = new CategoryHeadViewModel
                    {
                        Id = bllParentCategory.Id,
                        Name = bllParentCategory.Name,
                        ForRegistered = bllParentCategory.ForRegistered
                    };
                }

                category.Name = bllCategory.Name;
                category.Id = bllCategory.Id;
                category.ParentCategory = parentCategory;
            }

            return View(category);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create(int? id)
        {
            ViewBag.ParentCategoryId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Create(CategoryCreateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                categoryService.Create(new BllCategory
                {
                    Name = viewModel.Name,
                    ForRegistered = viewModel.ForRegistered,
                    ParentCategoryId = viewModel.ParentCategoryId
                });
                return RedirectToAction("Index", new { id = viewModel.ParentCategoryId });
            }
            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int id)
        {
            BllCategory category = categoryService.GetById(id);
            var viewModel = new CategoryUpdateViewModel
            {
                Id = category.Id,
                Name = category.Name,
                ForRegistered = category.ForRegistered,
                ParentCategoryId = category.ParentCategoryId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(CategoryUpdateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                categoryService.Update(new BllCategory
                {
                    Id = viewModel.Id,
                    Name = viewModel.Name,
                    ForRegistered = viewModel.ForRegistered,
                    ParentCategoryId = viewModel.ParentCategoryId
                });
                return RedirectToAction("Index", new { id = viewModel.ParentCategoryId });
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            BllCategory category = categoryService.GetById(id);
            categoryService.Delete(category);
            return RedirectToAction("Index", new { id = category.ParentCategoryId });
        }

        public ActionResult CategoriesList(int? id)
        {
            IEnumerable<CategoryHeadViewModel> categories;
            if (id.HasValue)
            {
                var category = categoryService.GetById(id.Value);
                categories = categoryService.GetCategories(category)
                    .Select(x => new CategoryHeadViewModel { Name = x.Name, Id = x.Id, ForRegistered = x.ForRegistered });
            }
            else
            {
                categories = categoryService.GetRootCategories()
                    .Select(x => new CategoryHeadViewModel { Name = x.Name, Id = x.Id, ForRegistered = x.ForRegistered });
            }
            return PartialView(categories);
        }

        public ActionResult TopicsList(int? id)
        {
            List<TopicHeadViewModel> topics = null;
            if (id.HasValue)
            {
                var category = categoryService.GetById(id.Value);
                var blltopics = topicService.GetAllByCategory(category);
                if (blltopics != null)
                {
                    topics = new List<TopicHeadViewModel>();
                    foreach (var blltopic in blltopics)
                    {
                        // add "GetCreator" method in topicService and remove commentService from controller
                        string creatorLogin = userService.GetById(commentService.GetFirstInTopic(blltopic).UserId).Login;
                        topics.Add(new TopicHeadViewModel { CreatorLogin = creatorLogin, Name = blltopic.Name, Id = blltopic.Id });
                    }
                }
            }
            return PartialView(topics);
        }
    }
}
