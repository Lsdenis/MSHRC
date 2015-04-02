using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class LessonsService : ILessonService
	{
		private readonly IRepository<Lesson> _lessonRepository;

		public LessonsService(IRepository<Lesson> lessonRepository)
		{
			_lessonRepository = lessonRepository;
		}

		public IEnumerable<Lesson> GetLessons()
		{
			return _lessonRepository.GetAll();
		}
	}
}
