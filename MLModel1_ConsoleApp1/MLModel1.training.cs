﻿﻿// This file was auto-generated by ML.NET Model Builder. 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML.Trainers;
using Microsoft.ML;

namespace MLModel1_ConsoleApp1
{
    public partial class MLModel1
    {
        public static ITransformer RetrainPipeline(MLContext context, IDataView trainData)
        {
            var pipeline = BuildPipeline(context);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.Categorical.OneHotEncoding(new []{new InputOutputColumnPair(@"Data as of", @"Data as of"),new InputOutputColumnPair(@"Start Date", @"Start Date"),new InputOutputColumnPair(@"End Date", @"End Date"),new InputOutputColumnPair(@"Sex", @"Sex")})      
                                    .Append(mlContext.Transforms.ReplaceMissingValues(@"Total deaths", @"Total deaths"))      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(@"Age Years", @"Age Years"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"Data as of",@"Start Date",@"End Date",@"Sex",@"Total deaths",@"Age Years"}))      
                                    .Append(mlContext.Regression.Trainers.FastForest(new FastForestRegressionTrainer.Options(){NumberOfTrees=4,FeatureFraction=1F,LabelColumnName=@"COVID-19 Deaths",FeatureColumnName=@"Features"}));

            return pipeline;
        }
    }
}
