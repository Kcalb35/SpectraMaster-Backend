using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpectraMaster.Models;
using SpectraMaster.Utils;

namespace SpectraMaster.Controllers
{
    [EnableCors]
    [ApiController]
    [Route("api")]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerRepository _repo;
        private IFileUtil _fileUtil;

        public AnswerController(IAnswerRepository repo, IFileUtil fileUtil)
        {
            _repo = repo;
            _fileUtil = fileUtil;
        }

        [AllowAnonymous]
        [HttpGet("ans/{id:int}")]
        public IActionResult GetAnswerById([FromRoute] int id)
        {
            var ans = _repo.RetrieveAnswerById(id);
            if (ans == null) return NotFound(ApiResponse.Error("answer not found"));
            return Ok(ApiResponse.Success(ResponseUtil.AnswerConvert(ans)));
        }

        [AllowAnonymous]
        [HttpGet("ans/all")]
        public IActionResult GetAllAnswer()
        {
            var ans = _repo.RetrieveAllAnswers();
            var resp = ans.Select(ResponseUtil.AnswerConvert);
            return Ok(ApiResponse.Success(resp));
        }

        [AllowAnonymous]
        [HttpGet("nmr")]
        public IActionResult GetAllNMR()
        {
            var probs = _repo.RetrieveAllNmrProblems().Select(ResponseUtil.NmrProblemConvert);
            return Ok(ApiResponse.Success(probs));
        }

        [AllowAnonymous]
        [HttpGet("nmr/{id:int}")]
        public IActionResult GetNMRById(int id)
        {
            var prob = _repo.RetrieveNmrProblemById(id);
            if (prob == null) return NotFound(ApiResponse.Error("nmr problem not found"));
            return Ok(ApiResponse.Success(ResponseUtil.NmrProblemConvert(prob)));
        }

        [AllowAnonymous]
        [HttpGet("mass")]
        public IActionResult GetAllMass()
        {
            var probs = _repo.RetrieveAllMassProblems().Select(ResponseUtil.MassProblemConvert);
            return Ok(ApiResponse.Success(probs));
        }

        [AllowAnonymous]
        [HttpGet("mass/{id:int}")]
        public IActionResult GetMassById(int id)
        {
            var prob = _repo.RetrieveMassProblemById(id);
            if (prob == null) return NotFound(ApiResponse.Error("mass problem not found"));
            return Ok(ApiResponse.Success(prob));
        }

        [AllowAnonymous]
        [HttpPost("ans/search")]
        public IActionResult SearchAnswer([FromBody] SearchReq req)
        {
            var answers = _repo.RetrieveAnswer(req.Formula, req.MinIonPeak, req.MaxIonPeak);
            if (answers == null || !answers.Any()) return NotFound(ApiResponse.Error("answer not found please modify your conditions"));
            var ans = answers.Select(ResponseUtil.AnswerConvert);
            return Ok(ApiResponse.Success(ans));
        }

        [Authorize]
        [HttpPost("ans")]
        public async Task<IActionResult> CreateAnswer([FromForm] CreateReq req)
        {
            if ((req.Formula==null || !req.Formula.IsValid()) && req.IonPeak< 0) return BadRequest(ApiResponse.Error("bad descriptions"));
            if (string.IsNullOrEmpty(req.Answer) || string.IsNullOrEmpty(req.Problem)) return BadRequest(ApiResponse.Error("please input problem and answer descriptions"));
            // save files
            var problemPictures = new List<string>();
            var ansPictures = new List<string>();
            if (req.ProbFiles!= null && req.ProbFiles.Any())
                foreach (var pic in req.ProbFiles)
                    problemPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            if (req.AnsFiles!= null && req.AnsFiles.Any())
                foreach (var pic in req.AnsFiles)
                    ansPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            var answer = await _repo.CreateAnswerAsync(req.Problem, req.Answer, problemPictures, ansPictures, req.Formula, req.IonPeak);
            return Ok(ApiResponse.Success(ResponseUtil.AnswerConvert(answer)));
        }

        [Authorize]
        [HttpDelete("ans/{id:int}")]
        public async Task<IActionResult> DeleteAnswer([FromRoute] int id)
        {
            var ans = await _repo.DeleteAnswerById(id);
            if (ans) return Ok(ApiResponse.Success("delete successfully"));
            return NotFound(ApiResponse.Error("answer not found"));
        }

        [Authorize]
        [HttpDelete("nmr/{id:int}")]
        public async Task<IActionResult> DeleteNmrProblem(int id)
        {
            var flag = await _repo.DeleteNmrProblemById(id);
            if (flag) return Ok(ApiResponse.Success("delete successfully"));
            return NotFound(ApiResponse.Error("problem not found"));
        }

        [Authorize]
        [HttpDelete("mass/{id:int}")]
        public async Task<IActionResult> DeleteMassProblem(int id)
        {
            var flag = await _repo.DeleteMassProblemById(id);
            if (flag) return Ok(ApiResponse.Success("delete successfully"));
            return NotFound(ApiResponse.Error("problem not found"));
        }

        [Authorize]
        [HttpPut("ans/{id:int}")]
        public async Task<IActionResult> UpdateAnswer([FromRoute] int id, [FromForm] CreateReq req)
        {
            var ans = _repo.RetrieveAnswerById(id);
            if (ans == null) return NotFound(ApiResponse.Error("answer not found"));
            if ((req.Formula == null || !req.Formula.IsValid()) && req.IonPeak < 0) return BadRequest(ApiResponse.Error("please limit conditions"));
            if (string.IsNullOrEmpty(req.Answer) || string.IsNullOrEmpty(req.Problem)) return BadRequest(ApiResponse.Error("please input problem and answer descriptions"));
            var problemPictures = new List<string>();
            var ansPictures = new List<string>();
            if (req.ProbFiles!= null && req.ProbFiles.Any())
                foreach (var pic in req.ProbFiles)
                    problemPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            if (req.AnsFiles!= null && req.AnsFiles.Any())
                foreach (var pic in req.AnsFiles)
                    ansPictures.Add(await _fileUtil.CopyToServerFileAsync(pic));
            var answer = await _repo.UpdateAnswerAsync(id, req.Problem, req.Answer, problemPictures, ansPictures, req.IonPeak,
                req.Formula);
            return Ok(ApiResponse.Success(ResponseUtil.AnswerConvert(answer)));
        }

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UplodaFile([FromBody] IFormFile file)
        {
            var filename = await _fileUtil.CopyToServerFileAsync(file);
            if (filename != null)
                return Ok(ApiResponse.Success(filename));
            else return BadRequest(ApiResponse.Error("error"));
        }
    }
}