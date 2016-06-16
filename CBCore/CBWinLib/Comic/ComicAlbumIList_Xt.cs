namespace CBWinLib.Comic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using CBLib.Comic;

    public static class ComicAlbumIList_Xt
    {
        #region Load
        public static void LoadFromFile(this IList<ComicAlbum> List, String ComicInfoFile, Boolean IsLight = false)
        {
            if (!File.Exists(ComicInfoFile)) return;

            List.Clear();

            var json = File.ReadAllText(ComicInfoFile);
            var cs = JsonConvert.DeserializeObject<ComicSerie>(json);

            if (IsLight)
                foreach (var ca in cs.ComicAlbums)
                    ca.AlbumCoverBytes = null;

            foreach(var album in cs.ComicAlbums)
                List.Add(album);
        }
        public static void LoadFromFiles(this IList<ComicAlbum> List, String[] ComicInfoFiles, Boolean IsLight = false)
        {
            List.Clear();

            foreach(var comicInfoFile in ComicInfoFiles)
            {
                var caList = new List<ComicAlbum>();
                caList.LoadFromFile(comicInfoFile, IsLight);

                foreach (var ca in caList)
                    List.Add(ca);
            }
        }
        #endregion
        #region Save
        public static void SaveToXAML(this IList<ComicAlbum> List, String ExportXMLFile, String ExportXAMLFile)
        {
            var xml = "" +
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>" +
                "<xs:schema" +
                "	xmlns:xs=\"http://www.w3.org/2001/XMLSchema\"" +
                "	xmlns:blend=\"http://schemas.microsoft.com/expression/blend/2008\"" +
                "	xmlns:tns=\"Expression.Blend.SampleData.SampleComicAlbum\"" +
                "	targetNamespace=\"Expression.Blend.SampleData.SampleComicAlbum\">" +
                "  <xs:element name=\"SampleComicAlbum\" type=\"tns:SampleComicAlbum\" />" +
                "  <xs:complexType name=\"SampleComicAlbum\">" +
                "    <xs:sequence>" +
                "      <xs:element name=\"Collection\" type=\"tns:ComicAlbumCollection\" />" +
                "    </xs:sequence>" +
                "  </xs:complexType>" +
                "  <xs:complexType name=\"ComicAlbumCollection\">" +
                "    <xs:sequence>" +
                "      <xs:element maxOccurs=\"unbounded\" name=\"ComicAlbum\" type=\"tns:ComicAlbum\" />" +
                "    </xs:sequence>" +
                "  </xs:complexType>" +
                "  <xs:complexType name=\"ComicAlbum\">" +
                "    <xs:attribute name=\"AlbumName\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumOrder\" type=\"xs:byte\" />" +
                "    <xs:attribute name=\"AlbumCount\" type=\"xs:byte\" />" +
                "    <xs:attribute name=\"AlbumDate\" type=\"xs:date\" />" +
                "    <xs:attribute name=\"AlbumScenarist\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumDrawer\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumColorist\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumCollection\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumEditor\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumIsbn\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumCover\" type=\"xs:string\" />" +
                "    <xs:attribute name=\"AlbumCoverBytes\" type=\"xsd:base64Binary\" />" +
                "  </xs:complexType>" +
                "</xs:schema>";

            var xaml_begin = "" +
                "<SampleData:SampleComicAlbum xmlns:SampleData=\"clr -namespace:Expression.Blend.SampleData.SampleComicAlbum\">" +
                "  <SampleData:SampleComicAlbum.Collection>";
            var xaml_content = "" +
                "<SampleData:ComicAlbum " +
                " AlbumName = \"{AlbumName}\"" +
                " AlbumOrder = \"{AlbumOrder}\"" +
                " AlbumCount = \"{AlbumCount}\"" +
                " AlbumDate = \"{AlbumDate}\"" +
                " AlbumScenarist = \"{AlbumScenarist}\"" +
                " AlbumDrawer = \"{AlbumDrawer}\"" +
                " AlbumColorist = \"{AlbumColorist}\"" +
                " AlbumCollection = \"{AlbumCollection}\"" +
                " AlbumEditor = \"{AlbumEditor}\"" +
                " AlbumIsbn = \"{AlbumIsbn}\"" +
                " AlbumCover = \"{AlbumCover}\"" +
                " AlbumCoverBytes = \"{AlbumCoverBytes}\"" +
                "/>";
            var xaml_end = "" +
                "  </SampleData:SampleComicAlbum.Collection>" +
                "</SampleData:SampleComicAlbum>";

            var xaml = new System.Text.StringBuilder();
            xaml.AppendLine(xaml_begin);
            foreach (var ca in List)
            {
                var xaml_line = xaml_content
                    .Replace("{AlbumName}", ca.AlbumName)
                    .Replace("{AlbumOrder}", ca.AlbumOrder.ToString())
                    .Replace("{AlbumCount}", ca.AlbumCount.ToString())
                    .Replace("{AlbumDate}", ca.AlbumDate.ToShortDateString())
                    .Replace("{AlbumScenarist}", ca.AlbumScenarist)
                    .Replace("{AlbumDrawer}", ca.AlbumDrawer)
                    .Replace("{AlbumColorist}", ca.AlbumColorist)
                    .Replace("{AlbumCollection}", ca.AlbumCollection)
                    .Replace("{AlbumEditor}", ca.AlbumEditor)
                    .Replace("{AlbumIsbn}", ca.AlbumIsbn)
                    .Replace("{AlbumCover}", ca.AlbumCover)
                    .Replace("{AlbumCoverBytes}", Convert.ToBase64String(ca.AlbumCoverBytes));
                xaml.AppendLine(xaml_line);
            }

            xaml.AppendLine(xaml_end);

            File.WriteAllText(ExportXMLFile, xml);
            File.WriteAllText(ExportXAMLFile, xaml.ToString());
        }
        #endregion
    }
}