/*
 * Created by SharpDevelop.
 * User: jgustafson
 * Date: 2/9/2016
 * Time: 9:18 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Reflection; //version etc

namespace blnk
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public static ArrayList blocations=new ArrayList();
		public static Cursor prev_cursor = null;
		public static readonly string my_name = "Blnk";
		public string[] arguments = null;
		public static string data_path = null;
		public static bool is_ok_to_repair_slash_in_name=true;
		public static bool is_ok_to_repair_type_string=true;
		public static string profiles_path=null;
		public static string userprofile_path=null;
		public static readonly string alternates_name = "alternates.yml";
		public static string alternates_path = null;
		
		public MainForm(string[] args)
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			arguments = args;
		}
		
		void update_window_size() {
			Application.DoEvents();
			//int correct_right = statusLabel.Right;
			int slack_x = this.Width - this.ClientRectangle.Width;
			int margin_x = statusLabel.Left;
			//if (this.ClientRectangle.Right<statusLabel.Right)
			this.Width = (statusLabel.Width + margin_x*2) + slack_x;
			Application.DoEvents();
		}
		
		public static bool PathExists(string path) {
			bool is_present=false;
			try {
				if (!string.IsNullOrEmpty(path)) {
					if (Directory.Exists(path) || File.Exists(path)) is_present=true;
				}
			}
			catch {} //don't care
			return is_present;
		}
		
		/// <summary>
		/// Run a lnk OR blnk file.
		/// If .blnk:
		/// DOES convert FullName to Name in Name field if is_ok_to_repair_slash_in_name is true.
		/// DOES change Type to Directory if file doesn't exist but Directory does if is_ok_to_repair_type_string is true.
		/// </summary>
		/// <param name="this_link_file_path"></param>
		/// <param name="is_deleting_lnk_desired"></param>
		/// <param name="status_counting_number"></param>
		/// <param name="status_max"></param>
		void run_link(string this_link_file_path, bool is_deleting_lnk_desired, int status_counting_number, int status_max) {
			statusLabel.Text = "Blnk...";
			//int bad_exec_count=0;
			update_window_size();
			//statusLabel.Text=this_link_file_path;
			if (!string.IsNullOrEmpty(this_link_file_path)) {
				if (this_link_file_path.ToLower().EndsWith(".lnk")) {
					if (this_link_file_path.Length>4) {
						statusLabel.Text = "Converting "+status_counting_number.ToString()+" of "+status_max.ToString()+" to blnk...";
						update_window_size();
						//StreamWriter streamOut=null;
						try {
							string converter_script_name = "lnk-to-blnk.vbs";
							string converter_script_path = Path.Combine(data_path,converter_script_name);
							Process script_p = new Process();
							FileInfo converter_fi = new FileInfo(converter_script_path);
							string this_blink_file_path = this_link_file_path.Substring(0, this_link_file_path.Length-4)+".blnk";
							FileInfo link_fi = new FileInfo(this_link_file_path);
							if (link_fi.Exists) {
								if (link_fi.Name.ToLower()=="target.lnk") {
									this_blink_file_path = Path.Combine(link_fi.Directory.Parent.FullName, link_fi.Directory.Name+".blnk");
								}
                                if (converter_fi.Exists) {
                                    script_p.StartInfo.FileName = converter_fi.FullName;
                                    script_p.StartInfo.Arguments = "\"" + this_link_file_path + "\"";
                                    //if (link_fi.Name.ToLower()=="target.lnk") {
                                    script_p.StartInfo.Arguments += " \"" + this_blink_file_path + "\"";
                                    //}
                                    script_p.Start ();
                                    script_p.WaitForExit (5000);
                                    try {
                                        if (is_deleting_lnk_desired && File.Exists (this_blink_file_path)) {
                                            File.Delete (this_link_file_path);
                                            if (link_fi.Name.ToLower () == "target.lnk") {
                                                link_fi.Directory.Delete (false);
                                            }
                                        }
                                    } catch (Exception exn) {
                                        Console.Error.WriteLine ("ERROR: Could not finish deleting " + this_link_file_path + ": " + exn.ToString ());
                                    }
                                } else {
                                    Console.Error.WriteLine ("ERROR: Could not find " + converter_script_path + " in system path or current working directory.");
                                }
	//							string program_path;
	//							
	//							streamOut=new StreamWriter(this_blink_file_name);
	//							
	//							WshShell shell = new WshShell(); //Create a new WshShell Interface
	//							IWshShortcut link = (IWshShortcut)shell.CreateShortcut(this_link_file_path); //Link the interface to our shortcut
	//							//MessageBox.Show(link.TargetPath); //Show the target in a MessageBox using IWshShortcut
	//							streamOut.WriteLine("Path:"+link.WorkingDirectory);
	//							streamOut.WriteLine("Icon:");
	//							streamOut.WriteLine("NoDisplay:true");
	//							streamOut.WriteLine("Terminal:false");
	//							streamOut.WriteLine("GenericName:");
	//							//streamOut.WriteLine("Encoding:utf-8");
	//							streamOut.WriteLine("Comment:File or folder shortcut generated by "+my_name);
	//							streamOut.WriteLine("Exec:"+link.TargetPath);
	//							streamOut.Close();
	//							streamOut=null;
                                if (!File.Exists(this_blink_file_path)) {
                                    statusLabel.Text = "Converting to blnk...FAIL ('" + converter_script_name + "' requires Windows)";
                                }
                                else {
                                    statusLabel.Text = "Converting to blnk...DONE";
                                }
							}
							else {
								statusLabel.Text = "Converting to blnk...FAIL (cannot access lnk file)";
							}
							update_window_size();
						}
						catch (Exception exn) {
							statusLabel.Text = "Converting to blnk...FAIL (could not finish)";
							update_window_size();
//							if (streamOut!=null) {
//								try {
//									streamOut.Close();
//									streamOut=null;
//								}
//								catch {}
//							}
							Console.Error.WriteLine("Could not finish processing link: "+exn.ToString());
						}
					}
					else {
						Console.Error.WriteLine("ERROR: Filename is "+this_link_file_path+" so it was not processed as a windows lnk Shortcut.");
					}
				}
				else if (this_link_file_path.ToLower().EndsWith(".blnk")) {
					string this_blink_file_path = this_link_file_path;
					//Process this_p = new Process();
					BlnkShortcut blnk = new BlnkShortcut(this_blink_file_path);
					//if (blnk.path_string!=null) this_p.StartInfo.WorkingDirectory = blnk.path_string;
					//if (blnk.exec_string!=null) this_p.StartInfo.FileName = blnk.exec_string;
					//if (blnk.is_terminal) this_p.StartInfo.UseShellExecute = true;
					Process this_p = blnk.get_new_process();
					if (is_ok_to_repair_slash_in_name) {
						if (blnk.name_string!=null) {
							int last_slash_index = blnk.name_string.LastIndexOf("\\");
							if (last_slash_index>-1) {
								if (last_slash_index==blnk.name_string.Length-1) {
									blnk.name_string = blnk.name_string.Substring(0, blnk.name_string.Length-1);
									last_slash_index = blnk.name_string.LastIndexOf("\\");
								}
								if (last_slash_index>-1) { //may have changed above
									blnk.name_string = blnk.name_string.Substring(last_slash_index+1);
								}
								blnk.save();
							}
						}
					}
					if (blnk.err!=null) {
						statusLabel.Text=blnk.err;
						blnk.err=null;
					}
					if (blnk.get_is_correct_format()) {
						if (!PathExists(this_p.StartInfo.FileName)) {
							string alt_path = get_alternate_path(this_p.StartInfo.FileName);
							if (alt_path!=this_p.StartInfo.FileName) {// && PathExists(alt_path)) {
								this_p.StartInfo.FileName = alt_path;
							}
						}
						if (!PathExists(this_p.StartInfo.WorkingDirectory)) {
							string alt_path = get_alternate_path(this_p.StartInfo.WorkingDirectory);
							if (alt_path!=this_p.StartInfo.WorkingDirectory) {// && PathExists(alt_path)) {
								this_p.StartInfo.WorkingDirectory = alt_path;
							}
						}
						if (is_ok_to_repair_type_string) {
							if (blnk.type_string!=null && blnk.type_string.ToLower()=="application") {
								try {
									//if (!File.Exists(blnk.exec_string) && Directory.Exists(blnk.exec_string)) {
									if (!File.Exists(this_p.StartInfo.FileName) && Directory.Exists(this_p.StartInfo.FileName)) {
										blnk.type_string="Directory";
										blnk.save();
									}
								}
								catch {} //don't care
							}
						}
						
						 
						if (!string.IsNullOrEmpty(this_p.StartInfo.FileName)) {
							if (blnk.type_string!=null && (blnk.type_string.ToLower()=="directory") && Directory.Exists(this_p.StartInfo.FileName)) { //(!File.Exists(this_p.StartInfo.FileName) && Directory.Exists(this_p.StartInfo.FileName)) {
								Process.Start(this_p.StartInfo.FileName);
								Application.Exit();
							}
							else if (File.Exists(this_p.StartInfo.FileName)) {
								this_p.Start();
								Application.Exit();
							}
							else {
								MessageBox.Show("Cannot launch because target does not exist. Please open a text editor, open the blnk file '"+this_blink_file_path+"', then fix the Exec line to point to a target that can be accessed (also ensure 'Type' line is correct) or get permission to access the target:\n\n'"+this_p.StartInfo.FileName+"'");
								Application.Exit();
							}
							
						}
						else {
							statusLabel.Text+="FAIL (nothing to do--missing Exec)";
							Console.Error.WriteLine("WARNING in '"+this_blink_file_path+"': missing exec line (no program to run)");
						}
					}
					else {
						statusLabel.Text="ERROR: file is not in correct format and will not be used.";
					}
				}
			}
			else {
				Console.Error.WriteLine("ERROR: Null or blank filename for link file itself.");
			}
		}
		
		public static string windows_users_string = "c:\\users";
		public static string get_alternate_path(string path) {
			string result = path;
			if (path!=null) {
				string lower_path = path.ToLower();
				if (lower_path.StartsWith(windows_users_string)) {
					try {
						if (!PathExists(path)) {
							int slash_after_username_index = path.IndexOf('\\', windows_users_string.Length+2);
							if (slash_after_username_index>-1) {
								if (Path.DirectorySeparatorChar=='/') {
									path=path.Replace('\\','/');
								}
								string theoretical_path = Path.Combine(userprofile_path, path.Substring(slash_after_username_index+1));
								//MessageBox.Show("Theoretical path:"+theoretical_path);
								if (PathExists(theoretical_path)) {
									result=theoretical_path;
								}
							}
							else MessageBox.Show("slash not found at or after "+(windows_users_string.Length+2).ToString()+" in "+path);
						}
						if (!PathExists(result)) {
							foreach (BlnkLocation blocation in blocations) {
								if (path.ToLower().StartsWith(blocation.original.ToLower())) {
									foreach (string s in blocation.alternates) {
										string theoretical_path=s+path.Substring(blocation.original.Length);
										if (PathExists(theoretical_path)) {
											result=theoretical_path;
										}
									}
								}
							}
						}
					}
					catch (Exception exn) {
						string msg=("Could not finish accessing "+path+":"+exn.ToString());
						//MessageBox.Show(msg);
						Console.WriteLine(msg);
					}//don't care
				}
				//else MessageBox.Show(lower_path+" does not start with "+windows_users_string);
			}
			else {
				Console.Error.WriteLine("ERROR: get_alternate_path got null path.");
			}
			return result;
		}
		
		void accept_drop(object sender, DragEventArgs e, bool is_deleting_lnk_desired) {
			try {
				string[] filepaths = (string[])e.Data.GetData(DataFormats.FileDrop, false);
				if (filepaths !=null) {
					int status_counting_number=1;
					foreach (string this_path in filepaths) {
						run_link(this_path, is_deleting_lnk_desired, status_counting_number, filepaths.Length);
					}
				}
			}
			catch (Exception exn) {
				Console.Error.WriteLine("ERROR: Could not finish accept_drop: "+exn.ToString());
			}
		}
		
		void MainFormDragEnter(object sender, DragEventArgs e)
		{
			//if (prev_cursor==null) prev_cursor = Cursor.Current;
			//Cursor.Current = Cursors.WaitCursor;
		}
		
		void StatusLabelDragEnter(object sender, DragEventArgs e)
		{
			//Cursor.Current = Cursors.WaitCursor;
		}
		
		void MainFormDragLeave(object sender, EventArgs e)
		{
			//if (prev_cursor!=null) Cursor.Current = prev_cursor;
			//else Cursor.Current = Cursors.Default;
		}
		
		void StatusLabelDragLeave(object sender, EventArgs e)
		{
			//if (prev_cursor!=null) Cursor.Current = prev_cursor;
			//else Cursor.Current = Cursors.Default;
		}
		
		void MainFormDragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}
		
		void StatusLabelDragOver(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}
		
		void MainFormDragDrop(object sender, DragEventArgs e)
		{
			accept_drop(sender, e, false);
		}
		
		void StatusLabelDragDrop(object sender, DragEventArgs e)
		{
			accept_drop(sender, e, true);
		}
		
		void MainFormLoad(object sender, EventArgs e)
		{
			try {
				if (Path.DirectorySeparatorChar=='/') {
					data_path = "/etc/blnk";
				}
				else {
					data_path = "C:\\ProgramData\\blnk";
				}
				alternates_path = Path.Combine(data_path, alternates_name);
				if (File.Exists(alternates_path)) {
					StreamReader ins = new StreamReader(alternates_path);
					string line_original;
					//string original=null;
					//string alternate=null;
					string name_string=null;
					string value_string=null;
					string section=null;
					string alternates_indent=null;
					string indent_string="";
					BlnkLocation this_location=null;
					while ( (line_original=ins.ReadLine()) != null ) {
						string line=line_original.Trim();
						if (!line.StartsWith("#") && !string.IsNullOrEmpty(line)) {
							indent_string = null;
							for (int i=0; i<line_original.Length; i++) {
								if (line_original[i]!=' ' && line_original[i]!='\t') {
									indent_string=line_original.Substring(0,i);
									break;
								}
							}
							//this should never happen: if (indent_string==null) indent_string="";
							if (line=="-") {
								section=null;
								alternates_indent=null;
								this_location=null;
								//original=null;
								//alternate=null;
							}
							else {
								if (section=="alternates") {
									if (indent_string.Length>alternates_indent.Length) {
										//Console.Error.WriteLine("Found alternate:");
										if (line.StartsWith("-")) {
											value_string=line.Substring(1).Trim();
											this_location.alternates.Add(value_string);
											//Console.Error.WriteLine("  - "+value_string);
										}
										else {
											section=null;
										}
									}
									else {
										//Console.Error.WriteLine(indent_string.Length.ToString()+"<="+alternates_indent.Length.ToString());
										section=null;
										alternates_indent=null;
									}
								}
								if (section!="alternates") {
									int delimiter_index=line.IndexOf(":");
									if (delimiter_index>=0) {
										name_string=line.Substring(0,delimiter_index);
										value_string=line.Substring(delimiter_index+1);
										section=null;
									}
									if (name_string.ToLower()=="alternates") {
										section="alternates";
										//Console.Error.WriteLine("Found alternates section...");
										alternates_indent=indent_string;
									}
									else {
										if (!string.IsNullOrEmpty(name_string) && !string.IsNullOrEmpty(value_string)) {
											if (name_string.ToLower()=="original") {
												//original=value_string;
												this_location=new BlnkLocation();
												this_location.original=value_string;
												blocations.Add(this_location);
												//Console.Error.WriteLine("Found original section...");
											}
										}
									}
								}
							}
						}
					}
					ins.Close();
					Console.Error.WriteLine("Found "+blocations.Count.ToString()+" location(s) in '"+alternates_path+"':");
					foreach (BlnkLocation this_bl in blocations) {
						Console.Error.WriteLine(this_bl.original+":");
						foreach (string s in this_bl.alternates) {
							Console.Error.WriteLine("  - "+s);
						}
					}
				}
				else {
					Console.Error.WriteLine("WARNING: '"+alternates_path+"' does not exist.");
				}
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish reading settings: "+exn.ToString());
			}
			try {
//				string version_string= ApplicationDeployment.IsNetworkDeployed
//                   ? ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString()
//                   : Assembly.GetExecutingAssembly().GetName().Version.ToString();
				Assembly assembly = Assembly.GetExecutingAssembly();
				FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
				string version_string = fileVersionInfo.ProductVersion;
				Console.Error.WriteLine("version_string:"+version_string);
			}
			catch (Exception exn) {
				Console.Error.WriteLine("Could not finish getting version: "+exn.ToString());
			}
			string participle="getting specialfolder";
			try {
				
				DirectoryInfo docs_di=new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
				participle="getting parent of '"+docs_di.FullName+"'";
				userprofile_path=docs_di.Parent.FullName;
				participle="getting parent of '"+docs_di.Parent.FullName+"'";
				profiles_path=docs_di.Parent.Parent.FullName;
				//MessageBox.Show("profiles_path:"+profiles_path);
			}
			catch (Exception exn) {
				string msg="Could not finish "+participle+" while getting profiles & userprofile folders in MainFormLoad: "+exn.ToString();
				Console.WriteLine(msg);
				//MessageBox.Show(msg);
			}
			
			if (arguments!=null && arguments.Length>0) {
				int status_counting_number=1;
				foreach (string this_path in arguments) {
					run_link(this_path, false, status_counting_number, arguments.Length);
				}
			}
			else {
				statusLabel.BackColor = Color.DarkRed;
				statusLabel.ForeColor = Color.White;
				statusLabel.Text="Drag LNK here to convert to BLNK then delete LNK.\nOtherwise drop LNK outside of this darker area.";
			}
			update_window_size();
		}
	}
}
