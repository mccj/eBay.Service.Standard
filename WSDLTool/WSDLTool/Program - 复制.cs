using System;
using System.Collections.ObjectModel;

public class dddddddddddddffffffffffffffffffffffffffffffffffff { }
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "urn:ebay:apis:eBLBaseComponents")]
public class Person: dddddddddddddffffffffffffffffffffffffffffffffffff
{
    public string dddddddddddddddddd()
    {
        return "ddddddd";
    }
    //[Required]
    public string FirstName { get; set; }

    public string MiddleName { get; set; }

    //[Required]
    public string LastName { get; set; }

    public Gender Gender { get; set; }

    //[Range(2, 5)]
    public int NumberWithRange { get; set; }

    public DateTime Birthday { get; set; }

    public Company Company { get; set; }

    public Collection<Car> Cars { get; set; }
}

public enum Gender
{
    Male,
    Female
}

public class Car
{
    public string Name { get; set; }

    public Company Manufacturer { get; set; }
}

public class Company
{
    public string Name { get; set; }
}