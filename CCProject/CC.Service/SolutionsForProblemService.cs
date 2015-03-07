using System.Collections.Generic;
using System.Linq;
using CC.Domain.Entities;
using CC.Domain.Repositories;

namespace CC.Service
{
    /**
    public interface ISolutionsForProblemService
    {
        SolutionsForProblem ById(int id);
        void AttachSolution(int id, Solution solution);
        IQueryable<Solution> GetSolutions(int id);
        void Save(SolutionsForProblem solutionsForProblem);
        Solution TopSolution(int id);
        Problem GetProblem(int id);
        IEnumerable<Solution> SolutionsFromTeam(int id);
    }

    public class SolutionsForProblemService : ISolutionsForProblemService
    {
        public ISolutionsForProblemRepository SolutionsForProblemRepository { get; set; }
        public IProblemService ProblemService { get; set; }

        public SolutionsForProblemService(ISolutionsForProblemRepository solutionsForProblemRepository, IProblemService problemService)
        {
            SolutionsForProblemRepository = solutionsForProblemRepository;
            ProblemService = problemService;
        }

        public SolutionsForProblem ById(int id)
        {
            return SolutionsForProblemRepository.SolutionsForProblems.FirstOrDefault(s => s.Id == id);
        }

        public void AttachSolution(int id, Solution solution)
        {
            var solutionsForProblem = ById(id);
            solutionsForProblem.Solutions.Add(solution);
            Save(solutionsForProblem);
        }

        public IQueryable<Solution> GetSolutions(int id)
        {
            return ById(id).Solutions.AsQueryable();
        } 

        public void Save(SolutionsForProblem solutionsForProblem)
        {
            SolutionsForProblemRepository.Save(solutionsForProblem);
        }

        public Solution TopSolution(int id)
        {
            var solutions = GetSolutions(id);
            var max = solutions.Max(s => s.Score);
            return solutions.FirstOrDefault(s => s.Score == max);
        }

        public Problem GetProblem(int id)
        {
            return ProblemService.ById(ById(id).ProblemId);
        }

        public IEnumerable<Solution> SolutionsFromTeam(int id)
        {
            var solutionsForProblem = ById(id);
            return solutionsForProblem.Solutions.OrderBy(s => s.SubmissionTime);
        }
    }
     */
}
