using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Classes;
using MSHRCS.BusinessLogic.Services.Interfaces;
using MSHRCS.BusinessLogic.UnitOfWork;
using MSHRCS.Presentation;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace MSHRCS.Presentation
{
	public static class NinjectWebCommon
	{
		private static readonly Bootstrapper bootstrapper = new Bootstrapper();

		/// <summary>
		/// Starts the application
		/// </summary>
		public static void Start()
		{
			DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
			DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
			bootstrapper.Initialize(CreateKernel);
		}

		/// <summary>
		/// Stops the application.
		/// </summary>
		public static void Stop()
		{
			bootstrapper.ShutDown();
		}

		/// <summary>
		/// Creates the kernel that will manage your application.
		/// </summary>
		/// <returns>The created kernel.</returns>
		private static IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			try
			{
				kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
				kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

				RegisterServices(kernel);
				return kernel;
			}
			catch
			{
				kernel.Dispose();
				throw;
			}
		}

		/// <summary>
		/// Load your modules or register your services here!
		/// </summary>
		/// <param name="kernel">The kernel.</param>
		private static void RegisterServices(IKernel kernel)
		{
			kernel.Bind<IRepository<AcademicDiscipline>>().To<BaseRepository<AcademicDiscipline>>().InRequestScope();
			kernel.Bind<IRepository<Cabinet>>().To<BaseRepository<Cabinet>>().InRequestScope();
			kernel.Bind<IRepository<GDCabinet>>().To<BaseRepository<GDCabinet>>().InRequestScope();
			kernel.Bind<IRepository<GDTeacher>>().To<BaseRepository<GDTeacher>>().InRequestScope();
			kernel.Bind<IRepository<Group>>().To<BaseRepository<Group>>().InRequestScope();
			kernel.Bind<IRepository<GroupDiscipline>>().To<BaseRepository<GroupDiscipline>>().InRequestScope();
			kernel.Bind<IRepository<Lesson>>().To<BaseRepository<Lesson>>().InRequestScope();
			kernel.Bind<IRepository<Teacher>>().To<BaseRepository<Teacher>>().InRequestScope();
			kernel.Bind<IRepository<User>>().To<BaseRepository<User>>().InRequestScope();

			kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
			kernel.Bind<MSHRCSchedulerContext>().ToSelf().InRequestScope();

			kernel.Bind<IAcademicDisciplineService>().To<AcademicDisciplineService>().InRequestScope();
			kernel.Bind<ICabinetService>().To<CabinetService>().InRequestScope();
			kernel.Bind<IGDCabinetService>().To<GDCabinetService>().InRequestScope();
			kernel.Bind<IGDTeacherService>().To<GDTeacherService>().InRequestScope();
			kernel.Bind<IGroupDisciplineService>().To<GroupDisciplineService>().InRequestScope();
			kernel.Bind<IGroupService>().To<GroupService>().InRequestScope();
			kernel.Bind<ILessonService>().To<LessonsService>().InRequestScope();
			kernel.Bind<ITeacherService>().To<TeacherService>().InRequestScope();
			kernel.Bind<IUserService>().To<UserService>().InRequestScope();
		}
	}
}
