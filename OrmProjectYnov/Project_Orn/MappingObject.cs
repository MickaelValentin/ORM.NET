using System.Collections.Generic;

namespace Project_Orn
{
    /// <summary>
    /// Classe créé dans le but d'avoir le nom de l objet/table puis ses propriétés
    /// sous forme de list classé par un autre objet PropertyAttributes
    /// </summary>
    public class MappingObject
    {
        public string ObjectName { get; set; }

        public List<PropertyAttributes> PropertiesAttributes { get; set; }
    }
}