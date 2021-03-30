namespace SpectraMaster.Data
{
    public class AnswerPicture
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int AnswerId { get; set; }
        public SpectraAnswer SpectraAnswer { get; set; }
    }

    public class ProblemPicture
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int AnswerId { get; set; }
        public SpectraAnswer SpectraAnswer { get; set; }
    }
}