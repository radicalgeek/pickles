﻿using System;
using System.IO;
using NUnit.Framework;
using Ninject;
using Pickles.DirectoryCrawler;
using Pickles.DocumentationBuilders.DITA;
using Pickles.Parser;
using Should;

namespace Pickles.Test
{
    [TestFixture]
    public class WhenGeneratingPathsFromFeaturesToDitaFiles : BaseFixture
    {
        [Test]
        public void ThenCanGeneratePathToDeepLevelFeatureFileSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.FeatureFolder = new DirectoryInfo(@"c:\features");
            var featureNode = new FeatureDirectoryTreeNode(new FileInfo(@"c:\features\path\to\the_feature.feature"),
                                                           @"features\path\to\the_feature.feature",
                                                           new Feature {Name = "The Feature"});

            var ditaMapPathGenerator = Kernel.Get<DitaMapPathGenerator>();

            Uri existingUri = ditaMapPathGenerator.GeneratePathToFeature(featureNode);
            existingUri.OriginalString.ShouldEqual(@"path/to/the_feature.dita");
        }

        [Test]
        public void ThenCanGeneratePathToTopLevelFeatureFileSuccessfully()
        {
            var configuration = Kernel.Get<Configuration>();
            configuration.FeatureFolder = new DirectoryInfo(@"c:\features");
            var featureNode = new FeatureDirectoryTreeNode(new FileInfo(@"c:\features\the_feature.feature"),
                                                           @"features\the_feature.feature",
                                                           new Feature {Name = "The Feature"});

            var ditaMapPathGenerator = Kernel.Get<DitaMapPathGenerator>();

            Uri existingUri = ditaMapPathGenerator.GeneratePathToFeature(featureNode);
            existingUri.OriginalString.ShouldEqual(@"the_feature.dita");
        }
    }
}