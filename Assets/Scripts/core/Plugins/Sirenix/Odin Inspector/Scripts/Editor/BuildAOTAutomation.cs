﻿#if UNITY_5_6_OR_NEWER

//-----------------------------------------------------------------------
// <copyright file="BuildAOTAutomation.cs" company="Sirenix IVS">
// Copyright (c) Sirenix IVS. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Sirenix.Serialization.Internal
{
    using Sirenix.Serialization;
    using UnityEditor;
    using UnityEditor.Build;
    using System.IO;
#if UNITY_2018_1_OR_NEWER

    using UnityEditor.Build.Reporting;

#endif

#if UNITY_2018_1_OR_NEWER
    public class PreBuildAOTAutomation : IPreprocessBuildWithReport
#else
    public class PreBuildAOTAutomation : IPreprocessBuild
#endif
    {
        public int callbackOrder
        {
            get
            {
                return -1000;
            }
        }

        public void OnPreprocessBuild(BuildTarget target, string path)
        {
            if (AOTGenerationConfig.Instance.AutomateBeforeBuilds
                && AOTGenerationConfig.Instance.AutomateForPlatforms != null
                && AOTGenerationConfig.Instance.AutomateForPlatforms.Contains(target))
            {
                AOTGenerationConfig.Instance.ScanProject();
                AOTGenerationConfig.Instance.GenerateDLL();
            }
        }

#if UNITY_2018_1_OR_NEWER

        public void OnPreprocessBuild(BuildReport report)
        {
            this.OnPreprocessBuild(report.summary.platform, report.summary.outputPath);
        }

#endif
    }

#if UNITY_2018_1_OR_NEWER
    public class PostBuildAOTAutomation : IPostprocessBuildWithReport
#else
    public class PostBuildAOTAutomation : IPostprocessBuild
#endif
    {
        public int callbackOrder
        {
            get
            {
                return -1000;
            }
        }

        public void OnPostprocessBuild(BuildTarget target, string path)
        {
            if (AOTGenerationConfig.Instance.AutomateBeforeBuilds
                && AOTGenerationConfig.Instance.AutomateForPlatforms != null
                && AOTGenerationConfig.Instance.AutomateForPlatforms.Contains(target)
                && AOTGenerationConfig.Instance.DeleteDllAfterBuilds)
            {
                Directory.Delete(AOTGenerationConfig.Instance.AOTFolderPath, true);
                File.Delete(AOTGenerationConfig.Instance.AOTFolderPath.TrimEnd('/', '\\') + ".meta");
                AssetDatabase.Refresh();
            }
        }

#if UNITY_2018_1_OR_NEWER

        public void OnPostprocessBuild(BuildReport report)
        {
            this.OnPostprocessBuild(report.summary.platform, report.summary.outputPath);
        }

#endif
    }
}

#endif