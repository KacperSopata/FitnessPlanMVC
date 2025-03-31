using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessPlanMVC.Domain.Model;

namespace FitnessPlanMVC.Domain.Interfaces
{
    public interface IChallengeRepository
    {
        IEnumerable<Challenge> GetAll();
        Challenge GetById(int id);
        int Add(Challenge challenge);
        void Update(Challenge challenge);
        void Delete(int id);
    }
}
