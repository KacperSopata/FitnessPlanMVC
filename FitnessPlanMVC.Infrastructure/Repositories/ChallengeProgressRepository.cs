//using FitnessPlanMVC.Domain.Interfaces;
//using FitnessPlanMVC.Domain.Model;
//using FitnessPlanMVC.Domain.Model.FitnessPlanMVC.Domain.Model;
//using FitnessPlanMVC.Infrastructure;

//public class ChallengeProgressRepository : IChallengeProgressRepository
//{
//    private readonly FitnessPlanMVCDbContext _context;

//    public ChallengeProgressRepository(FitnessPlanMVCDbContext context)
//    {
//        _context = context;
//    }

//    public ChallengeProgress GetProgressForToday(string userId, int challengeId)
//    {
//        return _context.ChallengeProgresses
//            .FirstOrDefault(cp => cp.UserId == userId && cp.ChallengeId == challengeId && cp.ProgressDate.Date == DateTime.Now.Date);
//    }

//    public void AddProgress(ChallengeProgress progress)
//    {
//        _context.ChallengeProgresses.Add(progress);
//        _context.SaveChanges();
//    }
//    public int GetTotalProgress(string userId, int challengeId)
//    {
//        return _context.ChallengeProgresses
//            .Where(p => p.UserId == userId && p.ChallengeId == challengeId)
//            .Sum(p => p.Progress);
//    }
//    public UserChallenge GetUserChallenge(string userId, int challengeId)
//    {
//        return _context.UserChallenges
//                       .FirstOrDefault(uc => uc.UserId == userId && uc.ChallengeId == challengeId);
//    }
//    public void UpdateUserChallenge(UserChallenge userChallenge)
//    {
//        _context.UserChallenges.Update(userChallenge); // Aktualizujemy UserChallenge
//        _context.SaveChanges(); // Zapisujemy zmiany w bazie
//    }
//}
