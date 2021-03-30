using System.Collections.Generic;
using System.Threading.Tasks;
using SpectraMaster.Data;
using Ubiety.Dns.Core.Records.NotUsed;

namespace SpectraMaster.Models
{
    public interface IAnswerRepository
    {
        Task<SpectraAnswer> CreateAnswer(string probDescription, string ansDescription, List<string> probPics,
            List<string> ansPics,float ionPeak=0,FormulaConfig formula=null);

        Task<bool> DeleteAnswerById(int id);
        Task<SpectraAnswer> UpdateNmrAnswer(int id,string probDescription, string ansDescription, List<string> probPics,
            List<string> ansPics,float ionPeak=0,FormulaConfig formula=null);

        SpectraAnswer RetrieveAnswerById(int id);
        IEnumerable<SpectraAnswer > RetrieveMassAnswer(float minIonPeak, float maxIonPeak);
        IEnumerable<SpectraAnswer> RetrieveNmrAnswer(FormulaConfig formula);
        IEnumerable<SpectraAnswer> RetrieveComplexAnswer(FormulaConfig formula, float minIonPeak, float maxIonPeak);

        IEnumerable<SpectraAnswer> RetrieveAllAnswers();
    }

    public record FormulaConfig(int H=0, int C=0, int N=0, int O=0, int F=0, int Si=0, int P=0, int S=0, int Cl=0, int Br=0, int I=0);
}