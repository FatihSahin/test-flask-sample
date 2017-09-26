using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using TestFlask.Models.Context;
using TestFlask.Models.Entity;

namespace TestFlask.Assistant.Config
{
    #region must be seperate file 1

    public interface ITestFlaskAssistantConfig
    {
        bool Enabled { get; }
        ProjectConfig Project { get; }
        ApiConfig Api { get; }
    }


    public class TestFlaskAssistantConfig : ITestFlaskAssistantConfig
    {
        private static TestFlaskAssistantConfig instance;
        public static TestFlaskAssistantConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    var section = ConfigurationManager.GetSection("testFlaskAssistant") as TestFlaskAssistantConfigSection;

                    instance = new TestFlaskAssistantConfig
                    {
                        Enabled = section.Enabled,
                        Api = new ApiConfig
                        {
                            Url = section.Api.Url
                        },
                        Manager = new ManagerConfig
                        {
                            Url = section.Manager.Url
                        },
                        Project = new ProjectConfig
                        {
                            Key = section.Project.Key
                        }
                    };
                }

                return instance;
            }
        }

        public bool Enabled { get; private set; }

        public ProjectConfig Project { get; private set; }

        public ApiConfig Api { get; private set; }

        public ManagerConfig Manager { get; private set; }
    }

    public class ProjectConfig
    {
        public string Key { get; set; }
    }

    public class ApiConfig
    {
        public string Url { get; set; }
    }

    public class ManagerConfig
    {
        public string Url { get; set; }
    }
    #endregion

    #region must be seperate file 2
    public class TestFlaskAssistantConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("enabled")]
        public bool Enabled
        {
            get
            {
                return (bool)this["enabled"];
            }
            set
            {
                this["enabled"] = value;
            }
        }

        [ConfigurationProperty("api")]
        public TestFlaskApiElement Api
        {
            get
            {
                return (TestFlaskApiElement)this["api"];
            }
            set
            {
                this["api"] = value;
            }
        }

        [ConfigurationProperty("manager")]
        public TestFlaskManagerElement Manager
        {
            get
            {
                return (TestFlaskManagerElement)this["manager"];
            }
            set
            {
                this["manager"] = value;
            }
        }

        [ConfigurationProperty("project")]
        public ProjectElement Project
        {
            get
            {
                return (ProjectElement)this["project"];
            }
            set
            {
                this["project"] = value;
            }
        }
    }

    public class TestFlaskApiElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }
    }

    public class TestFlaskManagerElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }
    }

    public class ProjectElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
            set
            {
                this["key"] = value;
            }
        }
    }

    #endregion

}