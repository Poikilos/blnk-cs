/*
 * Created by SharpDevelop.
 * User: jgustafson
 * Date: 2/10/2016
 * Time: 10:40 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Diagnostics; //Process etc

namespace blnk
{
	/// <summary>
	/// Description of BlnkShortcut.
	/// </summary>
	public class BlnkShortcut
	{
		public bool get_is_correct_format() {
			return is_correct_format;
		}
		public string err = null;
		private bool is_correct_format=false;
		public string file_path = null; //path to the blnk file itself
		
		#region values written to blnk file
		public static readonly string Content_Type = "text/blnk";
		public string encoding_string = "UTF-8";
		public string type_string = null; //can only be Application, Directory, or Link
		public bool is_no_display = false;
		public bool is_terminal = false;
		public string name_string = null;
		public string comment_string = null;
		public string path_string = null;
		public string exec_string = null;
		#endregion values written to blnk file
		
		public BlnkShortcut()
		{
		}
		
		public BlnkShortcut(string file)
		{
			load(file);
		}
		
		public void save() {
			err=null;
			if (file_path!=null) {
				try {
					StreamWriter outs = null;
					outs = new StreamWriter(file_path);
					outs.WriteLine("Content-Type: "+Content_Type);
					if (encoding_string!=null) outs.WriteLine("Encoding: "+encoding_string);
					if (type_string!=null) outs.WriteLine("Type: "+type_string);
					outs.WriteLine("NoDisplay: "+(is_no_display?"true":"false"));
					outs.WriteLine("Terminal: "+(is_terminal?"true":"false"));
					if (name_string!=null) outs.WriteLine("Name: "+name_string);
					if (comment_string!=null) outs.WriteLine("Comment: "+comment_string);
					if (exec_string!=null) outs.WriteLine("Exec: "+exec_string);
					outs.Close();
				}
				catch (Exception exn) {
					err=exn.ToString();
				}
			}
			else {
				err="There is no filename yet. You must set file_path(such as when load is called and it it automatically set), normally ending in '.blnk'.";
			}
		}
		
		public Process get_new_process() {
			Process this_process = null;
			if (exec_string!=null) {
				this_process = new Process();
				this_process.StartInfo.FileName = exec_string;
				if (is_terminal) this_process.StartInfo.UseShellExecute = true;
				if (path_string!=null) this_process.StartInfo.WorkingDirectory = path_string;
			}
			return this_process;
		}
		
		
		public void load(string this_file_path) {
			err=null;
			file_path = this_file_path;
			string line;
			string content_type_string = null;
			StreamReader stream_in = null;
			stream_in = new StreamReader(this_file_path);
			
			int line_counting_number = 1;
			while ( (line=stream_in.ReadLine()) != null ) {
				if (!line.StartsWith("#")) {
					int delim_index = line.IndexOf(":");
					if (delim_index >= 0) {
						string name = line.Substring(0,delim_index).Trim();
						string param = line.Substring(delim_index+1).Trim();
						if (name.Length>0) {
							string name_lower = name.ToLower();
							if (name_lower=="path") {
								path_string = param;
							}
							else if (name_lower=="exec") {
								exec_string = param;
							}
							else if (name_lower=="content-type") {
								content_type_string = param;
							}
							else if (name_lower=="terminal") {
								if (param.ToLower()=="true") {
									is_terminal = true;
								}
								else {
									is_terminal = false;
								}
							}
							else if (name_lower=="nodisplay") {
								if (param.ToLower()=="true") {
									is_no_display = true;
								}
								else {
									is_no_display = false;
								}
							}
							else if (name_lower=="name") {
								name_string = param;
							}
							else if (name_lower=="comment") {
								comment_string = param;
							}
							else if (name_lower=="encoding") {
								encoding_string = param;
							}
							else if (name_lower=="type") {
								type_string = param;
							}
						}
						else {
							Console.Error.WriteLine("SYNTAX ERROR in '"+this_file_path+"' line "+line_counting_number.ToString()+": should start with name before semicolon");
						}
					}
				}//end if not comment
				if (content_type_string.ToLower()!="text/blnk") {
					is_correct_format=false;
					err = "SYNTAX ERROR: file '"+this_file_path+"' does not contain the line 'content-type: text/blnk'";
				}
				else {
					is_correct_format=true;
				}
				line_counting_number++;
			}//end while lines in file
			stream_in.Close();
		}//end load
	}//end class BlnkShortcut
}//end namespace
