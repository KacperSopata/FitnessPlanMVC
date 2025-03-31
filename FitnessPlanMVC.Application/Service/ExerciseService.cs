using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FitnessPlanMVC.Application.Interfaces;
using FitnessPlanMVC.Application.ViewModels.Exercise;
using FitnessPlanMVC.Domain.Interfaces;

namespace FitnessPlanMVC.Application.Service
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository _exerciseRepo;
        private readonly IMapper _mapper;

        public ExerciseService(IExerciseRepository exerciseRepository, IMapper mapper)
        {
            _exerciseRepo = exerciseRepository;
            _mapper = mapper;
        }
        public ListExerciseForListVm GetAllExercisesForList(int pageSize, int pageNO, string searchString)
        {
            var exercise = _exerciseRepo.GetAllExercises().Where(p => p.Name.StartsWith(searchString))
             .ProjectTo<FitnessPlanMVC.Application.ViewModels.Exercise.ExerciseForListVm>(_mapper.ConfigurationProvider).ToList();
            var exerciseToShow = exercise.Skip(pageSize * (pageNO - 1)).Take(pageSize).ToList();
            var exerciseList = new ListExerciseForListVm()
            {
                PageSize = pageSize,
                CurrentPage = pageNO,
                SearchString = searchString,
                ExerciseForListVm = exerciseToShow,
                Count = exercise.Count
            };
            return exerciseList;
        }
        public ListExerciseForListVm GetAllExercisesForList2()
        {
            var exercise = _exerciseRepo.GetAllExercises()
             .ProjectTo<FitnessPlanMVC.Application.ViewModels.Exercise.ExerciseForListVm>(_mapper.ConfigurationProvider).ToList();
            var exerciseList = new ListExerciseForListVm()
            {
                ExerciseForListVm = exercise
            };

            return exerciseList;
        }
        public ExerciseDetailVm GetExerciseDetail(int id)
        {
            var exercise = _exerciseRepo.GetDetail(id);
            var exerciseVm = _mapper.Map<ExerciseDetailVm>(exercise);
            return exerciseVm;
        }
        public ExerciseDetailVm GetExerciseDetailByWorkoutExercise(int id)
        {
            var exercise = _exerciseRepo.GetDetailByWorkoutExercise(id);
            var exerciseVm = _mapper.Map<ExerciseDetailVm>(exercise);
            return exerciseVm;
        }
    }
}