using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CC.Domain.Entities;
using CC.Domain.Repositories;

namespace CC.Service
{
    public interface IProblemService
    {
        Problem ById(int id);
        void Save(Problem problem);
        IEnumerable<Problem> All();
    }

    public class ProblemService : IProblemService
    {
        public IProblemRepository ProblemRepository { get; set; }

        public ProblemService(IProblemRepository problemRepository)
        {
            ProblemRepository = problemRepository;
        }

        public Problem ById(int id)
        {
            return ProblemRepository.Problems.FirstOrDefault(p => p.Id == id);
        }

        public void Save(Problem problem)
        {
            ProblemRepository.Save(problem);
        }

        public IEnumerable<Problem> All()
        {
            return ProblemRepository.Problems.ToList();
        }
    }
}
