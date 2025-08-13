using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;

namespace Scada.Comm.Drivers.DrvFtpJP
{
    public class FilesDatabase
    {
        public FilesDatabase()
        {
            this.PathFile = string.Empty;
            this.LastTimeChanged = DateTime.MinValue;
            this.SizeFile = 0;
            this.NumberLines = 0;
            this.Parsed = false;
            this.Status = 0;
        }

        private string pathFile;
        public string PathFile
        {
            get { return pathFile; }
            set { pathFile = value; }
        }

        private DateTime lastTimeChanged;
        public DateTime LastTimeChanged
        {
            get { return lastTimeChanged; }
            set { lastTimeChanged = value; }

        }

        private long sizeFile;
        public long SizeFile
        {
            get { return sizeFile; }
            set { sizeFile = value; }
        }

        private int numberLines;
        public int NumberLines
        { 
            get { return numberLines; } 
            set { numberLines = value; } 
        }

        private bool parsed;
        public bool Parsed 
        { 
            get { return parsed; } 
            set { parsed = value; } 
        }

        private int status;
        public int Status 
        {
            get { return status; } 
            set { status = value; }
        }

        private string statusString;
        public string StatusString
        {
            get { return statusString; }
            set { statusString = value; }
        }

        public enum FileParsedStatus
        {
            NoParsed = 0,
            Parsed = 1,
            Error = 2,
        }


        public static List<FilesDatabase> LoadDB(string path)
        {
            List<FilesDatabase> rows = new List<FilesDatabase>();

            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                string[] parameters = s.Split("|", System.StringSplitOptions.None);
                if (parameters.Length >= 6)
                {
                    FilesDatabase line = new FilesDatabase();       
                    try { line.PathFile = parameters[0]; } catch { line.PathFile = string.Empty; }
                    try { line.LastTimeChanged = DateTime.Parse(parameters[1]); } catch { line.LastTimeChanged = DateTime.MinValue; }
                    try { line.SizeFile = Convert.ToInt32(parameters[2]); } catch { line.SizeFile = 0; }
                    try { line.NumberLines = Convert.ToInt32(parameters[3]); } catch { line.NumberLines = 0; }
                    try { line.Parsed = Convert.ToBoolean(parameters[4]); } catch { line.Parsed = false; }
                    try { line.Status = Convert.ToInt32(parameters[5]); } catch { line.Status = 3; }
                    rows.Add(line);
                }
            }
            sr.Close();

            return rows;
        }

        public static List<string> ListPathFiles(List<FilesDatabase> list)
        {
            List<string> result = new List<string>();
            foreach (FilesDatabase file in list)
            {
                result.Add(file.PathFile);
            }
            return result;
        }

        public static void AddRow(string path, FilesDatabase row)
        {
            StreamWriter streamWriter = File.AppendText(Path.Combine(path));

            string line = $@"{row.PathFile}|{row.LastTimeChanged.ToString()}|{row.SizeFile}|{row.NumberLines}|{row.Parsed.ToString()}|{row.Status}";

            streamWriter.WriteLine(line);
            streamWriter.Close();
        }

        public static void DeleteRow(string path, string pathFile)
        {
            string tempFile = path.Replace(".db", ".tmp");
            using (var sr = new StreamReader(path))
            {
                using (var sw = new StreamWriter(tempFile))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] parameters = s.Split("|", System.StringSplitOptions.None);
                        if (parameters.Length >= 6)
                        {
                            FilesDatabase row= new FilesDatabase();
                            try { row.PathFile = parameters[0]; } catch { row.PathFile = string.Empty; }
                            try { row.LastTimeChanged = DateTime.Parse(parameters[1]); } catch { row.LastTimeChanged = DateTime.MinValue; }
                            try { row.SizeFile = Convert.ToInt32(parameters[2]); } catch { row.SizeFile = 0; }
                            try { row.NumberLines = Convert.ToInt32(parameters[3]); } catch { row.NumberLines = 0; }
                            try { row.Parsed = Convert.ToBoolean(parameters[4]); } catch { row.Parsed = false; }
                            try { row.Status = Convert.ToInt32(parameters[5]); } catch { row.Status = 3; }

                            if (row.PathFile != pathFile)
                            {
                                string line = $@"{row.PathFile}|{row.LastTimeChanged.ToString()}|{row.SizeFile}|{row.NumberLines}|{row.Parsed.ToString()}|{row.Status}";
                                sw.WriteLine(line);
                            }
                        }
                    }
                }
            }
            File.Delete(path);
            File.Move(tempFile, path);
        }

        public static FilesDatabase SelectRow(string path, string pathFile)
        {
            FilesDatabase row = new FilesDatabase();

            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                string s = sr.ReadLine();
                string[] parameters = s.Split("|", System.StringSplitOptions.None);
                if (parameters.Length >= 6)
                {
                    FilesDatabase line = new FilesDatabase();
                    try { line.PathFile = parameters[0]; } catch { line.PathFile = string.Empty; }
                    try { line.LastTimeChanged = DateTime.Parse(parameters[1]); } catch { line.LastTimeChanged = DateTime.MinValue; }
                    try { line.SizeFile = Convert.ToInt32(parameters[2]); } catch { line.SizeFile = 0; }
                    try { line.NumberLines = Convert.ToInt32(parameters[3]); } catch { line.NumberLines = 0; }
                    try { line.Parsed = Convert.ToBoolean(parameters[4]); } catch { line.Parsed = false; }
                    try { line.Status = Convert.ToInt32(parameters[5]); } catch { line.Status = 3; }
                    
                    if(line.PathFile == pathFile)
                    {
                        sr.Close();
                        return line;        
                    }
                }
            }
            sr.Close();
            return null;
        }

        public static void UpdateRow(string path, FilesDatabase rowUpdate)
        {
            string tempFile = path.Replace(".db", ".tmp");
            using (var sr = new StreamReader(path))
            {
                using (var sw = new StreamWriter(tempFile))
                {
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        string[] parameters = s.Split("|", System.StringSplitOptions.None);
                        if (parameters.Length >= 6)
                        {
                            FilesDatabase row = new FilesDatabase();
                            try { row.PathFile = parameters[0]; } catch { row.PathFile = string.Empty; }
                            try { row.LastTimeChanged = DateTime.Parse(parameters[1]); } catch { row.LastTimeChanged = DateTime.MinValue; }
                            try { row.SizeFile = Convert.ToInt32(parameters[2]); } catch { row.SizeFile = 0; }
                            try { row.NumberLines = Convert.ToInt32(parameters[3]); } catch { row.NumberLines = 0; }
                            try { row.Parsed = Convert.ToBoolean(parameters[4]); } catch { row.Parsed = false; }
                            try { row.Status = Convert.ToInt32(parameters[5]); } catch { row.Status = 3; }

                            if (row.PathFile != rowUpdate.PathFile)
                            {
                                string line = $@"{row.PathFile}|{row.LastTimeChanged.ToString()}|{row.SizeFile}|{row.NumberLines}|{row.Parsed.ToString()}|{row.Status}";
                                sw.WriteLine(line);
                            }
                            else
                            {
                                string line = $@"{rowUpdate.PathFile}|{rowUpdate.LastTimeChanged.ToString()}|{rowUpdate.SizeFile}|{row.NumberLines}|{rowUpdate.Parsed.ToString()}|{rowUpdate.Status}";
                                sw.WriteLine(line);
                            }
                        }
                    }
                    sw.Close();
                }
                sr.Close();
            }


            File.Delete(path);
            File.Move(tempFile, path);
        }
    }
}
