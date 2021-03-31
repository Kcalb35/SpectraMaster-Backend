using System.Collections.Generic;
using System.Threading.Tasks;
using SpectraMaster.AnswerController.Dto;
using SpectraMaster.Controllers;
using SpectraMaster.Data;

namespace SpectraMaster.AnswerController.Dto
{
}

namespace SpectraMaster.Models
{
    public interface IAnswerRepository
    {
        Task<SpectraAnswer> CreateAnswerAsync(string probDescription, string ansDescription, List<string> probPics,
            List<string> ansPics,FormulaConfig formula,float ionPeak=-1);

        Task<bool> DeleteAnswerById(int id);
        Task<bool> DeleteNmrProblemById(int id);
        Task<bool> DeleteMassProblemById(int id);
        
        Task<SpectraAnswer> UpdateAnswerAsync(int id,string probDescription, string ansDescription, List<string> probPics,
            List<string> ansPics,float ionPeak=-1,FormulaConfig formula=null);

        SpectraAnswer RetrieveAnswerById(int id);
        MassProblem RetrieveMassProblemById(int id);
        NMRProblem RetrieveNmrProblemById(int id);
        IEnumerable<SpectraAnswer> RetrieveAnswer(FormulaConfig formula, float minIonPeak, float maxIonPeak);

        IEnumerable<SpectraAnswer> RetrieveAllAnswers();
        IEnumerable<MassProblem> RetrieveAllMassProblems();
        IEnumerable<NMRProblem> RetrieveAllNmrProblems();
    }
}