using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace Upload_Video_To_Youtube
{
    class UploadVideo
    {
        static string tagPath, videoPath, titleText, descripText, privacy, descPath;
        static string[] tagsList;
        [STAThread]
        static void Main(string[] args)
        {
            tagsList = new string[] { "Hi", "Zach", " R U ", "OK ?" };            
            try
            {
                if (args.Length==5)
                {
                    TagsClass tc = new TagsClass();
                    videoPath = args[0]; 
                    titleText = args[1];
                    descPath = args[2];
                    tagPath = args[3];
                    privacy = args[4];                    
                    if (CheckInput() == "NE")
                    {
                        tagsList = tc.GetTagList(tagPath);
                        descripText = ReadDesc(descPath);
                        try
                        {
                            UpVideo upVideo = new UpVideo();
                            upVideo.videoUp(videoPath, titleText, descripText, privacy, tagsList);
                        }
                        catch (Exception ex)
                        {
                            print("Error in Uploading\n Error is : " + ex.Message);
                        }
                    }
                    else
                    {
                        print(CheckInput());
                    }
                }
                else if (args.Length == 0)
                {
                    print("Error : There is no inputs");
                }
                else
                {
                    print("Error : Error in the inputs");
                }                
            }
            catch(Exception ex)
            {
                print("Error :\n" + ex.Message);
            }
            print("Press Any Key To Exit ...");
            Console.ReadKey();
        }       
        //Always The Description have a long text, So this Method used to read the description from a text file
        static string ReadDesc(string DescPath)
        {
            try
            {
                using (StreamReader sr = new StreamReader(DescPath))
                {
                    StringBuilder str = new StringBuilder();
                    while (sr.Peek() != -1)
                    {
                        str.Append(sr.ReadLine());
                        str.Append("\n");
                    }
                    return str.ToString();
                }

            }
            catch (Exception)
            {
                return "Wrong Path";
            }
        }
        static void print(string text)
        {
            Console.WriteLine(text);
        }
        static string CheckInput()
        {
            if (File.Exists(videoPath) && File.Exists(descPath) && File.Exists(tagPath))
                return "NE";
            else
                return "Error : File Missing";
        }
    }
}
