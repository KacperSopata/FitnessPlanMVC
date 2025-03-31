using FitnessPlanMVC.Domain.Interfaces;
using FitnessPlanMVC.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace FitnessPlanMVC.Infrastructure.Repositories
{
    public class ChallengeRepository : IChallengeRepository
    {
        private readonly FitnessPlanMVCDbContext _context;

        public ChallengeRepository(FitnessPlanMVCDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Challenge> GetAll()
        {
            return _context.Challenges.ToList();
        }

        public Challenge GetById(int id)
        {
            return _context.Challenges.FirstOrDefault(c => c.Id == id);
        }

        public int Add(Challenge challenge)
        {
            _context.Challenges.Add(challenge);
            _context.SaveChanges();
            return challenge.Id;
        }

        public void Update(Challenge challenge)
        {
            _context.Challenges.Update(challenge);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var challenge = _context.Challenges.FirstOrDefault(c => c.Id == id);
            if (challenge != null)
            {
                _context.Challenges.Remove(challenge);
                _context.SaveChanges();
            }
        }
    }
}
