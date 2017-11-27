using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using DotNetCoreXUnit1.src.DTO.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DotNetCoreXUnit1
{
    public sealed class TestContext
    {
        private static readonly string Location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        private bool _disposed = false;
        public BrowserFactory BrowserFactory { get; }
        public EnvironmentConfig EnvConfig { get;}
        public Dictionary<String, String> InitConfig { get;}
        public static TestContext Current = new TestContext();

        private TestContext()
        {
            Console.WriteLine("Inside TEst CONTEXT");
            InitConfig = LoadInitConfig();
            EnvConfig = LoadEnvironmentConfig(InitConfig.GetValueOrDefault("environmentConfig"));
            BrowserFactory = BrowserFactory.InitFactory(InitConfig, EnvConfig);
        }

        public Dictionary<String, String> LoadInitConfig()
        {
            Dictionary<String, String> initConfig = new Dictionary<string, string>();
            //TODO: We can get read of this and use standard yaml file. 
            using (var file = File.OpenText("Properties/launchSettings.json"))
            {
                var reader = new JsonTextReader(file);
                var jObject = JObject.Load(reader);

                var variables = jObject
                    .GetValue("profiles")
                    //select a proper profile here
                    .SelectMany(profiles => profiles.Children())
                    .SelectMany(profile => profile.Children<JProperty>())
                    .Where(prop => prop.Name == "environmentVariables")
                    .SelectMany(prop => prop.Value.Children<JProperty>())
                    .ToList();

                //TODO; we probably don't need SetEnvironmentVariable if we are storing config in TestContext. Anyway using Env variable to pass arguments a bad practice.
                foreach (var variable in variables)
                {
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                    initConfig.Add(variable.Name, variable.Value.ToString());
                }

                return initConfig;
            }
        }

        public EnvironmentConfig LoadEnvironmentConfig(string fileName)
        {
            string path = String.Format("configs/{0}", fileName);
            EnvironmentConfig config;
            using (var file = File.OpenText(path))
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(new CamelCaseNamingConvention())
                    .Build();
                config = deserializer.Deserialize<EnvironmentConfig>(file);
            }
            return config;
        }

        public void InitLogFIle()
        {
            var logFile = File.Create(Location + "/MyPersLSZ.txt");
            var logWriter = new StreamWriter(logFile);
            
            logWriter.WriteLine("Log message IlitLog is : " + Thread.CurrentThread.ManagedThreadId);
            logWriter.Dispose();
        }

        public void WritToLog(string text)
        {
            if (!File.Exists(Location + "/MyPersLSZ.txt"))
            {
                InitLogFIle();
            }
            var logFile = System.IO.File.AppendText(Location + "/MyPersLSZ.txt");
            logFile.WriteLine(text);
            logFile.Dispose();
        }

        public void WriteLog()
        {
            WritToLog("WriteLog(): " + Thread.CurrentThread.ManagedThreadId);
        }

    }
}
