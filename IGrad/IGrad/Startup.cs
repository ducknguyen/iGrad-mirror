using Microsoft.Owin;
using Owin;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using System;
using System.IO;
using System.Reflection;
using System.Web;

[assembly: OwinStartupAttribute(typeof(IGrad.Startup))]
namespace IGrad
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            try
            {
                GlobalFontSettings.FontResolver = new WebFontResolver();
            }
            catch(Exception ex)
            {
                throw new Exception(string.Format("Unable to resolve the font resolver: {0}", ex));
            }
        }
    }

    public class WebFontResolver : IFontResolver
    {
        public byte[] GetFont(string faceName)
        {
            switch (faceName)
            {
                case "Arial":
                    return LoadFontData("arial.ttf");
                case "Arial#":
                    return LoadFontData("arial.ttf");
                case "Arial#b":
                    return LoadFontData("arialbd.ttf");
                case "Arial#i":
                    return LoadFontData("ariali.ttf");
                case "Arial#bi":
                    return LoadFontData("arialbi.ttf");

                case "Couri":
                    return LoadFontData("cour.ttf");
                case "Couri#":
                    return LoadFontData("cour.ttf");
                case "Couri#b":
                    return LoadFontData("courdb.ttf");
                case "Couri#i":
                    return LoadFontData("couri.ttf");
                case "Couri#bi":
                    return LoadFontData("courbi.ttf");
            }

            // should never hit this but just in case...
            return LoadFontData("arial.ttf");
        }

        /// <summary>
        /// Resolve the font name. Allows to get the correct font that the pdf is looking for
        /// </summary>
        /// <param name="familyName"></param>
        /// <param name="isBold"></param>
        /// <param name="isItalic"></param>
        /// <returns></returns>
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            string name = familyName.ToLower();

            switch (name)
            {
                case "arial":
                    if (isBold)
                    {
                        if (isItalic)
                        {
                            return new FontResolverInfo("Arial#bi");
                        }
                        return new FontResolverInfo("Arial#b");
                    }
                    if (isItalic)
                    {
                        return new FontResolverInfo("Arial#i");
                    }
                    return new FontResolverInfo("Arial#");
                case "courier new":
                    if (isBold)
                    {
                        if (isItalic)
                        {
                            return new FontResolverInfo("Couri#bi");
                        }
                        return new FontResolverInfo("Couri#b");
                    }
                    if(isItalic)
                    {
                        return new FontResolverInfo("Couri#i");
                    }
                    return new FontResolverInfo("Couri");
            }

            // if font not defined, just return arial. 
            return new FontResolverInfo("Arial#");
        }

        /// <summary>
        /// Return the font byte data from the font folder.
        /// </summary>
        private byte[] LoadFontData(string name)
        {
            // font path
            string fontPath = HttpContext.Current.Server.MapPath("~/media/fonts/" + name);

            using (MemoryStream ms = new MemoryStream())
            using (FileStream file = new FileStream(fontPath, FileMode.Open, FileAccess.Read))
            {
                byte[] fontData = new byte[file.Length];
                file.Read(fontData, 0, (int)file.Length);
                ms.Write(fontData, 0, (int)file.Length);

                return fontData;
            }
        }
    }
}
