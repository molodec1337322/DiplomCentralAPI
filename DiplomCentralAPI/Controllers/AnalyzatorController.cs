﻿using DiplomCentralAPI.Data.Interfaces;
using DiplomCentralAPI.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DiplomCentralAPI.Controllers
{
    [Route("api/analyzator")]
    public class AnalyzatorController : Controller
    {
        private readonly IRepository<Schema> _schemaRepository;
        private readonly IRepository<Experiment> _experimentRepository;
        private readonly IConfiguration _appConfiguration;
        private readonly IWebHostEnvironment _appEnviroment;

        public AnalyzatorController(IRepository<Schema> schemaRepo, IRepository<Experiment> experimentRepo, IConfiguration appConfiguration, IWebHostEnvironment appEnviroment)
        {
            _schemaRepository = schemaRepo;
            _experimentRepository = experimentRepo;
            _appConfiguration = appConfiguration;
            _appEnviroment = appEnviroment;
        }

        [HttpGet]
        [Route("getAllAnalyzators")]
        public async Task<IActionResult> GetAllAnalyzators()
        {
            List<string> analyzators = new List<string>();

            var analyzatorScriptsPath = Path.Combine(_appEnviroment.ContentRootPath, "analyzators");
            if (!Directory.Exists(analyzatorScriptsPath))
            {
                return BadRequest(new { error = "Analyzator scripts folder not found" });
            }

            foreach(string dir in Directory.EnumerateDirectories(analyzatorScriptsPath))
            {
                if(System.IO.File.Exists(Path.Combine(dir, "main.py")))
                {
                    //analyzators.Add(Path.GetDirectoryName(dir));
                    analyzators.Add(dir.Split("\\").Last().ToString());
                }
            }

            return Ok(analyzators);
        }

        [HttpGet]
        [Route("analyze")]
        public async Task<IActionResult> StartAnalyze(string analyzerName, int experimentId)
        {
            var analyzatorScriptPath = Path.Combine(_appEnviroment.ContentRootPath, "analyzators", analyzerName, "main.py");
            if (!System.IO.File.Exists(analyzatorScriptPath))
            {
                return BadRequest(new { error = "Analyzator script not found" });
            }
            var resultsSavePath = Path.Combine(_appEnviroment.WebRootPath, _appConfiguration.GetValue<string>("AnalyzeResultFilesStorageFolder"));
            if (!Directory.Exists(resultsSavePath))
            {
                //return BadRequest(new { error = "camera script not found" });
                Directory.CreateDirectory(resultsSavePath);
            }

            Experiment experiment = _experimentRepository.Get(experimentId);

            var timeStarted = System.DateTime.UtcNow;
            var timeStr = timeStarted.Day + "-" + timeStarted.Month + "-" + timeStarted.Year + "_" + timeStarted.Hour + "-" + timeStarted.Minute + "-" + timeStarted.Second;
            var filename = experiment.VideoPath.Split("\\").Last().Split(".").First() + "_by_" + analyzerName + timeStr;
            var resultFile = Path.Combine(resultsSavePath, timeStr.Replace(" ", "_") + ".txt");

            try
            {
                char[] splitter = { '\r' };

                Process process = new Process();
                process.StartInfo.FileName = "py.exe";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;

                process.StartInfo.Arguments = string.Concat(analyzatorScriptPath, " ", experiment.VideoPath, " ", resultFile);
                process.Start();

                StreamReader sReader = process.StandardOutput;
                string[] output = sReader.ReadToEnd().Split(splitter);

                foreach (string s in output)
                    Console.WriteLine(s);

                process.WaitForExit();

                experiment.ResultPath = resultFile;
                _experimentRepository.Update(experiment);
                _experimentRepository.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return BadRequest(new { error = ex.Message, traceback = ex.StackTrace });
            }

            return Ok("analyzeOk");
        }
    }
}
