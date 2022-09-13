using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Utilities.ExMethod;

namespace Utilities.IO
{
   public static   class XMLHelper  
   {
       public static XmlDocument SafeLoad(this XmlDocument doc, string fileName, Action<Exception> except = null)
       {
           try
           {
               if (File.Exists(fileName))
                   doc.Load(fileName);
           }
           catch (Exception ex) { except.Do(ex); }
           return doc;
       }
       public static string SafeValue(this System.Xml.XmlAttribute atrr, string emptyValue = "")
        {
            if (atrr != null)
                return atrr.Value;
            return emptyValue;
        }
       public static string SafeValue(this  XAttribute atrr, string emptyValue = "")
       {
           if (atrr != null)
               return atrr.Value;
           return emptyValue;
       }
     
       public static void AddX(this XContainer xc, object content)
       {
           if (content == null)
               return;

           xc.Add(content);
       }
       public static XmlNode Find(this XmlNode N,string nodeName, Predicate<XmlNode> condition=null)
       {
           if (condition == null)
               return N.SelectSingleNode(nodeName);
           foreach (XmlNode n in N.SelectNodes(nodeName))
               if (condition(n))
                   return n;
           return null;
       }
       public static XElement Find(this XElement xele,string nodeName ,Predicate<XElement> condition=null)
       {
           if (condition == null)
               return xele.Element(nodeName);
           foreach (var n in xele.Elements(nodeName))
               if (condition(n))
                   return n;
           return null;
       }
       public static XmlNode CreateNodeIfNotExist(this XmlDocument doc, string nodeName,XmlNode parentNode=null, Predicate<XmlNode> condition=null)
       {
           XmlNode node = null;
           if(parentNode ==null)
                parentNode = doc;
           if(condition==null)
           {
                node = parentNode.SelectSingleNode(nodeName);
           }
           else
           {
               XmlNodeList nodes = parentNode.SelectNodes(nodeName);
               foreach (XmlNode n in nodes)
                   if (condition(n)) { node = n; break; }
           }         
          
           if (node == null)
           {
               node = doc.CreateElement(nodeName);
               parentNode.AppendChild(node);
           }
           return node;
       }
       public static XmlAttribute SetAttribute(this XmlNode Node, string localName, string value)
       {
           XmlAttribute attributeNode = Node.Attributes[localName];
           if (attributeNode == null)
           {
               attributeNode = Node.OwnerDocument.CreateAttribute(localName);
               Node.Attributes.Append(attributeNode);              
           }
           attributeNode.Value = value;
           return attributeNode;
       }
        
       public static XmlNode  CreateNodeAndRemoveOld(this XmlDocument doc, string nodeName, XmlNode parentNode = null)
       {
           XmlNode node = null;
           if (parentNode == null)
           {
              var  nodes = doc.SelectNodes(nodeName);
              foreach(XmlNode n in nodes)
                   doc.RemoveChild(n);
               node = doc.CreateElement(nodeName);
               doc.AppendChild(node);
           }
           else
           {
              var  nodes = parentNode.SelectNodes(nodeName);
               foreach (XmlNode n in nodes)
                   parentNode.RemoveChild(n);
               node = doc.CreateElement(nodeName);
               parentNode.AppendChild(node);
           } 
           return node;
       }
       public static XmlAttribute SetAttrVal(this XmlDocument doc, XmlNode node,string attrName,string attrValue)
       {
           var attr = node.Attributes[attrName];
           if(attr==null)
           {
               attr = node.Attributes.Append(doc.CreateAttribute(attrName));
           }
           attr.Value = attrValue;
           return attr;
       }
       public static XElement ElementX(this XElement xEle, XName name)
       {
           var rtnEle = xEle.Element(name);
           if(rtnEle ==null)
           {
               rtnEle = new XElement(name);
               xEle.Add(rtnEle);
           }
           return rtnEle;
       }
       public static XElement ElementX(this XDocument xEle, XName name)
       {
           var rtnEle = xEle.Element(name);
           if (rtnEle == null)
           {
               rtnEle = new XElement(name);
               xEle.Add(rtnEle);
           }
           return rtnEle;
       }

       public static XDocument GetXDocument(this XmlDocument xmldoc)
       {
           XDocument xDoc = new XDocument();
           using (XmlWriter xmlWriter = xDoc.CreateWriter())
               xmldoc.WriteTo(xmlWriter);
           return xDoc;
       }
       public static XmlDocument GetXmlDocument(this XDocument element)
       {
           using (XmlReader xmlReader = element.CreateReader())
           {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(xmlReader);
               return xmlDoc;
           }
       }

       public static XElement GetXElement(this XmlNode node)
       {
           XDocument xDoc = new XDocument();
           using (XmlWriter xmlWriter = xDoc.CreateWriter())
               node.WriteTo(xmlWriter);
           return xDoc.Root;
       }
       public static XmlNode GetXmlNode(this XElement element)
       {
           using (XmlReader xmlReader = element.CreateReader())
           {
               XmlDocument xmlDoc = new XmlDocument();
               xmlDoc.Load(xmlReader);
               return xmlDoc;
           }
       }
    }

    
}
