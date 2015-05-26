using System.Data.Entity;
using BLL;
using Bll.Interface;
using DAl;
using DAL.Interface;
using DAL.Interface.Entities;
using DAl.Repositories;
using Ninject.Modules;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public class ResolverModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<ForumContext>().InRequestScope();
            Bind<IRepository<DalUser>>().To<UserRepository>().InRequestScope();
            Bind<IRepository<DalCategory>>().To<CategoryRepository>().InRequestScope();
            Bind<IRepository<DalComment>>().To<CommentRepository>().InRequestScope();
            Bind<IRepository<DalTopic>>().To<TopicRepository>().InRequestScope();
            Bind<IRepository<DalRole>>().To<RoleRepository>().InRequestScope();
            Bind<IRepository<DalLike>>().To<LikeRepository>().InRequestScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            Bind<IUserService>().To<UserService>().InRequestScope();
            Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            Bind<ICommentService>().To<CommentService>().InRequestScope();
            Bind<ITopicService>().To<TopicService>().InRequestScope();
            Bind<IRoleService>().To<RoleService>().InRequestScope();
        }
    }
}
