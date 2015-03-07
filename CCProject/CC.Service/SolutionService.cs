using System.Linq;
using CC.Domain.Entities;
using CC.Domain.Repositories;

namespace CC.Service
{
    public interface ISolutionService
    {
        Solution ById(int id);
        void Save(Solution solution);
        void SaveFile(File file);
    }

    public class SolutionService : ISolutionService
    {
        public ISolutionRepository SolutionRepository { get; set; }

        public SolutionService(ISolutionRepository solutionRepository)
        {
            SolutionRepository = solutionRepository;
        }

        public Solution ById(int id)
        {
            return SolutionRepository.Solutions.FirstOrDefault(s => s.Id == id);
        }

        public void Save(Solution solution)
        {
            SolutionRepository.Save(solution);
        }

        public void SaveFile(File file)
        {
            SolutionRepository.SaveFile(file);
        }
    }
}