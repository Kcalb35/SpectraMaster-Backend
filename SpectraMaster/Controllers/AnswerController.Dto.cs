using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SpectraMaster.Data;

namespace SpectraMaster.Controllers
{
    public record FormulaConfig(int H = 0, int C = 0, int N = 0, int O = 0, int F = 0, int Si = 0, int P = 0, int S = 0,
        int Cl = 0, int Br = 0, int I = 0)
    {
        public bool IsValid()
        {
            return H >= 0 && C >= 0 && N >= 0 && O >= 0 && F >= 0 && Si >= 0 && P >= 0 && S >= 0 && Cl >= 0 &&
                   Br >= 0 && I >= 0;
        }
    }
    public record ResponseAnswer(int Id, string Problem, string Answer, IEnumerable<string> ProblemPics,
        IEnumerable<string> AnswerPics, float IonPeak, FormulaConfig Formula);

    public record ResponseNmrProblem(int Id, FormulaConfig Formula,int AnswerId,IEnumerable<string> ProblemPics);

    public record ResponseMassProblem(int Id, float IonPeak, int AnswerId,IEnumerable<string> ProblemPics);

    public record SearchReq(FormulaConfig Formula, float MinIonPeak, float MaxIonPeak);

    public record CreateReq(string Problem, string Answer, float IonPeak, FormulaConfig Formula,
        IFormFileCollection ProbFiles, IFormFileCollection AnsFiles);
    public static class ResponseUtil{
        public static ResponseAnswer AnswerConvert(SpectraAnswer ans)
        {
            var ansPics = ans.AnswerPictures.Select(pic => pic.Path);
            var probPics = ans.ProblemPictures.Select(pic => pic.Path);
            FormulaConfig formula = null;
            float ionPeak = -1;
            if (ans.NmrProblem != null)
                formula = FormulaConvert(ans.NmrProblem);
            if (ans.MassProblem != null)
                ionPeak = ans.MassProblem.IonPeak;
            return new ResponseAnswer(ans.Id, ans.ProblemDescription, ans.AnswerDescription, probPics, ansPics, ionPeak,
                formula);
        }

        public static FormulaConfig FormulaConvert(NMRProblem prob)
        {
            return new FormulaConfig(prob.H, prob.C, prob.N, prob.O, prob.F, prob.Si, prob.P, prob.S, prob.Cl, prob.Br,
                prob.I);
        }

        public static ResponseMassProblem MassProblemConvert(MassProblem prob)
        {
            var pics = prob.Answer.ProblemPictures.Select(pic => pic.Path);
            return new ResponseMassProblem(prob.Id, prob.IonPeak, prob.AnswerId, pics);
        }

        public static ResponseNmrProblem NmrProblemConvert(NMRProblem prob)
        {
            var pics = prob.Answer.ProblemPictures.Select(pic => pic.Path);
            var formula = FormulaConvert(prob);
            return new ResponseNmrProblem(prob.Id, formula, prob.AnswerId, pics);
        }
        
    }
}