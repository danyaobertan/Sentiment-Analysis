using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Sentiment_AnalysisML.Model;

namespace Sentiment_Analysis.Controllers
{
    public class SemanticController : Controller
    {
        [HttpGet]
        public IActionResult Analysis()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Analysis(ModelInput input)
        {
            // Load the model  
            MLContext mlContext = new MLContext();
            ITransformer mlModel = mlContext.Model.Load(@"..\Sentiment AnalysisML.Model\MLModel.zip", out var modelInputSchema);
            // Create predection engine related to the loaded train model
            var predEngine = mlContext.Model.CreatePredictionEngine<ModelInput, ModelOutput>(mlModel);
            //Input  
            input.Year = DateTime.Now.Year;
            // Try model on sample data and find the score
            ModelOutput result = predEngine.Predict(input);
            // Store result into ViewBag
            ViewBag.Result = result;
            return View();
        }
    }
}