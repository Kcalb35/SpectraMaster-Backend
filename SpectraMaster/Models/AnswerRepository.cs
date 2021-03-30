using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpectraMaster.Data;

namespace SpectraMaster.Models
{
    public class AnswerRepository:IAnswerRepository
    {
        private readonly AnswerDbContext _context;

        public AnswerRepository(AnswerDbContext context)
        {
            _context = context;
        }
        
        public async Task<SpectraAnswer> CreateAnswer(string probDescription, string ansDescription,
            List<string> probPics, List<string> ansPics, float ionPeak = 0,
            FormulaConfig formula = null)
        {
            if (formula == null && ionPeak <0.01) return null;
            var probPictures = from pic in probPics
                select new ProblemPicture(pic);
            var ansPictures = from pic in probPics
                select new AnswerPicture(pic);
            var answer = new SpectraAnswer(probDescription, ansDescription,probPictures,ansPictures);
            if (formula != null)
            {
                // NMR
                // create NMR problem
                var nmr = new NMRProblem(formula);
                // create problem-answer
                var nmrProbAns = new NMRProblemAnswer(nmr,answer);
                // put prob-ans into dbset
                await _context.NmrProblemAnswers.AddAsync(nmrProbAns);
            }

            if (ionPeak > 0.01)
            {
                // Mass
                // create Mass problem
                var massProb = new MassProblem(ionPeak);
                // create problem-answer
                var massProbAns = new MassProblemAnswer(massProb, answer);
                // put prob-ans into dbset
                await _context.MassProblemAnswers.AddAsync(massProbAns);
            }
            return answer;
        }

        public async Task<bool> DeleteAnswerById(int id)
        {
            var ans = RetrieveAnswerById(id);
            if (ans != null)
            {
                _context.SpectraAnswers.Remove(ans);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<SpectraAnswer> UpdateNmrAnswer(int id, string probDescription, string ansDescription,
            List<string> probPics, List<string> ansPics,
            float ionPeak = 0, FormulaConfig formula = null)
        {
            throw new System.NotImplementedException();
        }

        public SpectraAnswer RetrieveAnswerById(int id)
        {
            return  _context.SpectraAnswers.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<SpectraAnswer> RetrieveMassAnswer(float minIonPeak, float maxIonPeak)
        {
            var ans = from answer in _context.SpectraAnswers
                where answer.MassProblemAnswer != null
                let peak = answer.MassProblemAnswer.MassProblem.IonPeak
                where peak >= minIonPeak && peak <= maxIonPeak
                select answer;
            return ans;
        }

        public IEnumerable<SpectraAnswer> RetrieveNmrAnswer(FormulaConfig formula)
        {
            if (formula == null) return null;
            var ans = from answer in _context.SpectraAnswers
                let nmr = answer.NmrProblemAnswer.NmrProblem
                where nmr.IsEqual(formula)
                select answer;
            return ans;
        }

        public IEnumerable<SpectraAnswer> RetrieveComplexAnswer(FormulaConfig formula, float minIonPeak, float maxIonPeak)
        {
            if (formula == null || maxIonPeak < minIonPeak || minIonPeak < 0.01 || maxIonPeak < 0.01) return null;
            var ans = from answer in _context.SpectraAnswers
                where answer.MassProblemAnswer != null && answer.NmrProblemAnswer != null
                let nmr = answer.NmrProblemAnswer.NmrProblem
                let peak = answer.MassProblemAnswer.MassProblem.IonPeak
                where peak >= minIonPeak && peak <= maxIonPeak && nmr.IsEqual(formula)
                select answer;
            return ans;
        }

        public IEnumerable<SpectraAnswer> RetrieveAllAnswers()
        {
            return _context.SpectraAnswers.Select(x=>x);
        }
    }
}