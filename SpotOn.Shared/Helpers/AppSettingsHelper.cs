using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotOn.Shared.Helpers
{
    public static class  AppSettingsHelper
    {
        const string MAX_POSTS = "MaxPosts";
        const string IMAGE_FOLDER = "ImagesFolder";
        const string DEFAULT_IMAGE_PATH = "DefaultImagePath";

        public static IConfiguration Configuration;

        private static string GetAppSetting(string key)
        {
            return Configuration.GetSection(key).Value; 
        }

        public static string GetMaxPost()
        {
            return GetAppSetting(MAX_POSTS);
        }

        public static string GetImageFolder()
        {
            return GetAppSetting(IMAGE_FOLDER);
        }

        public static string GetDefaultImagePath()
        {
            return GetAppSetting(DEFAULT_IMAGE_PATH);
        }
    }
}
