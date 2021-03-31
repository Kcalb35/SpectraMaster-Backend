using SpectraMaster.AnswerController.Dto;
using SpectraMaster.Controllers;
using SpectraMaster.Models;

namespace SpectraMaster.Data
{
    public class NMRProblem
    {
        public NMRProblem(FormulaConfig formula)
        {
            H = formula.H;
            C = formula.C;
            N = formula.N;
            O = formula.O;
            F = formula.F;
            Si = formula.Si;
            P = formula.P;
            S = formula.S;
            Cl = formula.Cl;
            Br = formula.Br;
            I = formula.I;
        }

        public NMRProblem()
        {
            
        }
        public bool IsEqual(FormulaConfig formula)
        {
            return H == formula.H && C == formula.C && N == formula.N && O == formula.O && F == formula.F &&
                   Si == formula.Si && P == formula.P && S == formula.S && Cl == formula.Cl && Br == formula.Br &&
                   I == formula.I;
        }

        public int Id { get; set; }
        public int H { get; set; }
        public int C { get; set; }
        public int N { get; set; }
        public int O { get; set; }
        public int F { get; set; }
        public int Si { get; set; }
        public int P { get; set; }
        public int S { get; set; }
        public int Cl { get; set; }
        public int Br { get; set; }
        public int I { get; set; }

        public SpectraAnswer Answer{ get; set; }
        public int AnswerId { get; set; }
    }

    public class MassProblem
    {
        public MassProblem(float ionPeak)
        {
            IonPeak = ionPeak;
        }

        public MassProblem()
        {
            
        }
        public int Id { get; set; }
        public float IonPeak { get; set; }
        public SpectraAnswer Answer { get; set; }
        public int AnswerId { get; set; }
    }
}