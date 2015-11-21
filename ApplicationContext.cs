using System.Xml;
using System.Collections.Generic;
using System;
using System.Reflection;

namespace MyLibrary
{
    internal class ComponentInfo
    {
        public object Component;
        public Dictionary<string, string> Properties;
    }
    public class ApplicationContext
    {
        private string xmlFile;
        private XmlReader reader;
        private Dictionary<string, ComponentInfo> components; 

        public ApplicationContext(string v)
        {
            this.xmlFile = v;
            reader = XmlReader.Create(xmlFile);
            ComponentInfo lastComponentInfo = null;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "root":
                                components = new Dictionary<string, ComponentInfo>();
                                break;
                            case "component":
                                string id = reader.GetAttribute("id");
                                string typeName = reader.GetAttribute("type");
                                string assemblyName = reader.GetAttribute("assembly");
                                Assembly assembly;
                                //if (assemblyName == null)
                                    assembly = Assembly.GetEntryAssembly();
                                //else
                                  //  assembly = Assembly.LoadFrom(assemblyName); //da eroare aici, spune ca nu gaseste, nu ar trebui sa am ceva dll-uri generate in proiect?
                                foreach (Type type in assembly.GetTypes())
                                {
                                    if (type.Name == typeName)
                                    {
                                        lastComponentInfo = new ComponentInfo();
                                        lastComponentInfo.Component = Activator.CreateInstance(type);
                                        components[id] = lastComponentInfo;
                                    }
                                }
                                
                                break;
                            case "property":
                                if (lastComponentInfo.Properties == null)
                                {
                                    lastComponentInfo.Properties = new Dictionary<string, string>();
                                }
                                lastComponentInfo.Properties[reader.GetAttribute("name")] = reader.GetAttribute("ref");
                                break;
                            default:
                                break;
                        }
                        break;
                   
                    case XmlNodeType.EndElement:
                        switch (reader.Name)
                        {
                            case "root":
                                foreach (string componentID in components.Keys)
                                {
                                    ComponentInfo componentInfo = components[componentID];
                                    if (componentInfo.Properties!=null)
                                        foreach (string propertyName in componentInfo.Properties.Keys)
                                        {
                                            string propertyRefId = componentInfo.Properties[propertyName];
                                            Type type = componentInfo.Component.GetType();
                                            PropertyInfo propertyInfo = type.GetProperty(propertyName);//aici e buba, e null, vad de ce
                                            //PropertyInfo[] propertyInfo = type.GetProperties();
                                           // foreach (PropertyInfo pi in propertyInfo)
                                                //Console.WriteLine(pi.ToString());
                                            var value = components[propertyRefId].Component;
                                            propertyInfo.SetValue(componentInfo.Component, value);
                                        }
                                }
                                break;
                            case "component":
                                break;
                            case "property":
                                break;
                            default:
                                break;
                        }
                        break;
                    
                    default:
                        break;
                }
            }
        }

        public T GetComponent<T>(string v) where T:class
        {
            return components[v].Component as T;
        }
    }
}
