using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DotNetCoreXUnit1.src.DTO.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DotNetCoreXUnit1.scenarious
{
    public class LaunchSettingsFixture : IDisposable
    {
        public EnvironmentConfig _config;
        public Dictionary<String, String> _browserConfig;
        public LaunchSettingsFixture()
        {
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

                foreach (var variable in variables)
                {
                    Environment.SetEnvironmentVariable(variable.Name, variable.Value.ToString());
                }
            }

            var webbrowser = Environment.GetEnvironmentVariable("webbrowser");
            _browserConfig = new Dictionary<string, string>()
            {
                { "webbrowser" , webbrowser}
            };

            var environmentConfig = Environment.GetEnvironmentVariable("environmentConfig");
            string path = String.Format("configs/{0}", environmentConfig);
            using (var file = File.OpenText(path))
            {
                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(new CamelCaseNamingConvention())
                    .Build();
                _config = deserializer.Deserialize<EnvironmentConfig>(file);
            }
            _browserConfig.Add("hostUrl", @"http://" + _config.HostName + _config.DomainName);
        }

        public void Dispose()
        {
            // ... clean up
        }
    }
}
