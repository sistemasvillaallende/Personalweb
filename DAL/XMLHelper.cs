using System;
using System.Xml;
using System.Data.Common;

namespace DAL
{
  /// <summary>
  /// Descripción breve de LibXML.
  /// </summary>
  public class XMLHelper {

    public XMLHelper() {

    }

    public string OpenTag(string Name) {
      return "<" + Name.Trim()+ ">";
    }

    public string CloseTag(string Name) {
      return "</" + Name.Trim()+ ">";
    }

    public string Tag(string Name, object Value) {
      string retStr = "";
      switch (Value.GetType().ToString()) {
        case "System.String":
          retStr = "<" + Name.Trim()+ ">" + Value.ToString().Trim() + "</" + Name.Trim()+ ">";
          break;

        case "System.Boolean":
          retStr = "<" + Name.Trim()+ ">" + System.Convert.ToBoolean(Value).ToString() + 
                    "</" + Name.Trim()+ ">";
          break;

        case "System.Int16":
        case "System.Int32":
        case "System.Int64":
          retStr = "<" + Name.Trim()+ ">" + System.Convert.ToInt64(Value).ToString() + 
                   "</" + Name.Trim()+ ">";
          break;

        case "System.Double":
          retStr = "<" + Name.Trim()+ ">" + System.Convert.ToDouble(Value).ToString() + 
                   "</" + Name.Trim()+ ">";
          break;

        case "System.DateTime":
          retStr = "<" + Name.Trim()+ ">" + System.DateTime.Parse(Value.ToString()).ToString("dd/mm/yyyy hh:mm:ss") + "</" + Name.Trim()+ ">";
          break;
      }
      return retStr;

    }

    public string TagCDATA(string Name, string Value) {
      return "<" + Name.Trim()+ ">" + "<![CDATA[" + Value.Trim() + "]]>" + "</" + Name.Trim()+ ">";
    }

    public string RsToXMLStr(DbDataReader reader, string RootNodeName, string NodeName) {
      XmlNode node;
      XmlNode nodeRoot;
      XmlDocument doc;
			
      doc = new XmlDocument();
      nodeRoot = this.AddRootNode(doc, RootNodeName);

      while (reader.Read() == true) {
        node = this.AddNode(doc, nodeRoot, NodeName, "");
        for (int i=0; i < reader.FieldCount; i++) {
          this.AddNode(doc, node, reader.GetName(i), reader.GetValue(i).ToString());
        }
      }
      return doc.OuterXml;
    }

    public XmlNode AddRootNode(XmlDocument doc, string Name ) {
      XmlNode n;
      XmlElement el;

      n = doc.CreateNode(XmlNodeType.XmlDeclaration,"","");
      doc.AppendChild(n);

      el = doc.CreateElement("",Name,"");
      n  = doc.AppendChild(el);
      el = null;
      return n;
    }

    public XmlNode AddNode(XmlDocument doc, XmlNode Parent, string Name, string Value ) {
      XmlNode n;
      XmlElement el;
      XmlText t;

      el = doc.CreateElement("",Name,"");
      t  = doc.CreateTextNode(Value);
      el.AppendChild(t);
      n  = Parent.AppendChild(el);
      el = null;
      t  = null;
      return n;
    }

  }
}
