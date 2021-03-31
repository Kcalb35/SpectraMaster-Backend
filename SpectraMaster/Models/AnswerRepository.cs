using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpectraMaster.Controllers;
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
        
        public async Task<SpectraAnswer> CreateAnswerAsync(string probDescription, string ansDescription,
            List<string> probPics, List<string> ansPics, FormulaConfig formula, float ionPeak = -1)
        {
            if (!formula.IsValid() && ionPeak <0) return null;
            var probPictures = probPics.Select(pic => new ProblemPicture(pic));
            var ansPictures = ansPics.Select(pic => new AnswerPicture(pic));
            var answer = new SpectraAnswer(probDescription, ansDescription);
            if (formula.IsValid())
            {
                // NMR
                var nmrProb = new NMRProblem(formula);
                nmrProb.Answer = answer;
                await _context.NmrProblems.AddAsync(nmrProb);
            }

            if (ionPeak >=0)
            {
                // Mass
                var massProb = new MassProblem(ionPeak);
                massProb.Answer = answer;
                await _context.MassProblems.AddAsync(massProb);
            }

            // add pictures to DB set
            foreach (var pic in ansPictures)
            {
                pic.SpectraAnswer = answer;
                await _context.AnswerPics.AddRangeAsync(pic);
            }
            foreach (var pic in probPictures)
            {
                pic.SpectraAnswer = answer;
                await _context.ProblemPics.AddAsync(pic);
            }
            await _context.SaveChangesAsync();
            return RetrieveAnswerById(answer.Id);
        }

        public async Task<bool> DeleteAnswerById(int id)
        {
            var ans = RetrieveAnswerById(id);
            if (ans == null) return false;
            _context.Remove(ans);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNmrProblemById(int id)
        {
            var prob = RetrieveNmrProblemById(id);
            if (prob == null) return false;
            _context.Remove(prob);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMassProblemById(int id)
        {
            var prob = RetrieveMassProblemById(id);
            if (prob == null) return false;
            _context.Remove(prob);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SpectraAnswer> UpdateAnswerAsync(int id, string probDescription, string ansDescription,
            List<string> probPics, List<string> ansPics,
            float ionPeak = -1, FormulaConfig formula = null)
        {
            var ans = RetrieveAnswerById(id);
            if (ans == null || (ionPeak<0 && (formula==null || !formula.IsValid()))) return null;
            
            ans.ProblemDescription = probDescription;
            ans.AnswerDescription = ansDescription;
            var probPictures = probPics.Select(pic => new ProblemPicture(pic) {SpectraAnswer = ans}).ToList();
            var ansPictures = ansPics.Select(pic => new AnswerPicture(pic) {SpectraAnswer = ans}).ToList();
            
            // delete old pictures
            _context.AnswerPics.RemoveRange(ans.AnswerPictures);
            _context.ProblemPics.RemoveRange(ans.ProblemPictures);
            
            // delete old nmr or mass problem
            _context.MassProblems.Remove(ans.MassProblem);
            _context.NmrProblems.Remove(ans.NmrProblem);

            if (formula != null)
            {
                // nmr
                var nmrProblem = new NMRProblem(formula);
                nmrProblem.Answer = ans;
                await _context.NmrProblems.AddAsync(nmrProblem);
            }

            if (ionPeak >= 0)
            {
                // mass
                var massProblem = new MassProblem(ionPeak);
                massProblem.Answer = ans;
                await _context.MassProblems.AddAsync(massProblem);
            }
            
            // append new pictures
            await _context.AnswerPics.AddRangeAsync(ansPictures);
            await _context.ProblemPics.AddRangeAsync(probPictures);

            var answerEntity = _context.SpectraAnswers.Attach(ans);
            answerEntity.State = EntityState.Modified;
            
            await _context.SaveChangesAsync();
            return ans;
        }

        public SpectraAnswer RetrieveAnswerById(int id)
        {
            var ans= _context.SpectraAnswers
                .Include(a=>a.AnswerPictures)
                .Include(a=>a.ProblemPictures)
                .Include(a=>a.NmrProblem)
                .Include(a=>a.MassProblem)
                .FirstOrDefault(a => a.Id == id);
            return ans;
        }

        public MassProblem RetrieveMassProblemById(int id)
        {
            var prob = _context.MassProblems
                .Include(p => p.Answer)
                .Include(p => p.Answer.ProblemPictures)
                .FirstOrDefault(p => p.Id == id);
            return prob;
        }

        public NMRProblem RetrieveNmrProblemById(int id)
        {
            var prob = _context.NmrProblems
                .Include(p => p.Answer)
                .Include(p => p.Answer.ProblemPictures)
                .FirstOrDefault(p => p.Id == id);
            return prob;
        }

        public IEnumerable<SpectraAnswer> RetrieveAnswer(FormulaConfig formula, float minIonPeak, float maxIonPeak)
        {
            if (formula == null && (minIonPeak < 0 && maxIonPeak < 0 || minIonPeak > maxIonPeak)) return null;
            var ans = RetrieveAllAnswers();
            // nmr
            if (formula != null )
                ans = ans.Where(p => p.NmrProblem != null && p.NmrProblem.IsEqual(formula));
            // mass
            if (minIonPeak >= 0 && maxIonPeak >= 0)
                ans = ans.Where(p =>
                    p.MassProblem != null && p.MassProblem.IonPeak <= maxIonPeak &&
                    p.MassProblem.IonPeak >= minIonPeak);
            return ans;
        }

        public IEnumerable<SpectraAnswer> RetrieveAllAnswers()
        {
            return _context.SpectraAnswers
                .Include(a => a.ProblemPictures)
                .Include(a => a.AnswerPictures)
                .Include(a => a.NmrProblem)
                .Include(a => a.MassProblem);
        }

        public IEnumerable<MassProblem> RetrieveAllMassProblems()
        {
            return _context.MassProblems
                .Include(p => p.Answer)
                .Include(p => p.Answer.ProblemPictures);
        }

        public IEnumerable<NMRProblem> RetrieveAllNmrProblems()
        {
            return _context.NmrProblems
                .Include(p => p.Answer)
                .Include(p => p.Answer.ProblemPictures);
        }
    }
}