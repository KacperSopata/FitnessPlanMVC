//using FitnessPlanMVC.Application.Interfaces;
//using FitnessPlanMVC.Domain.Interfaces;
//using FitnessPlanMVC.Domain.Model.FitnessPlanMVC.Domain.Model;

//public class ChallengeProgressService : IChallengeProgressService
//{
//    private readonly IChallengeProgressRepository _challengeProgressRepository;
//    private readonly IChallengeRepository _challengeRepository;

//    public ChallengeProgressService(IChallengeProgressRepository challengeProgressRepository, IChallengeRepository challengeRepository)
//    {
//        _challengeProgressRepository = challengeProgressRepository;
//        _challengeRepository = challengeRepository;
//    }

//    // Sprawdzamy, czy użytkownik już dodał postęp w tym dniu
//    public ChallengeProgress GetProgressForToday(string userId, int challengeId)
//    {
//        return _challengeProgressRepository.GetProgressForToday(userId, challengeId);
//    }



//    public void AddProgress(int challengeId, string userId)
//    {
//        var challenge = _challengeRepository.GetById(challengeId); // Pobierz wyzwanie
//        if (challenge == null)
//        {
//            throw new Exception("Challenge not found.");
//        }

//        // Logowanie przed dodaniem
//        Console.WriteLine($"User {userId} is attempting to add progress for Challenge {challengeId}");

//        // Sprawdzamy, czy użytkownik dodał już postęp dzisiaj
//        var progressForToday = GetProgressForToday(userId, challengeId);
//        if (progressForToday != null)
//        {
//            throw new Exception("You have already added progress today.");
//        }

//        var totalProgress = _challengeProgressRepository.GetTotalProgress(userId, challengeId);

//        var newProgress = new ChallengeProgress
//        {
//            ChallengeId = challengeId,
//            UserId = userId,
//            ProgressDate = DateTime.Now,
//            Progress = totalProgress + 1, // Zwiększamy o 1
//            IsCompleted = totalProgress + 1 >= challenge.DurationInDays // Sprawdzamy, czy ukończono
//        };

//        // Ustawienie daty ukończenia, jeśli osiągnięto wymagany postęp
//        if (newProgress.IsCompleted)
//        {
//            newProgress.CompletionDate = DateTime.Now;
//        }

//        _challengeProgressRepository.AddProgress(newProgress); // Dodanie postępu
//    }
//}