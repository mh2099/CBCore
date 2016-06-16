namespace CBWinLib.Comic
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Web;
    using HtmlAgilityPack;
    using Newtonsoft.Json;
    using CBLib.Comic;
    using CBLib.Tools;

    public static class ComicScrapper
    {
        private class ComicRequest
        {
            public String id { get; set; }
            public String label { get; set; }
            public String value { get; set; }
            public String desc { get; set; }
            public String category { get; set; }
        }

        public static ComicSerie GetSerie(String Directory, String ComicName)
        {
            // Get series informations
            var serieUrl = $"http://www.bedetheque.com/ajax/tout?term={ComicName}";
            var webClient = new WebClient();
            var json = webClient.DownloadString(new Uri(serieUrl));
            var request = JsonConvert.DeserializeObject<List<ComicRequest>>(json);
            webClient.Dispose();

            var cs = new ComicSerie();

            if (request != null)
                if (request.Count > 0)
                {
                    cs.Directory = Directory;
                    cs.SerieId = Convert.ToUInt16(request.Select(a => a.id).FirstOrDefault().Substring(1));
                    cs.SerieName = ComicName.GetRealName();
                    var name = cs.SerieName.Replace(" ", "-").Replace("'", "").Replace(",", "").Replace("$", "S");
                    cs.SerieUrl = $"http://www.bedetheque.com/serie-{cs.SerieId}-BD-{name}.html";
                    cs.AlbumsUrl = $"http://www.bedetheque.com/albums-{cs.SerieId}-BD-{name}.html";

                    var hWeb = new HtmlWeb();
                    var hDoc = hWeb.Load(cs.SerieUrl);

                    var hNode = hDoc.DocumentNode.SelectSingleNode("(//article[@class='single-block'])/div/p");
                    var hNodes = hDoc.DocumentNode.SelectNodes("(//ul[@class='serie-info'])/li");

                    if (hNode != null)
                        cs.SerieSynopsis = hNode.InnerText;

                    if (hNodes != null)
                    {
                        foreach (var node in hNodes)
                        {
                            var infos = node.InnerText;
                            var left4 = node.InnerText.DeleteLineBreakAndSpace().Substring(0, 4).ToLower();

                            switch (left4)
                            {
                                case "genr": // genre
                                    cs.SerieCategory = infos.DeleteThis("Genre :");
                                    break;
                                case "paru": // parution
                                    var parution = infos.DeleteThis("Parution :");
                                    switch (parution)
                                    {
                                        case "Série finie":
                                            cs.SerieFinished = true;
                                            break;
                                        case "Série en cours":
                                            cs.SerieFinished = false;
                                            break;
                                    }
                                    break;
                            }
                        }
                    }

                    // Get albums list
                    var hWeb2 = new HtmlWeb();
                    var hDoc2 = hWeb.Load(cs.AlbumsUrl);

                    var b = hDoc2.DocumentNode.SelectNodes("(//ul[@class='gallery-couv-large'])/li");

                    if (b != null)
                        for (var j = 1; j <= b.Count; j++)
                        {
                            var xpathUrl = $"(//ul[@class='gallery-couv-large'])/li[{j}]/a/@href";
                            var urlNode2 = hDoc2.DocumentNode.SelectSingleNode(xpathUrl);
                            if (urlNode2 != null)
                                cs.AlbumUrls.Add(urlNode2.Attributes["href"].Value);
                        }

                    Byte i = 0;
                    // Get albums informations
                    foreach (var album_url in cs.AlbumUrls)
                    {
                        i++;

                        // Get information for this album
                        var album = new ComicAlbum();

                        album.Filename = $"{cs.SerieName} - {i.ToString("00")}";

                        var hWeb3 = new HtmlWeb();
                        var hDoc3 = hWeb.Load(album_url);

                        var hNode3a = hDoc3.DocumentNode.SelectSingleNode("//div[@class='bandeau-wrapper albums']");
                        var hNode3b = hDoc3.DocumentNode.SelectSingleNode("//img[@class='fadeover image_album']/@src");
                        var hNode3c = hDoc3.DocumentNode.SelectSingleNode("//span[@itemprop='ratingValue']");
                        var hNode3d = hDoc3.DocumentNode.SelectSingleNode("//span[@itemprop='ratingCount']");

                        var hNodes3 = hDoc3.DocumentNode.SelectNodes("(//ul[@class='infos-albums'])/li");

                        if (hNode3b != null && hNodes3 != null)
                        {
                            album.AlbumCover = hNode3b.Attributes["src"].Value;

                            album.Note = Single.Parse(hNode3c.InnerText, CultureInfo.InvariantCulture);
                            album.NoteCount = Single.Parse(hNode3d.InnerText, CultureInfo.InvariantCulture);

                            foreach (var node in hNodes3)
                            {
                                var infos = node.InnerText;
                                var left4 = node.InnerText.DeleteLineBreakAndSpace().Substring(0, 4).ToLower();

                                switch (left4)
                                {
                                    case "titr": // titre
                                        album.AlbumName = HttpUtility.HtmlDecode(infos.DeleteThis("Titre :"));
                                        break;
                                    case "tome": // tome
                                        var tome = infos.DeleteThis("Tome :");
                                        Byte tomeByte;
                                        Byte.TryParse(tome, out tomeByte);
                                        if (tomeByte > 0)
                                            album.AlbumOrder = Convert.ToByte(tomeByte);
                                        else
                                            album.AlbumOrder = 0;
                                        break;
                                    case "scén": // scénario
                                        album.AlbumScenarist = infos.DeleteThis("Scénario :");
                                        break;
                                    case "dess": // dessin
                                        album.AlbumDrawer = infos.DeleteThis("Dessin :");
                                        break;
                                    case "coul": // couleurs
                                        album.AlbumColorist = infos.DeleteThis("Couleurs :");
                                        break;
                                    case "dépo": // dépot légal
                                        var date = infos.DeleteThis("Dépot légal : ");
                                        var dt = new DateTime(1, 1, 1);
                                        if (date.Length >= 7)
                                            DateTime.TryParseExact(date.Substring(0, 7), "dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt);
                                        album.AlbumDate = dt;
                                        break;
                                    case "edit": // éditeur
                                        album.AlbumEditor = infos.DeleteThis("Editeur :");
                                        break;
                                    case "coll": // collection
                                        album.AlbumCollection = infos.DeleteThis("Collection :");
                                        break;
                                    case "isbn": // isbn
                                        album.AlbumIsbn = infos.DeleteThis("ISBN  :");
                                        break;
                                    case "plan": // nombre de pages
                                        var count = infos.DeleteThis("Planches :");
                                        Byte countByte;
                                        Byte.TryParse(count, out countByte);
                                        album.AlbumCount = countByte;
                                        break;
                                }
                            }
                        }

                        if (hNode3a == null)
                            album.AlbumOrder = 1;

                        album.AlbumCoverBytes = webClient.DownloadData(album.AlbumCover);

                        if(album.AlbumOrder > 0)
                            cs.ComicAlbums.Add(album);

                        Thread.Sleep(500);
                    }
                }

            return cs;
        }
    }
}