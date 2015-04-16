using System.Collections.Generic;
using MSHRCS.BusinessLogic.DataModel;
using MSHRCS.BusinessLogic.Repository;
using MSHRCS.BusinessLogic.Services.Interfaces;

namespace MSHRCS.BusinessLogic.Services.Classes
{
	public class LessonsService : ILessonService
	{
		private readonly IRepository<Lesson> _lessonRepository;
		private readonly IRepository<LessonType> _lessonTypeRepository;

		public LessonsService(IRepository<Lesson> lessonRepository, IRepository<LessonType> lessonTypeRepository)
		{
			_lessonRepository = lessonRepository;
			_lessonTypeRepository = lessonTypeRepository;
		}

		public IEnumerable<Lesson> GetLessons()
		{
			return _lessonRepository.GetAll();
		}

		public IEnumerable<LessonType> GetLessonTypes()
		{
			return _lessonTypeRepository.GetAll();
		}
	}
}
